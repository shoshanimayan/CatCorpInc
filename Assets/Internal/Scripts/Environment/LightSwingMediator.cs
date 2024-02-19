using UnityEngine;
using Core;
using Zenject;
using UniRx;
using System;
using System.Collections;
using System.Collections.Generic;
using Signals.Core;

namespace Environment
{
	public class LightSwingMediator: MediatorBase<LightSwingView>, IInitializable, IDisposable
	{

		///  INSPECTOR VARIABLES       ///

		///  PRIVATE VARIABLES         ///

		///  PRIVATE METHODS           ///

		///  LISTNER METHODS           ///
		public void OnGameEnding()
		{ 
			_view.EnableSwing();
		}

		public void OnEndedGame()
		{ 
			_view.DisableSwing();
		}
		///  PUBLIC API                ///

		///  IMPLEMENTATION            ///

		[Inject]

		private SignalBus _signalBus;

		readonly CompositeDisposable _disposables = new CompositeDisposable();

		public void Initialize()
		{
            _signalBus.GetStream<EndingGameSignal>()
					.Subscribe(x => OnGameEnding()).AddTo(_disposables);
            _signalBus.GetStream<EndedGameSignal>()
                   .Subscribe(x => OnEndedGame()).AddTo(_disposables);
        }

		public void Dispose()
		{

			_disposables.Dispose();

		}

	}
}
