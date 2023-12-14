using UnityEngine;
using Core;
using Zenject;
using UniRx;
using System;
using System.Collections;
using System.Collections.Generic;
using Signals.Core;

namespace Gameplay
{
	public class InteractableMediator: MediatorBase<InteractableView>, IInitializable, IDisposable
	{

		///  INSPECTOR VARIABLES       ///

		///  PRIVATE VARIABLES         ///

		///  PRIVATE METHODS           ///

		///  LISTNER METHODS           ///

		///  PUBLIC API                ///
		public void CompleteObjective(Objective objective) {
			_signalBus.Fire(new ObjectiveCompleteSignal() { Objective = objective });
		}

		public void PlaySound(Vector3 worldPos)
		{
			_signalBus.Fire(new PlayOneShotSignal() { ClipName="interacted", WorldPos=	worldPos  });
		}

		///  IMPLEMENTATION            ///

		[Inject]

		private SignalBus _signalBus;

		readonly CompositeDisposable _disposables = new CompositeDisposable();

		public void Initialize()
		{
			_view.Initializer(this);
		}

		public void Dispose()
		{

			_disposables.Dispose();

		}

	}
}
