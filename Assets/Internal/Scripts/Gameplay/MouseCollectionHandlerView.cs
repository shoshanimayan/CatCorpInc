using UnityEngine;
using Core;
using System.Collections;
using System.Collections.Generic;
namespace Gameplay
{
	public class MouseCollectionHandlerView: MonoBehaviour,IView
	{

		///  INSPECTOR VARIABLES       ///
		[SerializeField] int _mouseCount;
		[SerializeField] int _key;
		[SerializeField] Objective _objective;
		///  PRIVATE VARIABLES         ///

		///  PRIVATE METHODS           ///

		///  PUBLIC API                ///
		public bool CheckKey(int compKey) { return compKey == _key; }
		public bool MouseCountCheck(int currentCount) { return _mouseCount <= currentCount; }

		public int GetTotal() { return _mouseCount; }

		public Objective GetObjective() { return _objective; }
	}
}
