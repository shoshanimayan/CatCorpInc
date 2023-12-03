using UnityEngine;
using Core;
using Zenject;
using UniRx;
using System;
using System.Collections;
using System.Collections.Generic;
using Signals.Core;
using Managers;
using Gameplay;
using Signals.Game;

namespace Ui
{
	public class DraggableMediator: MediatorBase<DraggableView>, IInitializable, IDisposable
	{

        ///  INSPECTOR VARIABLES       ///

        ///  PRIVATE VARIABLES         ///
        private ReadState _currentState;
        private bool _completed;
        ///  PRIVATE METHODS           ///

        ///  LISTNER METHODS           ///
        private void OnStateChanged(ReadState state)
        {
            _currentState = state;

        }
        ///  PUBLIC API                ///
        public bool CanDrag { get { return _currentState == ReadState.Drag; } }
        public void Complete()
        {
            if (!_completed)
            { 
            _completed = true;
                _signalBus.Fire(new FinishDragSignal());
            }
        }
        ///  IMPLEMENTATION            ///

        [Inject]

		private SignalBus _signalBus;

		readonly CompositeDisposable _disposables = new CompositeDisposable();

		public void Initialize()
		{
            _view.Init(this);
            _signalBus.GetStream<ChangeReadStateSignal>()
          .Subscribe(x => OnStateChanged(x.ReadState)).AddTo(_disposables);
        }

		public void Dispose()
		{

			_disposables.Dispose();

		}

	}
}
