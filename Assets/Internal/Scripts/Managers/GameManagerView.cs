using UnityEngine;
using Core;
using System.Collections;
using System.Collections.Generic;
namespace Managers
{
	public class GameManagerView: MonoBehaviour,IView
	{

		///  INSPECTOR VARIABLES       ///
		[SerializeField] private TextAsset _intro; 
		///  PRIVATE VARIABLES         ///
		private GameManagerMediator _mediator;
		///  PRIVATE METHODS           ///

		///  PUBLIC API                ///
		public void Init(GameManagerMediator mediator)
		{ 
			_mediator = mediator;
			_mediator.StartIntro(_intro);
		}
	}
}
