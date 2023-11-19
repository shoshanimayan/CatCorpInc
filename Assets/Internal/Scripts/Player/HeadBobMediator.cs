using UnityEngine;
using Core;
using Zenject;
using UniRx;
using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using Signals.Core;
using Signals.Game;

namespace Player
{
	public class HeadBobMediator: MediatorBase<HeadBobView>, IInitializable, IDisposable
	{

		///  INSPECTOR VARIABLES       ///

		///  PRIVATE VARIABLES         ///
		///  PRIVATE METHODS           ///

		///  LISTNER METHODS           ///
		private void OnWalkStateChange(WalkState state) {
			if (_gameSettings.CanHeadBob())
			{ 
				_view.CurrentWalkState = state;
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
            _signalBus.GetStream<WalkStateChangedSignal>()
                         .Subscribe(x => OnWalkStateChange(x.ToState)).AddTo(_disposables);

			if (!_gameSettings.CanHeadBob())
			{
                _view.CurrentWalkState = WalkState.None;
			}
        }

		public void Dispose()
		{

			_disposables.Dispose();

		}

	}
}
