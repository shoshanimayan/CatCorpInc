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

namespace Gameplay
{
	public class CoffeeParticleMediator: MediatorBase<CoffeeParticleView>, IInitializable, IDisposable
	{

		///  INSPECTOR VARIABLES       ///

		///  PRIVATE VARIABLES         ///

		///  PRIVATE METHODS           ///

		///  LISTNER METHODS           ///
		public void OnCoffeeHit(Vector3 pos)
		{ 
		_view.SetAndPlay(pos);
		}

		public void OnStateChange(State state)
		{
			if (state == State.Text)
			{ 
				_view.KillParticle();
			}
		}
		///  PUBLIC API                ///

		///  IMPLEMENTATION            ///

		[Inject]

		private SignalBus _signalBus;

		readonly CompositeDisposable _disposables = new CompositeDisposable();

		public void Initialize()
		{
            _signalBus.GetStream<StateChangeSignal>()
                      .Subscribe(x => OnStateChange(x.ToState)).AddTo(_disposables);
			_signalBus.GetStream<SendCoffeePositionSignal>()
                         .Subscribe(x => OnCoffeeHit(x.Position)).AddTo(_disposables);
        }

		public void Dispose()
		{

			_disposables.Dispose();

		}

	}
}
