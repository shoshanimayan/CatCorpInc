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

namespace Ui
{
	public class ChecklistUIMediator: MediatorBase<ChecklistUIView>, IInitializable, IDisposable
	{

		///  INSPECTOR VARIABLES       ///

		///  PRIVATE VARIABLES         ///

		///  PRIVATE METHODS           ///

		///  LISTNER METHODS           ///
		private void OnRecieveObjectives(Objective[] objectives)
		{
			_view.SetObjectives(objectives);		
		}

		private void OnObjectiveCompleted(Objective objective)
		{
			_view.completeObjectiveUI(objective);
		}

        private void OnObjectiveCountUpdated(Objective objective, int total, int current)
        {
			

            _view.UpdateObjectiveCountUI(objective, total, current);

        }

        public void AddObjective(Objective objective)
        {
            _view.AddObjective(objective);


        }
        ///  PUBLIC API                ///

        ///  IMPLEMENTATION            ///

        [Inject]

		private SignalBus _signalBus;

		readonly CompositeDisposable _disposables = new CompositeDisposable();

		public void Initialize()
		{
            _signalBus.GetStream<ObjectiveListSignal>()
            .Subscribe(x => OnRecieveObjectives(x.Objectives)).AddTo(_disposables);
            _signalBus.GetStream<ObjectiveCompletedSignal>()
            .Subscribe(x => OnObjectiveCompleted(x.Objective)).AddTo(_disposables);
            _signalBus.GetStream<UpdateObjectiveCountSignal>()
            .Subscribe(x => OnObjectiveCountUpdated(x.Objective, x.total, x.current)).AddTo(_disposables);
            _signalBus.GetStream<AddObjectiveSignal>()
                .Subscribe(x => AddObjective(x.Objective)).AddTo(_disposables);
          
        }

		public void Dispose()
		{

			_disposables.Dispose();

		}

	}
}
