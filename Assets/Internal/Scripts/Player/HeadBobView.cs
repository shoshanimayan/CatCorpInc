using UnityEngine;
using Core;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;

namespace Player
{
	public class HeadBobView: MonoBehaviour,IView
	{

		///  INSPECTOR VARIABLES       ///
		[SerializeField] CinemachineBasicMultiChannelPerlin _camera;
		[SerializeField] NoiseSettings _noiseSettingsWalk;
        [SerializeField] NoiseSettings _noiseSettingsRun;


        ///  PRIVATE VARIABLES         ///

        ///  PRIVATE METHODS           ///

        ///  PUBLIC API                ///
        ///  
        public void SetWalk()
        {
            _camera.enabled = true;
            _camera.m_NoiseProfile = _noiseSettingsWalk;
        }
        public void SetRun() {
            _camera.enabled = true;
            _camera.m_NoiseProfile = _noiseSettingsRun;
        }

        public void SetOff() {
            _camera.enabled = false;
        
        }
    }
}
