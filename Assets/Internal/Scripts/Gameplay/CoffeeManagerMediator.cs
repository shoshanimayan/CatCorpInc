using UnityEngine;
using Core;
using Zenject;
using UniRx;
using System;
using System.Collections;
using System.Collections.Generic;
using Signals.Core;
using Signals.Game;
using TMPro.EditorUtilities;

namespace Gameplay
{
	public class CoffeeManagerMediator: MediatorBase<CoffeeManagerView>, IInitializable, IDisposable
	{

        ///  INSPECTOR VARIABLES       ///

        ///  PRIVATE VARIABLES         ///
        private int _coffeeCount;
		private bool _completed;
		///  PRIVATE METHODS           ///
		private void CheckCoffeeCompletion()
		{
			_signalBus.Fire(new ObjectiveCompleteSignal() { Objective = _view.Objective });
		}
        ///  LISTNER METHODS           ///

        ///  PUBLIC API                ///
        public void GotCoffee() { 
			_coffeeCount--;
			if (_coffeeCount <= 0 && !_completed)
			{ 
				_completed = true;
				CheckCoffeeCompletion();
			}
		}

		public void SetCoffee() { _coffeeCount++; }
		///  IMPLEMENTATION            ///

		[Inject]

		private SignalBus _signalBus;

		readonly CompositeDisposable _disposables = new CompositeDisposable();

		public void Initialize()
		{
			
            _signalBus.GetStream<GotCoffeeSignal>()
                          .Subscribe(x => GotCoffee()).AddTo(_disposables);
            _signalBus.GetStream<SetCoffeeSignal>()
                                  .Subscribe(x => SetCoffee()).AddTo(_disposables);
        }

		public void Dispose()
		{

			_disposables.Dispose();

		}

	}
}
