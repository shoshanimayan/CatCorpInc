using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Gameplay
{
	public interface Interactable
	{

		///  INSPECTOR VARIABLES       ///

		///  PRIVATE VARIABLES         ///

		///  PRIVATE METHODS           ///

		///  LISTNER METHODS           ///

		///  PUBLIC API                ///
		public void DoInteraction();


		public void HoverOn();

        public GameObject gameObject { get; }

        ///  IMPLEMENTATION            ///

    }
}
