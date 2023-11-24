using UnityEngine;
using Core;
using Zenject;
using UniRx;
using System;
using System.Collections;
using System.Collections.Generic;
using Signals.Game;
using Signals.Core;
using System.Xml;

namespace Gameplay
{
    [System.Serializable]
    public class TextEntry
	{
		public String Type;
		public String Name;
		public String Content;

	}

    public struct TextObject
    {
        public string Name;
        public string BodyText;

        public TextObject(string name, string bodyText)
        {
            Name = name;
            BodyText = bodyText;
        }

    }

    public class ReaderMediator: MediatorBase<ReaderView>, IInitializable, IDisposable
	{


		///  INSPECTOR VARIABLES       ///

		///  PRIVATE VARIABLES         ///
		private ReadState _readState = ReadState.Null;
		///  PRIVATE METHODS           ///


		
		

		///  LISTNER METHODS           ///
		private void OnReadStateChanged(ReadState readstate)
		{
            Debug.Log(3);

            if (readstate != _readState)
			{
				_readState = readstate;
				_view.SetReadUI(readstate);
			}
		}

		private void OnReceiveTextAsset(TextAsset textAsset)
		{
            Debug.Log(2);

            _signalBus.Fire(new StateChangeSignal() { ToState= Managers.State.Text});
			TextEntry entry = JsonUtility.FromJson<TextEntry>(textAsset.text);
            switch (entry.Type)
            {
                case "Read":
                    _signalBus.Fire(new ChangeReadStateSignal() { ReadState = ReadState.Text });
					_signalBus.Fire(new SendTextSignal() {  Text = new TextObject(entry.Name,entry.Content)});
                    break;
                case "MultipleChoice":
                    _signalBus.Fire(new ChangeReadStateSignal() { ReadState = ReadState.Choice });
					_signalBus.Fire(new ChoiceListSignal() { Choices = entry.Content.Split(",") }) ;
                    break;
            }

        }
		///  PUBLIC API                ///
		public ReadState GetState() { return _readState; }


		public void SendProgressReader()
		{
            _signalBus.Fire(new ProgressReaderSignal());

        }

        ///  IMPLEMENTATION            ///

        [Inject]

		private SignalBus _signalBus;

		readonly CompositeDisposable _disposables = new CompositeDisposable();

		public void Initialize()
		{
			_view.Init(this);
            _signalBus.GetStream<ChangeReadStateSignal>()
                         .Subscribe(x => OnReadStateChanged(x.ReadState)).AddTo(_disposables);
            _signalBus.GetStream<SendTextAssetSignal>()
                         .Subscribe(x => OnReceiveTextAsset(x.TextAsset)).AddTo(_disposables);
        }

		public void Dispose()
		{

			_disposables.Dispose();

		}

	}
}
