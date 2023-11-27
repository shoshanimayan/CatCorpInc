using UnityEngine;
using Core;
using Zenject;
using UniRx;
using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using Signals.Game;
using Signals.Core;

namespace NPC
{
	public class NPCMediator: MediatorBase<NPCView>, IInitializable, IDisposable
	{

        ///  INSPECTOR VARIABLES       ///

        ///  PRIVATE VARIABLES         ///
        private bool _gotCoffee;
        ///  PRIVATE METHODS           ///

        ///  LISTNER METHODS           ///
        
        private void GotCollectable(int key )
        {
            if (_view.GetNeedsCollectable() && _view.GetCollectableKey()==key)
            { 
                _view.ForceIncrementStep();
            }
        }

        private void UnblockStep(TextStep step)
        {
            if (_view.IsStepBlocked() &&_view.CheckForMatchingStep(step))
            {
                _view.ForceIncrementStep();
            }
        }

        ///  PUBLIC API                ///
        public void GotCoffee()
        {
            if (!_gotCoffee) {
                _signalBus.Fire(new GotCoffeeSignal());
            }
        }

        public void SetCoffee()
        {
            _signalBus.Fire(new SetCoffeeSignal());

        }

        public void SendStep(TextStep step, NPCView view, Transform transform = null)
        {
            _signalBus.Fire(new SendTextStepSignal() { TextStep = step, Origin=view });
            if (transform != null)
            {
                _signalBus.Fire(new CameraFocusSignal() { Focus= transform });
            }

        }

        public void SendUnblock(TextStep unblock)
        {
            _signalBus.Fire(new UnblockedConversationSignal() {Unblock=unblock});
        }
        ///  IMPLEMENTATION            ///

        [Inject]

		private SignalBus _signalBus;

		readonly CompositeDisposable _disposables = new CompositeDisposable();

		public void Initialize()
		{
            _view.Init(this);
            _signalBus.GetStream<GotCollectableSignal>()
             .Subscribe(x => GotCollectable(x.Key)).AddTo(_disposables);
            _signalBus.GetStream<UnblockedConversationSignal>()
            .Subscribe(x => UnblockStep(x.Unblock)).AddTo(_disposables);
        }

		public void Dispose()
		{

			_disposables.Dispose();

		}

	}
}
