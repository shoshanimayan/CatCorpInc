using UnityEngine;
using Core;
using System.Collections;
using System.Collections.Generic;
namespace Gameplay
{
    public class DoorView : MonoBehaviour, IView
    {

        ///  INSPECTOR VARIABLES       ///
        [SerializeField] private float _closedY;
        [SerializeField] private float _openY;
        [SerializeField] private bool _openDoor;
        [SerializeField] private int _eventKey;

        ///  PRIVATE VARIABLES         ///

        ///  PRIVATE METHODS           ///
        private void Start()
        {
            DoorState = _openDoor;
        }
        ///  PUBLIC API                ///

        public bool DoorState
        {
            get { return _openDoor; }
            set {
                if (_openDoor == value) { return; }
                _openDoor = value;
                if (_openDoor)
                {
                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, _openY, transform.eulerAngles.z);
                }
                else
                {
                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, _closedY, transform.eulerAngles.z);

                }

            } 
        }

        public bool CheckEvent(int key)
        {
           
            return key == _eventKey;
        }
    }
}
