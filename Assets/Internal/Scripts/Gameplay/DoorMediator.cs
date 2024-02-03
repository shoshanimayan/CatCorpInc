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
	public class DoorMediator: MediatorBase<DoorView>, IInitializable, IDisposable
	{

		///  INSPECTOR VARIABLES       ///

		///  PRIVATE VARIABLES         ///

		///  PRIVATE METHODS           ///

		///  LISTNER METHODS           ///
		private void OnEventRecieved(int key)
		{
			if (_view.CheckEvent(key))
			{
				_view.DoorState = true;
			}
		}
		///  PUBLIC API                ///

		///  IMPLEMENTATION            ///

		[Inject]

		private SignalBus _signalBus;

		readonly CompositeDisposable _disposables = new CompositeDisposable();

		public void Initialize()
		{
            _signalBus.GetStream<SendEventSignal>()
                   .Subscribe(x => OnEventRecieved(x.EventKey)).AddTo(_disposables);
        }

		public void Dispose()
		{

			_disposables.Dispose();

		}

	}
}
