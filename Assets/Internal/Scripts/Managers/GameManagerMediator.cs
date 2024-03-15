using UnityEngine;
using Core;
using Zenject;
using UniRx;
using System;
using System.Collections;
using System.Collections.Generic;
using Signals.Core;
using Signals.Game;
using ScriptableObjects;

namespace Managers
{
	public class GameManagerMediator: MediatorBase<GameManagerView>, IInitializable, IDisposable
	{

		///  INSPECTOR VARIABLES       ///

		///  PRIVATE VARIABLES         ///

		///  PRIVATE METHODS           ///

		///  LISTNER METHODS           ///
		private void EndingGame()
		{
            _signalBus.Fire(new SendTextStepSignal() { TextStep = _view.Outro, CameraFocus = _view.Camera });
            _signalBus.Fire(new PlayOneShotSignal() { ClipName = "speaker" });


        }
        private void SendStorage()
        {
            _signalBus.Fire(new SendTextStepSignal() { TextStep = _view.Outro,Storage=true, CameraFocus = _view.Camera });

        }

        ///  PUBLIC API                ///
        public void StartIntro( TextStep intro) 
		{
			_signalBus.Fire(new SendTextStepSignal() { TextStep=intro});

		}
		///  IMPLEMENTATION            ///

		[Inject]

		private SignalBus _signalBus;
        [Inject] private GameSettings _gameSettings;

        readonly CompositeDisposable _disposables = new CompositeDisposable();

		public void Initialize()
		{
            _gameSettings.SetEnded(false);
            _gameSettings.SetCanShoot(false);
            _view.Init(this);
            _signalBus.GetStream<EndingGameSignal>()
         .Subscribe(x => EndingGame()).AddTo(_disposables);

            _signalBus.GetStream<SendStorageSignal>()
       .Subscribe(x =>SendStorage()).AddTo(_disposables);
        }

		public void Dispose()
		{

			_disposables.Dispose();

		}

	}
}
