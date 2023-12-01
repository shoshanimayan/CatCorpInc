using UnityEngine;
using Core;
using Zenject;
using UniRx;
using System;
using System.Collections;
using System.Collections.Generic;
using Signals.Game;
using Gameplay;

namespace Ui
{
	public class TextDispalyMediator: MediatorBase<TextDispalyView>, IInitializable, IDisposable
	{

        ///  INSPECTOR VARIABLES       ///

        ///  PRIVATE VARIABLES         ///

        ///  PRIVATE METHODS           ///
       
        ///  LISTNER METHODS           ///
        private void OnRecievedText(TextObject text)
		{ 
			_view.SetName(text.Name);
			_view.SetText(text.BodyText);
		}

        private void HandlePageTransition()
        {

            if (_view.GetCurrentPage()<=_view.GetTotalPages())
            {
                _view.IncrementPage();
            }
            else
            {
                _view.ClearToken();
                _signalBus.Fire(new FinishStepSignal());
            }

        }
        ///  PUBLIC API                ///
        public void ForceEnd()
        {
            _view.ClearToken();
            _signalBus.Fire(new FinishStepSignal());
        }
        ///  IMPLEMENTATION            ///

        [Inject]

		private SignalBus _signalBus;

		readonly CompositeDisposable _disposables = new CompositeDisposable();

		public void Initialize()
		{
            _view.Initilization(this);
            _signalBus.GetStream<SendTextSignal>()
           .Subscribe(x => OnRecievedText(x.Text)).AddTo(_disposables);
            _signalBus.GetStream<ProgressReaderSignal>()
          .Subscribe(x => HandlePageTransition()).AddTo(_disposables);
        }

		public void Dispose()
		{

			_disposables.Dispose();

		}

	}
}
