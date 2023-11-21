using UnityEngine;
using Core;
using Zenject;
using UniRx;
using System;
using System.Collections;
using System.Collections.Generic;
using Signals.Core;
using Signals.Game;
using Managers;

namespace Ui
{
	public class NotificationMediator: MediatorBase<NotificationView>, IInitializable, IDisposable
	{

		///  INSPECTOR VARIABLES       ///

		///  PRIVATE VARIABLES         ///

		///  PRIVATE METHODS           ///

		///  LISTNER METHODS           ///
		private void OnObjectiveCompleted(Objective obj)
		{
			_view.KillNotification();
			_view.SetText(obj.Name);
			_view.Notify();
		}

        private void OnStateChanged(State state)
        {
			_view.KillNotification();
			if (!_gameSettings.GetEnded())
			{
				CheckForCompletetion();

            }
        }
        ///  PUBLIC API                ///

        public void CheckForCompletetion()
		{
			_signalBus.Fire(new ChecklistCompletionCheck());
		}

		///  IMPLEMENTATION            ///

		[Inject]
		private SignalBus _signalBus;
		[Inject] 
		GameSettings _gameSettings;

		readonly CompositeDisposable _disposables = new CompositeDisposable();

		public void Initialize()
		{
			_view.Initializer(this);
            _signalBus.GetStream<StateChangedSignal>()
                          .Subscribe(x => OnStateChanged(x.ToState)).AddTo(_disposables);
            _signalBus.GetStream<ObjectiveCompletedSignal>()
						.Subscribe(x => OnObjectiveCompleted(x.Objective)).AddTo(_disposables);

        }

		public void Dispose()
		{

			_disposables.Dispose();

		}

	}
}
