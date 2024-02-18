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

namespace Ui
{
	public class GameUIManagerMediator: MediatorBase<GameUIManagerView>, IInitializable, IDisposable
	{

        ///  INSPECTOR VARIABLES       ///

        ///  PRIVATE VARIABLES         ///

        ///  PRIVATE METHODS           ///

        ///  LISTNER METHODS           ///
        private void OnStateChanged(State state)
        {

            _view.SetCanvas(state);
        }

        private void OnEndedGame()
        {
            _view.OnEnd();
        }
        ///  PUBLIC API                ///
       
        ///  IMPLEMENTATION            ///

        [Inject]

		private SignalBus _signalBus;

        readonly CompositeDisposable _disposables = new CompositeDisposable();

		public void Initialize()
		{
            _view.Init(this);
        
            _signalBus.GetStream<StateChangeSignal>()
                       .Subscribe(x => OnStateChanged(x.ToState)).AddTo(_disposables);
              _signalBus.GetStream<EndedGameSignal>()
                      .Subscribe(x => OnEndedGame()).AddTo(_disposables);
        }

        public void Dispose()
		{

			_disposables.Dispose();

		}

	}
}
