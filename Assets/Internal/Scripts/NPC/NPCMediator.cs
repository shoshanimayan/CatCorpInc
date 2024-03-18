using UnityEngine;
using Core;
using Zenject;
using UniRx;
using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using Signals.Game;
using Signals.Core;
using Managers;
using DG.Tweening;
using Gameplay;

namespace NPC
{
	public class NPCMediator: MediatorBase<NPCView>, IInitializable, IDisposable
	{

        ///  INSPECTOR VARIABLES       ///

        ///  PRIVATE VARIABLES         ///
        private bool _gotCoffee;
        private State _currentState;
        ///  PRIVATE METHODS           ///

        ///  LISTNER METHODS           ///
        private void OnStateChanged(State state)
        {
            _currentState = state;
            if (state == State.Play)
            {
                _view.ShowQuestSymbol();
            }
            else if (state==State.Text)
            {
                DOTween.Kill("coffee");
                _view.HideSymbol();
            }
        }
        private void GotCollectable(int key )
        {
            if (_view.GetNeedsCollectable() && _view.GetCollectableKey()==key)
            { 
                _view.ForceIncrementStep();
            }
        }

        

        private void UnblockStep(TextStep step)
        {
            if (_view.IsStepBlocked() &&_view.CheckForMatchingStep(step))
            {
                _view.ForceIncrementStep();
            }
        }

        private void OnObjectiveCompleted(Objective obj)
        {
            if (_view.CompareObjective(obj))
            {
                _view.DeactivateQuest();
            }
        }

        private void OnRecievedProgressCompletion(float percent)
        {
            if (_view.IsBoss)
            {
                int index = 0;
                if (percent > .5f)
                {
                    if (percent == 1)
                    {
                        index = 2;
                    }
                    else
                    {
                        index = 1;
                    }
                }
                _view.SetStepIndex(index);
            }
        }


        private void OnGameEnding()
        {
            if (!_view.IsBoss)
            {
                _view.DisableNPC();
            }
        }
        ///  PUBLIC API                ///
        public void GotCoffee()
        {
            if (!_gotCoffee && !_view.IsBoss) {
                _view.ShowHeartSymbol();
                _gotCoffee = true;
                _signalBus.Fire(new GotCoffeeSignal());
            }
        }

        public void SetCoffee()
        {
            if (!_view.IsBoss)
            {
                _signalBus.Fire(new SetCoffeeSignal());
            }
        }

        public void SendStep(TextStep step, NPCView view, Transform transform = null)
        {
            _signalBus.Fire(new SendTextStepSignal() { TextStep = step, Origin=view, CameraFocus = _view.Cam });
            if (transform != null)
            {
                _signalBus.Fire(new CameraFocusSignal() { Focus= transform });
            }

        }

        public void SendUnblock(TextStep unblock)
        {
            _signalBus.Fire(new UnblockedConversationSignal() {Unblock=unblock});
        }

        public State GetCurrentState()
        {

            return _currentState;
        }


        ///  IMPLEMENTATION            ///

        [Inject]

		private SignalBus _signalBus;

		readonly CompositeDisposable _disposables = new CompositeDisposable();

		public void Initialize()
		{
            _view.Init(this);
            _signalBus.GetStream<GotCollectableSignal>()
             .Subscribe(x => GotCollectable(x.Key)).AddTo(_disposables);
            _signalBus.GetStream<UnblockedConversationSignal>()
            .Subscribe(x => UnblockStep(x.Unblock)).AddTo(_disposables);
            _signalBus.GetStream<ObjectiveCompleteSignal>().Subscribe(x => OnObjectiveCompleted(x.Objective));
            _signalBus.GetStream<StateChangedSignal>()
              .Subscribe(x => OnStateChanged(x.ToState)).AddTo(_disposables);
            _signalBus.GetStream<CompletionPercentageSignal>()
            .Subscribe(x => OnRecievedProgressCompletion(x.CompletionPercent)).AddTo(_disposables);
            _signalBus.GetStream<EndingGameSignal>()
           .Subscribe(x => OnGameEnding()).AddTo(_disposables);
            _view.ShowQuestSymbol();
        }

		public void Dispose()
		{

			_disposables.Dispose();

		}

	}
}
