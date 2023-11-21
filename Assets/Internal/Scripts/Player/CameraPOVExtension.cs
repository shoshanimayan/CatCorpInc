
using UnityEngine;
using Cinemachine;
using Player;
using Unity.VisualScripting;

public class CameraPOVExtension : CinemachineExtension
{
    [SerializeField] private InputReciever _inputReciever;
    [SerializeField] private float _clampAngle=80f;
    [SerializeField] private float _horizontalSpeed = 10f;
    [SerializeField] private float _verticalSpeed = 10f;
    [SerializeField]private Vector3 _startingRotation;
    

    private void Start()
    {
        _startingRotation = transform.localRotation.eulerAngles;
    }

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (vcam.Follow && _inputReciever.IsPlaying())
        {
            if (stage == CinemachineCore.Stage.Aim)
            {
                if (_startingRotation == null) 
                {
                    _startingRotation = transform.localRotation.eulerAngles;
                }
                Vector2 deltaInput = _inputReciever.GetMouseDelta();
                _startingRotation.x += deltaInput.x * Time.deltaTime*_verticalSpeed;
                _startingRotation.y += deltaInput.y * Time.deltaTime*_horizontalSpeed;
                _startingRotation.y = Mathf.Clamp(_startingRotation.y,-_clampAngle,_clampAngle);
                state.RawOrientation = Quaternion.Euler(-_startingRotation.y,_startingRotation.x,0f);
            }
        }
    }

    


}
