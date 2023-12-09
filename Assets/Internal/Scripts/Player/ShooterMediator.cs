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
	public class ShooterMediator: MediatorBase<ShooterView>, IInitializable, IDisposable
	{

		///  INSPECTOR VARIABLES       ///

		///  PRIVATE VARIABLES         ///
		private bool _canShoot;
		///  PRIVATE METHODS           ///
		private State _currentState;
        ///  LISTNER METHODS           ///
        private void OnStateChanged(State state)
        {
            _currentState = state;
			_view.ForceCoolDown();
            
        }
        ///  PUBLIC API                ///
        public bool GetCanShoot()
		{ 
			return _gameSettings.GetCanShoot() && _currentState==State.Play;
		}

		public void SendCoffeeHitPosition(Vector3 pos)
		{
			_signalBus.Fire(new SendCoffeePositionSignal() { Position = pos });
		}
		///  IMPLEMENTATION            ///

		[Inject]

		private SignalBus _signalBus;

        [Inject] private GameSettings _gameSettings;


        readonly CompositeDisposable _disposables = new CompositeDisposable();

		public void Initialize()
		{
			_canShoot = _gameSettings.GetCanShoot();
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
