
using Core;
using Zenject;
using UniRx;
using System;

using Managers;
using Signals.Core;
using Audio;
using Signals.Game;
using UnityEngine;

namespace Ui
{
	public class TimerMediator: MediatorBase<TimerView>, IInitializable, IDisposable
	{

        ///  INSPECTOR VARIABLES       ///

        ///  PRIVATE VARIABLES         ///
        private bool _firstTime=true;
        ///  PRIVATE METHODS           ///

        ///  LISTNER METHODS           ///
        private void OnStateChanged(State state)
        {
            if (!_gameSettings.GetTimerEnabled()) { return; }
            switch (state)
            {
                case State.Play:
                    if (_firstTime)
                    {
                        _firstTime = false;
                        TimerEnabled = true;
                        _view.TimerCanvasEnabled(true);

                    }
                    if (TimerEnabled)
                    {
                        _view.CountDownActive = true;
                        _view.TimerCanvasEnabled(true);

                    }
                    break;
                case State.Text:
                    if (TimerEnabled)
                    {
                        _view.TimerCanvasEnabled(false);
                        _view.CountDownActive = false;

                    }
                    break;
                default:
                    _view.TimerCanvasEnabled(false);
                    _view.CountDownActive = false;
                    break;
            }

        }

        private void EnabledTimer()
        {
            if (!_gameSettings.GetTimerEnabled())
            { 
                _gameSettings.SetTimerEnabled(true);
                TimerEnabled = true;
                _view.ShowTimerText(true);

            }
        }

        private void DisableTimer()
        {
            if (_gameSettings.GetTimerEnabled())
            {
                _gameSettings.SetTimerEnabled(false);
                TimerEnabled = false;
                _view.ShowTimerText(false);
            }
        }
        ///  PUBLIC API                ///
        public bool TimerEnabled;

        

        public void TransitionToLateMusic()
        {
            _signalBus.Fire(new TransitionMusicSignal() {  musicState=MusicState.song2});

        }

        public void EndTimer()
        {
            if (!_gameSettings.GetEnded())
            {
                _gameSettings.SetEnded(true);
                _signalBus.Fire(new EndingGameSignal() {  });

            }

        }

        private void EndingGame()
        {

            DisableTimer();
        }
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
            _signalBus.GetStream<EnableTimerSignal>()
            .Subscribe(x => EnabledTimer()).AddTo(_disposables);
            _signalBus.GetStream<DisableTimerSignal>()
         .Subscribe(x => DisableTimer()).AddTo(_disposables);
            _signalBus.GetStream<EndingGameSignal>()
         .Subscribe(x => EndingGame()).AddTo(_disposables);
        }

		public void Dispose()
		{

			_disposables.Dispose();

		}

	}
}
