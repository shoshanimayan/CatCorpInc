using UnityEngine;
using Core;
using Zenject;
using UniRx;
using System;
using System.Collections;
using System.Collections.Generic;
using Signals.Game;
using Signals.Core;
using Managers;

namespace Player
{
	public class InteractorMediator: MediatorBase<InteractorView>, IInitializable, IDisposable
	{

		///  INSPECTOR VARIABLES       ///

		///  PRIVATE VARIABLES         ///
		State _currentState ;
        ///  PRIVATE METHODS           ///

        ///  LISTNER METHODS           ///
        private void OnStateChanged(State state)
        {
            _currentState = state;

        }
        ///  PUBLIC API                ///
        public void SetHovering(string hovering) {
			_signalBus.Fire(new HoveringSignal() { Hovering = hovering });
		}

		public bool CanInteract()
		{
			return _currentState == State.Play;
		}
		///  IMPLEMENTATION            ///

		[Inject]

		private SignalBus _signalBus;

		readonly CompositeDisposable _disposables = new CompositeDisposable();

		public void Initialize()
		{
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
