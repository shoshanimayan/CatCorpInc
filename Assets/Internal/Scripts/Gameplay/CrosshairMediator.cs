using UnityEngine;
using Core;
using Zenject;
using UniRx;
using System;
using System.Collections;
using System.Collections.Generic;
using Signals.Core;
using Signals.Game;

namespace Gameplay
{
	public class CrosshairMediator: MediatorBase<CrosshairView>, IInitializable, IDisposable
	{

		///  INSPECTOR VARIABLES       ///

		///  PRIVATE VARIABLES         ///

		///  PRIVATE METHODS           ///
		
		///  LISTNER METHODS           ///
		private void OnHovering(string name)
		{
			if (name == "")
			{
				_view.SetLabelText("");
				_view.Hovering(false);
			}
			else {
				_view.Hovering(true);
                _view.SetLabelText("[ "+name+" ]");

            }
        }
		///  PUBLIC API                ///

		///  IMPLEMENTATION            ///

		[Inject]

		private SignalBus _signalBus;

		readonly CompositeDisposable _disposables = new CompositeDisposable();

		public void Initialize()
		{

            _signalBus.GetStream<HoveringSignal>()
                          .Subscribe(x => OnHovering(x.Hovering)).AddTo(_disposables);
        }

		public void Dispose()
		{

			_disposables.Dispose();

		}

	}
}
