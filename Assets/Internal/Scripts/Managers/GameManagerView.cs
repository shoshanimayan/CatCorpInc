using UnityEngine;
using Core;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using Cinemachine;

namespace Managers
{
	public class GameManagerView: MonoBehaviour,IView
	{

		///  INSPECTOR VARIABLES       ///
		[SerializeField] private TextStep _intro;
		[SerializeField] private TextStep _outro;
        [SerializeField] private CinemachineVirtualCamera _cam;

        ///  PRIVATE VARIABLES         ///
        private GameManagerMediator _mediator;
        ///  PRIVATE METHODS           ///
        private void Start()
        {
            _mediator.StartIntro(_intro);

        }
        ///  PUBLIC API                ///
        public void Init(GameManagerMediator mediator)
		{ 
			_mediator = mediator;
		}

		public TextStep Outro { get { return _outro; } }

	}
}
