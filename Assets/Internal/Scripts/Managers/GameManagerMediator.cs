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
            _signalBus.Fire(new SendTextStepSignal() { TextStep = _view.Outro });

        }
        ///  PUBLIC API                ///
        public void StartIntro( TextStep intro) 
		{
			_signalBus.Fire(new SendTextStepSignal() { TextStep=intro });

		}
		///  IMPLEMENTATION            ///

		[Inject]

		private SignalBus _signalBus;

		readonly CompositeDisposable _disposables = new CompositeDisposable();

		public void Initialize()
		{
			_view.Init(this);
            _signalBus.GetStream<EndingGameSignal>()
         .Subscribe(x => EndingGame()).AddTo(_disposables);
        }

		public void Dispose()
		{

			_disposables.Dispose();

		}

	}
}
