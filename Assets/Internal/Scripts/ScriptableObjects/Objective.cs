using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEditor;

    [CreateAssetMenu]
    public class Objective: ScriptableObject
	{

		///  INSPECTOR VARIABLES       ///
		[SerializeField] private string _name;
		[SerializeField] private string _description;
    [SerializeField] private bool _hasCount;

    ///  PRIVATE VARIABLES         ///

    ///  PRIVATE METHODS           ///

    ///  LISTNER METHODS           ///

    ///  PUBLIC API   
    public string Name { get { return _name; } }
		public string Description { get { return _description; } }
    public bool HasCount { get { return _hasCount; } }

    public int Total;
    [HideInInspector]
    public int CurrentCount;
    public Objective( string name, string description, bool hasCount) {
			_name = name;
			_description = description;
			_hasCount = hasCount;
		}

    ///  IMPLEMENTATION            ///
    ///  
#if UNITY_EDITOR
    [CustomEditor(typeof(Objective))]
    class MyClassEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            Objective self = (Objective)target;
            serializedObject.Update();
            List<string> exclude = new List<string>();
            if (!self.HasCount)
            {
                exclude.Add("Total");

            }

            DrawPropertiesExcluding(serializedObject, exclude.ToArray());

            serializedObject.ApplyModifiedProperties();
        }
    }
#endif



}

