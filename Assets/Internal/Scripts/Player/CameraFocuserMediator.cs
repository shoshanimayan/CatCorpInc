using UnityEngine;
using Core;
using Zenject;
using UniRx;
using System;
using System.Collections;
using System.Collections.Generic;
using Signals.Core;
using Managers;

namespace Player
{
	public class CameraFocuserMediator: MediatorBase<CameraFocuserView>, IInitializable, IDisposable
	{

        ///  INSPECTOR VARIABLES       ///

        ///  PRIVATE VARIABLES         ///

        ///  PRIVATE METHODS           ///

        ///  LISTNER METHODS           ///
        private void OnStateChanged(State state)
        {
            if (state != State.Text)
            {

                if (state == State.Paused)
                {
                    _view.FocusCamera(_view.GetFollow());

                }
                else
                {
                    _view.UnfocusCamera();
                }
            }
        }


        private void OnFocusCamera(Transform transform)
        {
            _view.FocusCamera(transform);
        }
        ///  PUBLIC API                ///

        ///  IMPLEMENTATION            ///

        [Inject]

		private SignalBus _signalBus;

		readonly CompositeDisposable _disposables = new CompositeDisposable();

		public void Initialize()
		{
            _signalBus.GetStream<StateChangeSignal>()
                     .Subscribe(x => OnStateChanged(x.ToState)).AddTo(_disposables);
            _signalBus.GetStream<CameraFocusSignal>()
                .Subscribe(x=>OnFocusCamera(x.Focus));
        }

		public void Dispose()
		{

			_disposables.Dispose();

		}

	}
}
