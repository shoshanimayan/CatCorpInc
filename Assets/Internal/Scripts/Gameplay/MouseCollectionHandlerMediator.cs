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
	public class MouseCollectionHandlerMediator: MediatorBase<MouseCollectionHandlerView>, IInitializable, IDisposable
	{

        ///  INSPECTOR VARIABLES       ///

        ///  PRIVATE VARIABLES         ///
        private int _currentMouseCount;
        ///  PRIVATE METHODS           ///
        private void CompleteCollection() 
        {
            _signalBus.Fire(new ObjectiveCompletedSignal { Objective=_view.GetObjective()});
        }
        ///  LISTNER METHODS           ///
        private void GotCollectable(int key)
        {
            if (_view.CheckKey(key))
            {
                _currentMouseCount++;
                _signalBus.Fire(new UpdateObjectiveCountSignal() { Objective = _view.GetObjective(), total = _view.GetTotal(), current = _currentMouseCount });

                if (_view.MouseCountCheck(_currentMouseCount))
                {
                    CompleteCollection();            
                }
            }
        }
        ///  PUBLIC API                ///

        ///  IMPLEMENTATION            ///

        [Inject]

		private SignalBus _signalBus;

		readonly CompositeDisposable _disposables = new CompositeDisposable();

		public void Initialize()
		{
            _signalBus.GetStream<GotCollectableSignal>()
            .Subscribe(x => GotCollectable(x.Key)).AddTo(_disposables);
        }

		public void Dispose()
		{

			_disposables.Dispose();

		}

	}
}