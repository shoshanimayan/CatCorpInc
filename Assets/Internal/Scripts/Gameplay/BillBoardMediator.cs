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
using Managers;
using DG.Tweening;


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


        private void OnRecievedTypedMessage(string message)
        {
            _view.SetText(message);
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
            _signalBus.GetStream<UnblockedConversationSignal>()
            .Subscribe(x => UnblockStep(x.Unblock)).AddTo(_disposables);
            _signalBus.GetStream<SendTypedMessageSignal>()
                       .Subscribe(x => OnRecievedTypedMessage(x.Message)).AddTo(_disposables);
        }

		public void Dispose()
		{
          
            _disposables.Dispose();

		}

	}
}
