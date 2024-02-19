using UnityEngine;
using Core;
using Zenject;
using UniRx;
using System;
using System.Collections;
using System.Collections.Generic;
using Signals.Game;
using Signals.Core;
using System.Xml;
using ScriptableObjects;
using Managers;
using Cinemachine;

namespace Gameplay
{
    [System.Serializable]
    public class TextEntry
	{
		public String Type;
		public String Name;
		public String Content;

	}

    public struct TextObject
    {
        public string Name;
        public string BodyText;

        public TextObject(string name, string bodyText)
        {
            Name = name;
            BodyText = bodyText;
        }

    }

    public class ReaderMediator: MediatorBase<ReaderView>, IInitializable, IDisposable
	{


		///  INSPECTOR VARIABLES       ///

		///  PRIVATE VARIABLES         ///
		private ReadState _readState = ReadState.Null;
		private TextStep _step;
		private Interactable _origin = null;
		private bool _endingStored;
		private CinemachineVirtualCamera _cam;
		///  PRIVATE METHODS           ///



		///  LISTNER METHODS           ///
		private void OnReadStateChanged(ReadState readstate)
		{

            if (readstate != _readState)
			{
				_readState = readstate;
				_view.SetReadUI(readstate);
			}
		}

		private void OnRecievedTypedMessage(string message)
        {
            OnFinishStep();
		}

        private void OnFinishedDrag()
        {
            OnFinishStep();
        }
        private void OnFinishStep()
		{
			if (_step)
			{
				if (_cam)
				{
					_cam.enabled = false;
				}

                if (_step.AddGoal)
				{
					foreach (Objective obj in _step.ObjectiveToAdd)
					{
						_signalBus.Fire(new AddObjectiveSignal() { Objective = obj });
					}
				}

				if (_step.HasEvent)
				{
                    _signalBus.Fire(new SendEventSignal() {EventKey=_step.EventKey  });

                }

				if (_step.ActivateTimer)
				{
                    _signalBus.Fire(new EnableTimerSignal() { });

                }

				if (_step.DeactivateTimer)
				{
                    _signalBus.Fire(new DisableTimerSignal() { });

                }

				if (_step.EndsGame)
				{
                    _signalBus.Fire(new EndedGameSignal() { });

                }


             

                if (_step.StartNextEntryOnCompletion)
				{
                    if (_origin != null)
                    {
                        _origin.IncrementStep();
                    }

                    _signalBus.Fire(new SendTextStepSignal() { TextStep=_step.GetNextStep(), Origin=_origin,CameraFocus=_cam});

                }
                else
				{
					if (_origin!=null)
					{
						_origin.IncrementStep();
					}

                    _signalBus.Fire(new StateChangeSignal() { ToState = State.Play });
                    _origin = null;
                    if (_step.CompleteGoalOnCompletion)
                    {
                     

                        _signalBus.Fire(new ObjectiveCompleteSignal() { Objective = _step.Objective });

                    }

				}

               
			
			}
			
		}

		private void OnReceiveStepAsset(TextStep textStep,Interactable origin,bool Storage =false, CinemachineVirtualCamera cam=null)
		{
			if (!_endingStored)
			{
				_origin = origin;
				_step = textStep;
				_cam = cam;
			}
			if (Storage)
			{
				_endingStored = true;
				return;
			}
			_endingStored = false;
            _signalBus.Fire(new StateChangeSignal() { ToState= State.Text});
            TextEntry entry = JsonUtility.FromJson<TextEntry>(_step.Json.text);

            entry.Content=entry.Content.Replace("\n","<page>");
			if (_cam)
			{
				_cam.enabled = true;
			}
            switch (entry.Type)
            {
                case "Read":
                    _signalBus.Fire(new ChangeReadStateSignal() { ReadState = ReadState.Text });
					_signalBus.Fire(new SendTextSignal() {  Text = new TextObject(entry.Name,entry.Content)});
                    break;
                case "MultipleChoice":
                    _signalBus.Fire(new ChangeReadStateSignal() { ReadState = ReadState.Choice });
					_signalBus.Fire(new ChoiceListSignal() { Choices = entry.Content.Split(",") }) ;
                    break;
				case "Typing":
                    _signalBus.Fire(new ChangeReadStateSignal() { ReadState = ReadState.Type});
					_signalBus.Fire(new SetTypingSignal() { Prompt = entry.Content });
					break;
                case "Dragging":
                    _signalBus.Fire(new ChangeReadStateSignal() { ReadState = ReadState.Drag });
                    _signalBus.Fire(new SetDragSignal() { Message = entry.Content });
                    break;



            }

        }

		private void ChoiceRecieved(int choice)
		{
			
			if (_step.IsMultipleChoice && _origin!=null)
			{ 
				_origin.IncrementStepByValue(choice);
			}
		}
		///  PUBLIC API                ///
		public ReadState GetState() { return _readState; }


		public void SendProgressReader()
		{
            _signalBus.Fire(new ProgressReaderSignal());

        }

		

        ///  IMPLEMENTATION            ///

        [Inject]

		private SignalBus _signalBus;

        [Inject] private GameSettings _gameSettings;


        readonly CompositeDisposable _disposables = new CompositeDisposable();

		public void Initialize()
		{
			_view.Init(this);
            _signalBus.GetStream<ChangeReadStateSignal>()
                         .Subscribe(x => OnReadStateChanged(x.ReadState)).AddTo(_disposables);
            _signalBus.GetStream<SendTextStepSignal>()
                         .Subscribe(x => OnReceiveStepAsset(x.TextStep,x.Origin,x.Storage,x.CameraFocus)).AddTo(_disposables);
            _signalBus.GetStream<FinishStepSignal>()
                         .Subscribe(x => OnFinishStep()).AddTo(_disposables);
            _signalBus.GetStream<ChoiceSendSignal>()
                        .Subscribe(x => ChoiceRecieved(x.Choice)).AddTo(_disposables);
            _signalBus.GetStream<SendTypedMessageSignal>()
                       .Subscribe(x => OnRecievedTypedMessage(x.Message)).AddTo(_disposables);
            _signalBus.GetStream<FinishDragSignal>()
                      .Subscribe(x => OnFinishedDrag()).AddTo(_disposables);
        }

		public void Dispose()
		{

			_disposables.Dispose();

		}

	}
}
