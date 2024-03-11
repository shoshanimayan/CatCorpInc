using UnityEngine;
using Core;
using Zenject;
using UniRx;
using System;

using Signals.Core;
using Managers;

namespace Audio
{
	public class AudioManagerMediator: MediatorBase<AudioManagerView>, IInitializable, IDisposable
	{

		///  INSPECTOR VARIABLES       ///

		///  PRIVATE VARIABLES         ///
		
		///  PRIVATE METHODS           ///

		///  LISTNER METHODS           ///
		private void OnGetOneShot(string key, Vector3 worldPos)
		{ 
			_view.PlayOneShot(key, worldPos);
		}

		private void OnPlaySound(string key, Vector3 worldPos)
        {
            _view.PlaySound(key, worldPos);
        }

        private void OnStopSound(string key)
		{ 
			_view.StopClipInstance(key);
		}

		private void OnStopAllSounds()
		{ 
			_view.KillAllClips();
		}


		private void OnPlayMusic(string song=null)
		{ 
			_view.PlayMusic(song);
		}

        private void OnPauseMusic(string song=null)
        {
            _view.PauseMusic(song);
        }

		private void OnTransitionMusic(MusicState musicState)
		{ 
			_view.TransitionMusicState(musicState);
		}

		private void OnStateChange(State state)
		{
			switch (state)
			{
				case State.Paused:
					_view.PauseMusic();
					break;
				default:
					if (!_gameSettings.GetEnded())
					{
						_view.PlayMusic();
					}
					break;
			
			}
		}

        ///  PUBLIC API                ///

        ///  IMPLEMENTATION            ///

        [Inject]

		private SignalBus _signalBus;

        [Inject] private GameSettings _gameSettings;


        readonly CompositeDisposable _disposables = new CompositeDisposable();

		public void Initialize()
		{
            _signalBus.GetStream<PlayOneShotSignal>()
                                .Subscribe(x => OnGetOneShot(x.ClipName,x.WorldPos)).AddTo(_disposables);

            _signalBus.GetStream<StopSoundSignal>()
                               .Subscribe(x => OnStopSound(x.ClipName)).AddTo(_disposables);
            _signalBus.GetStream<StopAllSoundsSignal>()
                                  .Subscribe(x => OnStopAllSounds()).AddTo(_disposables);
            _signalBus.GetStream<StateChangedSignal>()
                     .Subscribe(x => { OnStopAllSounds();OnStateChange(x.ToState); }).AddTo(_disposables);
            _signalBus.GetStream<PlaySoundSignal>()
                    .Subscribe(x => OnPlaySound(x.ClipName, x.WorldPos)).AddTo(_disposables);
            _signalBus.GetStream<PauseMusicSignal>()
                              .Subscribe(x => OnPauseMusic(x.ClipName)).AddTo(_disposables);

            _signalBus.GetStream<PlayMusicSignal>()
                            .Subscribe(x => OnPlayMusic(x.ClipName)).AddTo(_disposables);

            _signalBus.GetStream<TransitionMusicSignal>()
                            .Subscribe(x => OnTransitionMusic(x.musicState)).AddTo(_disposables);
            _signalBus.GetStream<EndingGameSignal>()
        .Subscribe(x => OnPauseMusic()).AddTo(_disposables);
        }

		public void Dispose()
		{

			_disposables.Dispose();

		}

	}
}
