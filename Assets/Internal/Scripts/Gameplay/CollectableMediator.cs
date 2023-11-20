using UnityEngine;
using Core;
using Zenject;
using UniRx;
using System;
using System.Collections;
using System.Collections.Generic;
using Signals.Core;

namespace Gameplay
{
	public class CollectableMediator: MediatorBase<CollectableView>, IInitializable, IDisposable
	{

        ///  INSPECTOR VARIABLES       ///

        ///  PRIVATE VARIABLES         ///

        ///  PRIVATE METHODS           ///

        ///  LISTNER METHODS           ///
        public void CollectObject()
        {
            _signalBus.Fire(new GotCollectableSignal());
			
        }
        ///  PUBLIC API                ///

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
