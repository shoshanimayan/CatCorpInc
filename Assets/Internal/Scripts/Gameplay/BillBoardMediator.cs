using UnityEngine;
using Core;
using Zenject;
using UniRx;
using System;
using System.Collections;
using System.Collections.Generic;
using Signals.Core;
using ScriptableObjects;
using NPC;
using Signals.Game;

namespace Gameplay
{
	public class BillBoardMediator: MediatorBase<BillBoardView>, IInitializable, IDisposable
	{

        ///  INSPECTOR VARIABLES       ///

        ///  PRIVATE VARIABLES         ///

        ///  PRIVATE METHODS           ///

        ///  LISTNER METHODS           ///
        private void UnblockStep(TextStep step)
        {
            if (_view.IsStepBlocked() && _view.CheckForMatchingStep(step))
            {
                _view.ForceIncrementStep();
            }
        }


        public void SetString(string text)
        { 
            _view.SetText(text);
            _view.EnableBillBoard(true);
        }

        ///  PUBLIC API                ///
        public void SendStep(TextStep step, Interactable view, Transform transform = null)
        {
            _signalBus.Fire(new SendTextStepSignal() { TextStep = step, Origin = view });
            if (transform != null)
            {
                _signalBus.Fire(new CameraFocusSignal() { Focus = transform });
            }

        }
        ///  IMPLEMENTATION            ///

        [Inject]

		private SignalBus _signalBus;

		readonly CompositeDisposable _disposables = new CompositeDisposable();

		public void Initialize()
		{
            _view.Init(this);
		}

		public void Dispose()
		{
            _signalBus.GetStream<UnblockedConversationSignal>()
            .Subscribe(x => UnblockStep(x.Unblock)).AddTo(_disposables);
            _signalBus.GetStream<SendTypedMessageSignal>()
             .Subscribe(x=>SetString(x.Message)).AddTo(_disposables);
            _disposables.Dispose();

		}

	}
}
