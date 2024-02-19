using UnityEngine;
using Core;
using Zenject;
using UniRx;
using System;
using System.Collections;
using System.Collections.Generic;
using Signals.Core;
using NPC;
using ScriptableObjects;
using Signals.Game;
using Managers;

namespace Gameplay
{
	public class CollectableMediator: MediatorBase<CollectableView>, IInitializable, IDisposable
	{

        ///  INSPECTOR VARIABLES       ///

        ///  PRIVATE VARIABLES         ///

        ///  PRIVATE METHODS           ///

        ///  LISTNER METHODS           ///
        public void CollectObject(int key)
        {
            _signalBus.Fire(new GotCollectableSignal() { Key=key});
			
        }
        ///  PUBLIC API                ///
		public void SendStep(TextStep step, CollectableView view, Transform transform = null)
        {
            _signalBus.Fire(new SendTextStepSignal() { TextStep = step, Origin = view ,CameraFocus=_view.Camera});
            if (transform != null)
            {
                _signalBus.Fire(new CameraFocusSignal() { Focus = transform });
            }

        }

        public bool GameStarted()
        {
            return _gameSettings.GetTimerEnabled();
        }
        ///  IMPLEMENTATION            ///

        [Inject]

		private SignalBus _signalBus;
        [Inject]
        private GameSettings _gameSettings;


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
