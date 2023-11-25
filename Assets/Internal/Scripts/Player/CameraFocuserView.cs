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

        ///  PRIVATE VARIABLES         ///
        private CinemachineVirtualCamera _camera;
        ///  PRIVATE METHODS           ///
         private void Awake()
         {
            _camera = GetComponent<CinemachineVirtualCamera>();
         }
        ///  PUBLIC API                ///

        public void FocusCamera(Transform focus)
        {
            _camera.LookAt = focus;
        }

        public void UnfocusCamera()
        {
            if (_camera.LookAt != null) {
                _camera.LookAt = null;
            }
        }


    }
}
