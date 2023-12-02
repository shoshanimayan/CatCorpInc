using UnityEngine;
using Core;
using Zenject;
using UniRx;
using System;
using System.Collections;
using System.Collections.Generic;
using Signals.Game;

namespace Ui
{
	public class TextTypeMediator: MediatorBase<TextTypeView>, IInitializable, IDisposable
	{

		///  INSPECTOR VARIABLES       ///

		///  PRIVATE VARIABLES         ///

		///  PRIVATE METHODS           ///

		///  LISTNER METHODS           ///

		private void SetTypingPrompt(string prompt)
		{
			_view.SetPromptLabel(prompt);
		}

		///  PUBLIC API                ///
		public void SendTypedMessage(string message)
		{ 
			_signalBus.Fire(new SendTypedMessageSignal() { Message=message });
		}


		///  IMPLEMENTATION            ///

		[Inject]

		private SignalBus _signalBus;

		readonly CompositeDisposable _disposables = new CompositeDisposable();

		public void Initialize()
		{
			_view.Init(this);
            _signalBus.GetStream<SetTypingSignal>()
                    .Subscribe(x => SetTypingPrompt(x.Prompt)).AddTo(_disposables);
        }

		public void Dispose()
		{

			_disposables.Dispose();

		}

	}
}
