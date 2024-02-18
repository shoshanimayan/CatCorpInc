using UnityEngine;
using Core;
using Zenject;
using UniRx;
using System;
using System.Collections;
using System.Collections.Generic;
using Signals.Core;
using Managers;
using Signals.Game;
using Gameplay;

namespace Player
{
	public class PlayerControllerMediator: MediatorBase<PlayerControllerView>, IInitializable, IDisposable
	{

		///  INSPECTOR VARIABLES       ///

		///  PRIVATE VARIABLES         ///
		private bool _canInput;
        private bool _ended;
        public bool _transitionSignalSent;
		private State _currentState;
		private ReadState _readState= ReadState.Null;
        ///  PRIVATE METHODS           ///
        private void PlayWalkSound(WalkState state, Vector3 position)
        {
            switch (state)
            {
                case WalkState.Walk:
                    _signalBus.Fire(new PlaySoundSignal() { ClipName = "walk" });
                    _signalBus.Fire(new StopSoundSignal() { ClipName = "run" });


                    break;
                case WalkState.Run:
                    _signalBus.Fire(new PlaySoundSignal() { ClipName = "run" });
                    _signalBus.Fire(new StopSoundSignal() { ClipName = "walk" });


                    break;
                case WalkState.None:
                    _signalBus.Fire(new StopSoundSignal() { ClipName = "walk" });
                    _signalBus.Fire(new StopSoundSignal() { ClipName = "run" });


                    break;
            }

        }
        ///  LISTNER METHODS           ///
        private void OnStateChanged(State state)
		{
            _signalBus.Fire(new WalkStateChangedSignal() { ToState = WalkState.None });

            _currentState = state;
			if (_currentState != State.Text)
			{
				_readState = ReadState.Null;
			}
			if (state == State.Play)
			{ 
				_canInput = true;
				_view.EnableInputPlay(true);
			}	
			else
			{
				_canInput= false;
                _view.EnableInputPlay(false);

            }
            switch (state)
			{
				case State.Play:
                    Cursor.lockState = CursorLockMode.Locked;
                    break;
				case State.Text:
                    _view.StopWalkState();
                    Cursor.lockState = CursorLockMode.Confined;
                    break;
				case State.Paused:
                    _view.StopWalkState();
                    Cursor.lockState = CursorLockMode.Confined;
                    break;
                case State.Objective:
                    _view.StopWalkState();
                    Cursor.lockState = CursorLockMode.Confined;
                    break;
            }
		}

        private void OnReadStateChanged(ReadState readstate)
        {

            if (readstate != _readState)
            {
                _readState = readstate;
               
            }
        }
        ///  PUBLIC API                ///
        public void ChangeWalkState(WalkState state)
		{
            PlayWalkSound(state,_view.transform.position);
			_signalBus.Fire(new WalkStateChangedSignal() { ToState = state });
		}

		public bool IsEnd
        {
            get { return _ended; }
        }

		public bool CanReadInput()
		{
			return _canInput;
		}

		public bool IsReadStateClickable()
		{
			return _readState == ReadState.Text;
		}

		public void ProgressReader()
		{

			_signalBus.Fire(new ProgressReaderSignal());
		}

		public void TogglePauseMenu()
		{

			if (_currentState == State.Paused)
			{
				_signalBus.Fire(new StateChangeSignal() { ToState=State.Play });
			}
            else if (_currentState == State.Play)
            {
                _signalBus.Fire(new StateChangeSignal() { ToState = State.Paused });

            }
        }

        public void EndTransition()
        {
            if (!_transitionSignalSent)
            {
                _transitionSignalSent = true;
                _signalBus.Fire(new EndTransitionSignal());
            }
        }

        public void ToggleObjectiveMode()
		{
            if (_currentState == State.Objective)
            {
                _signalBus.Fire(new StateChangeSignal() { ToState = State.Play });
            }
            else if(_currentState==State.Play)
            {
                _signalBus.Fire(new StateChangeSignal() { ToState = State.Objective });

            }
        }

		public State GetCurrentState() { return _currentState; }

		///  IMPLEMENTATION            ///

		[Inject]

		private SignalBus _signalBus;
        [Inject] private GameSettings _gameSettings;

        readonly CompositeDisposable _disposables = new CompositeDisposable();

		public void Initialize()
		{
			_view.Init(this);
            _signalBus.GetStream<StateChangedSignal>()
                           .Subscribe(x => OnStateChanged(x.ToState)).AddTo(_disposables);
            _signalBus.GetStream<ChangeReadStateSignal>()
                         .Subscribe(x => OnReadStateChanged(x.ReadState)).AddTo(_disposables);
            _signalBus.GetStream<EndedGameSignal>()
               .Subscribe(x => { 
                   _ended = true;
                   Cursor.lockState = CursorLockMode.Confined;
               }).AddTo(_disposables);
            _gameSettings.SetCanShoot(false) ;
        }

		public void Dispose()
		{

			_disposables.Dispose();

		}

	}
}
