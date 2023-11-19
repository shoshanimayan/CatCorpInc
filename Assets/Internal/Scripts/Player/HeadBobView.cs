using UnityEngine;
using Core;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;

namespace Player
{
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public class HeadBobView : MonoBehaviour, IView
    {

        ///  INSPECTOR VARIABLES       ///
        [SerializeField] private NoiseSettings _noiseSettingsWalk;
        [SerializeField]  private NoiseSettings _noiseSettingsRun;


        ///  PRIVATE VARIABLES         ///
        private CinemachineBasicMultiChannelPerlin _bobber;
        private CinemachineVirtualCamera _camera;

        private WalkState _walkState;

        ///  PRIVATE METHODS           ///
        private void Awake()
        {
            _camera = GetComponent<CinemachineVirtualCamera>();
            _bobber = _camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }

        ///  PUBLIC API                ///

        public WalkState CurrentWalkState {
            get { return _walkState; }
            set {
                if (value == _walkState) { return; }
                else { _walkState = value; }
                switch (_walkState)
                {

                    case WalkState.None:
                        SetOff();
                        break;
                    case WalkState.Walk:
                        SetWalk();
                        break;
                    case WalkState.Run:
                        SetWalk();
                        break;
                }
            }
        }

    

        public void SetWalk()
        {
            _bobber.m_NoiseProfile = _noiseSettingsWalk;
        }
        public void SetRun() {
            _bobber.m_NoiseProfile = _noiseSettingsRun;
        }

        public void SetOff() {
            _bobber.m_NoiseProfile = null;


        }




    }
}
