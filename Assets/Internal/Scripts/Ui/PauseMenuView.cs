using UnityEngine;
using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

namespace Ui
{
	public class PauseMenuView: MonoBehaviour,IView
	{

		///  INSPECTOR VARIABLES       ///

		///  PRIVATE VARIABLES         ///
		private PauseMenuMediator _mediator;
		///  PRIVATE METHODS           ///

		///  PUBLIC API                ///
		public void Init(PauseMenuMediator mediator)
		{ 
			_mediator = mediator; 
		}

        public void ExitGame()
        {
            _mediator.PlayClickAudio();
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        }

        public void ToMenu()
        {
            _mediator.ToMenu();
        }

        public void Unpause()
        {
            _mediator.Unpause();
        }
    }
}
