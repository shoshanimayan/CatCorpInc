using UnityEngine;
using Core;
using System.Collections;
using System.Collections.Generic;
namespace Gameplay
{
    public class PlantCollectionHandlerView : MonoBehaviour, IView, ICollectionHandlerView
    {

        ///  INSPECTOR VARIABLES       ///
        [SerializeField] int _plantCount;
        [SerializeField] int _key;
        [SerializeField] Objective _objective;
        ///  PRIVATE VARIABLES         ///

        ///  PRIVATE METHODS           ///

        ///  PUBLIC API                ///
        public bool CheckKey(int compKey) { return compKey == _key; }
        public bool CountCheck(int currentCount) { return _plantCount <= currentCount; }

        public int GetTotal() { return _plantCount; }

        public Objective GetObjective() { return _objective; }

    }
}
