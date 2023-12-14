using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using FMODUnity;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/AudioLibrary", order = 1)]

    public class AudioLibrary : ScriptableObject
    {
        ///  INSPECTOR VARIABLES       ///

        [Serializable]
        public class AudioDictionaryEntry
        {
            public string key;
            public EventReference value;
        }

        [SerializeField]
        private List<AudioDictionaryEntry> inspectorDictionary;
        ///  PRIVATE VARIABLES         ///

        private Dictionary<string, EventReference> myDictionary;

        ///  PRIVATE METHODS           ///
        private void Awake()
        {
            myDictionary = new Dictionary<string, EventReference>();
            foreach (AudioDictionaryEntry entry in inspectorDictionary)
            {
                myDictionary.Add(entry.key, entry.value);
            }
        }

        ///  PUBLIC API                ///

        

        public bool TryGetClip(string key, out EventReference reference) {
            reference= new EventReference();
            if (myDictionary == null)
            {
                myDictionary = new Dictionary<string, EventReference>();
                foreach (AudioDictionaryEntry entry in inspectorDictionary)
                {
                    myDictionary.Add(entry.key, entry.value);
                }
            }
            if (myDictionary.ContainsKey(key))
            {
                reference = myDictionary[key];
                return true;
            }
            return false;
        }

    }
}
