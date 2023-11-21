using UnityEngine;
using Core;
using Zenject;
using UniRx;
using System;
using System.Collections;
using System.Collections.Generic;
using Signals.Game;
using Signals.Core;
using System.Linq;

namespace Gameplay
{
	public class ChecklistMediator: MediatorBase<ChecklistView>, IInitializable, IDisposable
	{

		///  INSPECTOR VARIABLES       ///

		///  PRIVATE VARIABLES         ///
		private int _objectiveCount = 0;
		///  PRIVATE METHODS           ///

		///  LISTNER METHODS           ///
		private void OnObjectiveCompleted( Objective obj)
		{

			if (_view.GetObjectives().Contains(obj))
			{
				_objectiveCount--;
				_view.RemoveObjective(obj);
				_signalBus.Fire(new ObjectiveCompletedSignal() { Objective = obj });
			}
		
		}

		private void CheckForCompletion()
		{
			if (_objectiveCount==0)
			{
				Debug.Log("win");
			}
		}
		///  PUBLIC API                ///

		///  IMPLEMENTATION            ///

		[Inject]

		private SignalBus _signalBus;

		readonly CompositeDisposable _disposables = new CompositeDisposable();

		public void Initialize()
		{
			_objectiveCount=_view.GetObjectives().Length;
            _signalBus.GetStream<ObjectiveCompleteSignal>()
             .Subscribe(x => OnObjectiveCompleted(x.Objective)).AddTo(_disposables);
            _signalBus.GetStream<ChecklistCompletionCheck>()
             .Subscribe(x => CheckForCompletion()).AddTo(_disposables);
        }

		public void Dispose()
		{

			_disposables.Dispose();

		}

	}
}
