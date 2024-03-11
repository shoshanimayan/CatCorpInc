using UnityEngine;
using Core;
using Zenject;
using UniRx;
using System;
using System.Collections;
using System.Collections.Generic;
using Signals.Core;
using Managers;
using Gameplay;
using Signals.Game;
using UnityEditor;
using Player;

namespace Ui
{
	public class DraggableMediator: MediatorBase<DraggableView>, IInitializable, IDisposable
	{

        ///  INSPECTOR VARIABLES       ///

        ///  PRIVATE VARIABLES         ///
        private ReadState _currentState;
        private bool _completed;
        private bool _playingAudio;
        ///  PRIVATE METHODS           ///

        ///  LISTNER METHODS           ///
        private void OnStateChanged(ReadState state)
        {
            _currentState = state;

        }
        ///  PUBLIC API                ///
        public bool CanDrag { get { return _currentState == ReadState.Drag; } }
        public void Complete()
        {
            if (!_completed)
            { 
                _completed = true;
                StopShredAudio();
                _signalBus.Fire(new FinishDragSignal());
                _signalBus.Fire(new WalkStateChangedSignal() { ToState = WalkState.None });

            }
        }

        public void PlayShredAudio()
        {
            if (!_playingAudio)
            {
                _playingAudio = true;
                _signalBus.Fire(new PlaySoundSignal() { ClipName = "shred" });
            }
        }

        public void StopShredAudio() 
        {
            _signalBus.Fire(new StopSoundSignal() { ClipName = "shred" });
            _playingAudio = false;

        }

        public void ShakeScreen(bool shake)
        {
            if (shake)
            {
                _signalBus.Fire(new WalkStateChangedSignal() { ToState=WalkState.Shake }) ;
            }
            else
            {
                _signalBus.Fire(new WalkStateChangedSignal() { ToState = WalkState.None });


            }
        }
        ///  IMPLEMENTATION            ///

        [Inject]

		private SignalBus _signalBus;

		readonly CompositeDisposable _disposables = new CompositeDisposable();

		public void Initialize()
		{
            _view.Init(this);
            _signalBus.GetStream<ChangeReadStateSignal>()
          .Subscribe(x => OnStateChanged(x.ReadState)).AddTo(_disposables);
        }

		public void Dispose()
		{

			_disposables.Dispose();

		}

	}
}
