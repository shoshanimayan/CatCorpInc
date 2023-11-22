using UnityEngine;
using Core;
using Zenject;
using UniRx;
using System;
using System.Collections;
using System.Collections.Generic;
using Signals.Game;

namespace Gameplay
{
	public class ReaderMediator: MediatorBase<ReaderView>, IInitializable, IDisposable
	{


		///  INSPECTOR VARIABLES       ///

		///  PRIVATE VARIABLES         ///
		ReadState _readState = ReadState.Text;
		///  PRIVATE METHODS           ///

		///  LISTNER METHODS           ///
		private void OnReadStateChanged(ReadState readstate)
		{ 
		_readState = readstate;
		}

		private void OnReceiveTextAsset(TextAsset textAsset)
		{ 

		}
		///  PUBLIC API                ///
		public ReadState GetState() { return _readState; }
		///  IMPLEMENTATION            ///

		[Inject]

		private SignalBus _signalBus;

		readonly CompositeDisposable _disposables = new CompositeDisposable();

		public void Initialize()
		{
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
