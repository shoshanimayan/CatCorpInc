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
	public class DragPanelMediator: MediatorBase<DragPanelView>, IInitializable, IDisposable
	{

        ///  INSPECTOR VARIABLES       ///

        ///  PRIVATE VARIABLES         ///

        ///  PRIVATE METHODS           ///

        ///  LISTNER METHODS           ///

        private void SetDragLetter(string prompt)
        {
            _view.SetLetterLabel(prompt);
        }

        ///  PUBLIC API                ///
        


        ///  IMPLEMENTATION            ///

        [Inject]

        private SignalBus _signalBus;

        readonly CompositeDisposable _disposables = new CompositeDisposable();

        public void Initialize()
        {
            _view.Init(this);
            _signalBus.GetStream<SetDragSignal>()
                    .Subscribe(x => SetDragLetter(x.Message)).AddTo(_disposables);
        }

        public void Dispose()
        {

            _disposables.Dispose();

        }

    }
}
