using UnityEngine;
using Core;
using System.Collections;
using System.Collections.Generic;
namespace Ui
{
	public class LoadingUIView: MonoBehaviour,IView
	{

		///  INSPECTOR VARIABLES       ///
		[SerializeField] private Canvas _loadingCanvas;
		///  PRIVATE VARIABLES         ///

		///  PRIVATE METHODS           ///

		///  PUBLIC API                ///
		public void EnableLoadingUI(bool enabled)
		{ 
		_loadingCanvas.enabled = enabled;
		}

	}
}
