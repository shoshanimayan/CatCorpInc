using UnityEngine;
using Core;
using Zenject;
using UniRx;
using System;
using System.Collections;
using System.Collections.Generic;
using Signals.Core;
using Managers;

namespace Menu
{
	public class MenuMediator : MediatorBase<MenuView>, IInitializable, IDisposable
	{

		///  INSPECTOR VARIABLES       ///

		///  PRIVATE VARIABLES         ///

		///  PRIVATE METHODS           ///

		///  LISTNER METHODS           ///

		///  PUBLIC API                ///
		public void PlayClickAudio()
		{
            _signalBus.Fire(new PlayOneShotSignal() { ClipName = "interacted" });

        }

        public void PlayGame()
		{
			PlayClickAudio();
            _signalBus.Fire(new LoadSceneSignal() { SceneToLoad = SceneState.Game });
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

			_disposables.Dispose();

		}

	}
}
