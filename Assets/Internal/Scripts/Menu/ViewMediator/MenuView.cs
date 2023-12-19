using UnityEngine;
using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

namespace Menu
{
	public class MenuView: MonoBehaviour,IView
	{

        ///  INSPECTOR VARIABLES       ///

        ///  PRIVATE VARIABLES         ///
        private MenuMediator _mediator;
        ///  PRIVATE METHODS           ///

        ///  PUBLIC API                ///
        public void Init(MenuMediator mediator)
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

        public void PlayGame()
        {
            _mediator.PlayGame();
        }
    }
}
