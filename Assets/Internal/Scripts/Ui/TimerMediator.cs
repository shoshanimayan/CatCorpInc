
using Core;
using Zenject;
using UniRx;
using System;

using Managers;
using Signals.Core;
using Audio;

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
                        _view.CountDownActive = true;
                        _view.TimerCanvasEnabled(true);

                    }
                    break;
                default:
                    _view.TimerCanvasEnabled(false);
                    _view.CountDownActive = false;
                    break;
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
            _signalBus.Fire(new LoadSceneSignal() { SceneToLoad = SceneState.Menu });

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
        }

		public void Dispose()
		{

			_disposables.Dispose();

		}

	}
}
