using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

namespace ScriptableObjects
{
    [CreateAssetMenu]
    public class TextEntry : ScriptableObject
    {

        ///  INSPECTOR VARIABLES       ///
        [SerializeField] private TextAsset _json;
        [SerializeField] private bool _startNextEntryOnCompletion;
        [SerializeField] private bool _completeGoalOnCompletion;
        ///  PRIVATE VARIABLES         ///

        ///  PRIVATE METHODS           ///

        ///  LISTNER METHODS           ///

        ///  PUBLIC API   
        public Objective Objective;

        public TextAsset Json { get { return _json; } }
        public bool StartNextEntryOnCompletion { get { return _startNextEntryOnCompletion; } }
        public bool CompleteGoalOnCompletion { get { return _completeGoalOnCompletion; } }

        public TextEntry(TextAsset json, bool startNextEntryOnCompletion, bool completeGoalOnCompletion, Objective objective)
        { 
            _json = json;
            _startNextEntryOnCompletion = startNextEntryOnCompletion;
            _completeGoalOnCompletion = completeGoalOnCompletion;
            Objective = objective;
        }

        ///  IMPLEMENTATION            ///
#if UNITY_EDITOR
        [CustomEditor(typeof(TextEntry))]
        class MyClassEditor : Editor
        {
            public override void OnInspectorGUI()
            {
                TextEntry self = (TextEntry)target;
                serializedObject.Update();
                if (self.CompleteGoalOnCompletion)
                    DrawDefaultInspector();
                else
                {
                    DrawPropertiesExcluding(serializedObject, "Objective");
                }
                serializedObject.ApplyModifiedProperties();
            }
        }
#endif


    }


}
