using UnityEngine;
using Core;
using Zenject;
using UniRx;
using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using ScriptableObjects;
using Signals.Core;
using Signals.Game;

namespace Gameplay
{
	public class CoffeePotMediator: MediatorBase<CoffeePotView>, IInitializable, IDisposable
	{

		///  INSPECTOR VARIABLES       ///

		///  PRIVATE VARIABLES         ///

		///  PRIVATE METHODS           ///

		///  LISTNER METHODS           ///

		///  PUBLIC API                ///
		public void ActivateCoffee()
		{
			_gameSettings.SetCanShoot(true);
		}

        public void SendStep(TextStep step, CoffeePotView view, Transform transform = null)
        {
            _signalBus.Fire(new SendTextStepSignal() { TextStep = step, Origin = view ,CameraFocus = _view.Camera });
            if (transform != null)
            {
                _signalBus.Fire(new CameraFocusSignal() { Focus = transform });
            }

        }
        ///  IMPLEMENTATION            ///

        [Inject]

		private SignalBus _signalBus;
        [Inject] private GameSettings _gameSettings;


        readonly CompositeDisposable _disposables = new CompositeDisposable();

		public void Initialize()
		{
			_view.Init(this);
		}

		public void Dispose()
		{

			_disposables.Dispose();

		}

	}
}
