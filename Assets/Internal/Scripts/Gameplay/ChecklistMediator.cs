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
using Managers;

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

                _objectiveCount++;
                _view.RemoveObjective(obj);
				if(_objectiveCount==_view.TotalObjectives)
				{
                    _signalBus.Fire(new SendStorageSignal() {  });

                }
                _signalBus.Fire(new ObjectiveCompletedSignal() { Objective = obj });
                _signalBus.Fire(new PlayOneShotSignal() { ClipName = "notification"});

            }

        }

		private void CheckForCompletion()
        {

            if (_objectiveCount==_view.TotalObjectives && !_gameSettings.GetEnded())
			{
				_gameSettings.SetEnded(true);
                _signalBus.Fire(new EndingGameSignal() { });

            }
        }

		private void AddObjective(Objective obj)
		{ 
			_view.AddObjective(obj);
		}

        private void EndingGame()
        {
			

            float percent = (_objectiveCount/_view.TotalObjectives);

            _signalBus.Fire(new CompletionPercentageSignal() { CompletionPercent=percent});
        }
        ///  PUBLIC API                ///
        public void InitializeObjectiveMenu(Objective[] objectives)
		{
            _signalBus.Fire(new ObjectiveListSignal() { Objectives = objectives });
        }
		///  IMPLEMENTATION            ///

		[Inject]

		private SignalBus _signalBus;
        [Inject]
        GameSettings _gameSettings;

        readonly CompositeDisposable _disposables = new CompositeDisposable();

		public void Initialize()
		{
			_view.Initilize(this);
            _signalBus.GetStream<ObjectiveCompleteSignal>()
             .Subscribe(x => OnObjectiveCompleted(x.Objective)).AddTo(_disposables);
            _signalBus.GetStream<AddObjectiveSignal>()
                .Subscribe(x => AddObjective(x.Objective)).AddTo(_disposables);
            _signalBus.GetStream<ChecklistCompletionCheckSignal>()
             .Subscribe(x => CheckForCompletion()).AddTo(_disposables);
            _signalBus.GetStream<EndingGameSignal>()
			 .Subscribe(x => EndingGame()).AddTo(_disposables);

        }

		public void Dispose()
		{

			_disposables.Dispose();

		}

	}
}
