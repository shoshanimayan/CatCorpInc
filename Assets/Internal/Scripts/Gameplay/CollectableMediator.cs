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
            _signalBus.Fire(new SendTextStepSignal() { TextStep = step, Origin = view });
            if (transform != null)
            {
                _signalBus.Fire(new CameraFocusSignal() { Focus = transform });
            }

        }
        ///  IMPLEMENTATION            ///

        [Inject]

		private SignalBus _signalBus;

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
