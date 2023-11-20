using UnityEngine;
using System.Collections;
using System.Collections.Generic;

	[CreateAssetMenu]
	public class Character: ScriptableObject
	{

	///  INSPECTOR VARIABLES       ///
	[SerializeField] private string _name;
		///  PRIVATE VARIABLES         ///

		///  PRIVATE METHODS           ///

		///  LISTNER METHODS           ///

		///  PUBLIC API                ///
		public string Name{ get{return _name;} }
		///  IMPLEMENTATION            ///

	}

