using UnityEngine;
using Core;
using System.Collections;
using System.Collections.Generic;
namespace Gameplay
{
	public class CoffeeManagerView: MonoBehaviour,IView
	{

		///  INSPECTOR VARIABLES       ///
		[SerializeField] private Objective _objective;
		///  PRIVATE VARIABLES         ///

		///  PRIVATE METHODS           ///

		///  PUBLIC API                ///
		public Objective Objective { get { return _objective; } }
	}
}
