using UnityEngine;
using Core;
using Zenject;
using UniRx;
using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using Signals.Game;
using Signals.Core;

namespace NPC
{
	public class NPCMediator: MediatorBase<NPCView>, IInitializable, IDisposable
	{

        ///  INSPECTOR VARIABLES       ///

        ///  PRIVATE VARIABLES         ///

        ///  PRIVATE METHODS           ///

        ///  LISTNER METHODS           ///

        ///  PUBLIC API                ///
        public void SendStep(TextStep step, Transform transform=null)
        {
            _signalBus.Fire(new SendTextStepSignal() { TextStep = step });
            if (transform != null)
            {
                _signalBus.Fire(new CameraFocusSignal() { Focus= transform });
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
