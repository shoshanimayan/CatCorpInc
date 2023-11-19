using UnityEngine;
using Core;
using Zenject;
using UniRx;
using System;
using System.Collections;
using System.Collections.Generic;
using Signals.Core;
using Managers;
using Signals.Game;

namespace Player
{
	public class PlayerControllerMediator: MediatorBase<PlayerControllerView>, IInitializable, IDisposable
	{

		///  INSPECTOR VARIABLES       ///

		///  PRIVATE VARIABLES         ///

		///  PRIVATE METHODS           ///

		///  LISTNER METHODS           ///
		private void OnStateChanged(State state)
		{
			Debug.Log(state);
			switch (state)
			{
				case State.Play:
                    Cursor.lockState = CursorLockMode.Locked;
                    break;
				case State.Text:
                    Cursor.lockState = CursorLockMode.Confined;
                    break;
				case State.Paused:
                    Cursor.lockState = CursorLockMode.Confined;
                    break;
			}
		}
		///  PUBLIC API                ///
		public void ChangeWalkState(WalkState state)
		{
			_signalBus.Fire(new WalkStateChangedSignal() { ToState = state });
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
