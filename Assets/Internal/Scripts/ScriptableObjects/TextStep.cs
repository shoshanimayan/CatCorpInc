using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using Signals.Core;

namespace ScriptableObjects
{
    [CreateAssetMenu]
    public class TextStep : ScriptableObject
    {

        ///  INSPECTOR VARIABLES       ///
        [SerializeField] private TextAsset _json;
        [SerializeField] private bool _startNextEntryOnCompletion;
        [SerializeField] private bool _completeGoalOnCompletion;
        [SerializeField] private bool _needsCollectable;
        [SerializeField] private bool _waitingForAnotherConversation;
        [SerializeField] private bool _unblocksConversation;


        ///  PRIVATE VARIABLES         ///
        private TextStep _nextStep;
        ///  PRIVATE METHODS           ///

        ///  LISTNER METHODS           ///

        ///  PUBLIC API   
        public void SetNextStep(TextStep nextStep)
        {   
            _nextStep = nextStep;
        }

        public TextStep GetNextStep()
        { 
            return _nextStep;
        }

        private void OnValidate()
        {
            if (!_completeGoalOnCompletion)
            {
                Objective = null;
            
            }
        }


        public Objective Objective;
        

        public TextAsset Json { get { return _json; } }
        public bool StartNextEntryOnCompletion { get { return _startNextEntryOnCompletion; }  
            set { _startNextEntryOnCompletion = value; } }
        public bool CompleteGoalOnCompletion { get { return _completeGoalOnCompletion; } }

        public TextStep(TextAsset json, bool startNextEntryOnCompletion, bool completeGoalOnCompletion, Objective objective)
        { 
            _json = json;
            _startNextEntryOnCompletion = startNextEntryOnCompletion;
            _completeGoalOnCompletion = completeGoalOnCompletion;
            Objective = objective;
        }

        public bool NeedsCollectable { get { return _needsCollectable; } }
        public bool WaitingForAnotherConversation { get { return _waitingForAnotherConversation; } }
        public bool UnblocksConversation { get { return _unblocksConversation; } }

        ///  IMPLEMENTATION            ///
#if UNITY_EDITOR
        [CustomEditor(typeof(TextStep))]
        class MyClassEditor : Editor
        {
            public override void OnInspectorGUI()
            {
                TextStep self = (TextStep)target;
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
