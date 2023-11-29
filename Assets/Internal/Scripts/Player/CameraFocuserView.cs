using UnityEngine;
using Core;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;

namespace Player
{
	public class CameraFocuserView: MonoBehaviour,IView
	{

        ///  INSPECTOR VARIABLES       ///
        [SerializeField] private Transform _tempFocus;

        ///  PRIVATE VARIABLES         ///
        private CinemachineVirtualCamera _virCam;
        private Transform _originalFollow;
        private Camera _camera;
        private Transform _focus=null;
        private Vector3 _tempPos;
        ///  PRIVATE METHODS           ///
         private void Awake()
         {
            _camera=Camera.main;
           _virCam = GetComponent<CinemachineVirtualCamera>();
            _originalFollow = _virCam.Follow;
            _tempPos= _tempFocus.localPosition;

         }
        ///  PUBLIC API                ///

        private void Update()
        {
            if (_focus!=null)
            {
                _originalFollow.transform.LookAt(_focus);
            }
        }

        public Vector3 GetFollow() { return _originalFollow.position+Camera.main.transform.forward; }

        public void FocusCamera(Transform focus)
        {
            
            _focus = focus;
           
        }

        public void FocusCamera(Vector3 focus)
        {
            _tempFocus.position = focus;
            _focus = _tempFocus;

        }

        public void UnfocusCamera()
        {
           _focus = null;
            _tempFocus.position = _tempPos;

        }


    }
}
