using UnityEngine;
using Core;
using Zenject;
using UniRx;
using System;
using System.Collections;
using System.Collections.Generic;
using Signals.Core;
using Signals.Game;


namespace Ui
{
	public class TextChoiceMediator: MediatorBase<TextChoiceView>, IInitializable, IDisposable
	{

		///  INSPECTOR VARIABLES       ///

		///  PRIVATE VARIABLES         ///

		///  PRIVATE METHODS           ///
		private void OnRecievedChoices(string[] choices)
		{ 
			_view.SetChoices(choices);
		}
		///  LISTNER METHODS           ///

		///  PUBLIC API                ///
		public void SendChoice(int choice)
		{
			Debug.Log(choice);
			_signalBus.Fire(new ChoiceSendSignal() { Choice = choice });
		}
		///  IMPLEMENTATION            ///

		[Inject]

		private SignalBus _signalBus;

		readonly CompositeDisposable _disposables = new CompositeDisposable();

		public void Initialize()
		{
			_view.Init(this);
            _signalBus.GetStream<ChoiceListSignal>()
                          .Subscribe(x => OnRecievedChoices(x.Choices)).AddTo(_disposables);
        }

		public void Dispose()
		{

			_disposables.Dispose();

		}

	}
}
