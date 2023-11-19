using UnityEngine;
using Core;
using Zenject;
using UniRx;
using System;
using System.Collections;
using System.Collections.Generic;
using Managers;

namespace Player
{
	public class ShooterMediator: MediatorBase<ShooterView>, IInitializable, IDisposable
	{

		///  INSPECTOR VARIABLES       ///

		///  PRIVATE VARIABLES         ///
		private bool _canShoot;
		///  PRIVATE METHODS           ///

		///  LISTNER METHODS           ///

		///  PUBLIC API                ///
		public bool GetCanShoot()
		{ 
			return _canShoot;
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
		}

		public void Dispose()
		{

			_disposables.Dispose();

		}

	}
}
