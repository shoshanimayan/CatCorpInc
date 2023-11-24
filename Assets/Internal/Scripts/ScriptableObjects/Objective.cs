using UnityEngine;
using System.Collections;
using System.Collections.Generic;

    [CreateAssetMenu]
    public class Objective: ScriptableObject
	{

		///  INSPECTOR VARIABLES       ///
		[SerializeField] private string _name;
		[SerializeField] private string _description;
		///  PRIVATE VARIABLES         ///

		///  PRIVATE METHODS           ///

		///  LISTNER METHODS           ///

		///  PUBLIC API   
		public string Name { get { return _name; } }
		public string Description { get { return _description; } }

		public Objective( string name, string description) {
			_name = name;
			_description = description;
		}

		///  IMPLEMENTATION            ///
		///  



	}

