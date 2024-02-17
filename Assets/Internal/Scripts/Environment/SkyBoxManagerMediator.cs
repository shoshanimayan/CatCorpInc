using UnityEngine;
using Core;
using Zenject;
using UniRx;
using System;
using System.Collections;
using System.Collections.Generic;
using Signals.Core;

namespace Environment
{
	public class SkyBoxManagerMediator: MediatorBase<SkyBoxManagerView>, IInitializable, IDisposable
	{

		///  INSPECTOR VARIABLES       ///

		///  PRIVATE VARIABLES         ///

		///  PRIVATE METHODS           ///
		private void SetSkyBox(Material skyboxMaterial)
		{
			RenderSettings.skybox = skyboxMaterial;

        }
		///  LISTNER METHODS           ///

		private void OnGameEnding()
		{
            SetSkyBox(_view.NightSkyBox);
            SetAmbientLight(_view.NightLightColor);

        }
        private void SetAmbientLight(Color color)
		{
			RenderSettings.ambientLight = color;

        }

        ///  PUBLIC API                ///

        ///  IMPLEMENTATION            ///

        [Inject]

		private SignalBus _signalBus;

		readonly CompositeDisposable _disposables = new CompositeDisposable();

		public void Initialize()
		{
            _signalBus.GetStream<EndingGameSignal>()
			.Subscribe(x => OnGameEnding()).AddTo(_disposables);
            SetSkyBox(_view.DaySkyBox);
			SetAmbientLight(_view.DayLightColor);
		}

		public void Dispose()
		{

			_disposables.Dispose();

		}

	}
}
