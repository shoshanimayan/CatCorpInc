using UnityEngine;
using Core;
using Zenject;
using UniRx;
using System;
using System.Collections;
using System.Collections.Generic;
using Signals.Core;
using Signals.Game;
using UnityEngine.Analytics;

namespace Gameplay
{
	public class CrosshairMediator: MediatorBase<CrosshairView>, IInitializable, IDisposable
	{

		///  INSPECTOR VARIABLES       ///

		///  PRIVATE VARIABLES         ///

		///  PRIVATE METHODS           ///
		
		///  LISTNER METHODS           ///
		private void OnHovering(string name,bool dontShowInteract)
		{
			if (name == "")
			{
				_view.SetLabelText("");
				_view.Hovering(false);
			}
			else {
				_view.Hovering(true);
                _view.SetLabelText("[ "+name+" ]"+(!dontShowInteract?"\n Press E to interact":""));

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
                          .Subscribe(x => OnHovering(x.Hovering,x.DontShowInteract)).AddTo(_disposables);
            _signalBus.GetStream<EndedGameSignal>()
            .Subscribe(x => {
                  _view.DisableCrosshair();
              }).AddTo(_disposables);
        }

		public void Dispose()
		{

			_disposables.Dispose();

		}

	}
}
