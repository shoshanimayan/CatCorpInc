using UnityEngine;
using Core;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using DG.Tweening;

namespace Player
{
    public enum WalkState {None,Walk,Run,Shake }

    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(InputReciever))]

    public class PlayerControllerView: MonoBehaviour,IView
	{

        [SerializeField] private float _playerSpeed = 2.5f;
        [SerializeField] private float _SprintSpeedBonus = 1f;

        [SerializeField] private float _jumpHeight = 1.0f;
        [SerializeField] private float _gravityValue = -9.81f;
        [SerializeField] private Transform _head;

        private CharacterController _controller;
        private Vector3 _playerVelocity;
        private bool _groundedPlayer;
        private InputReciever _inputReciever;
        private Transform _cameraTransform;
        private PlayerControllerMediator _mediator;
        private bool _sprinting;

        private float _originalHeadHeight;
        private bool _crouching;
        private WalkState _walkState=WalkState.None;
       

        private void Awake()
        {
            _originalHeadHeight = _head.localPosition.y;
            _controller = GetComponent<CharacterController>();
            _inputReciever = GetComponent<InputReciever>();
        }

        private void Start()
        {
            _cameraTransform = Camera.main.transform;

        }

        private void UpdateWalkState(WalkState state) {
            if (state != _walkState)
            { 
                _walkState = state;
                _mediator.ChangeWalkState(_walkState);
            }
        
        }

        private void HandleCrouch(bool enable)
        {
            if (enable )
            {
                _crouching = true;
                _head.DOLocalMove(new Vector3(_head.localPosition.x, _originalHeadHeight - 1f, _head.localPosition.z),.1f) ;
            }
            else 
            {
                _crouching = false;
                _head.DOLocalMove(new Vector3(_head.localPosition.x, _originalHeadHeight, _head.localPosition.z), .1f);

            }
        }

        private void Update()
        {
            if(_inputReciever.PlayerToggledObjective() && (_mediator.GetCurrentState() == Managers.State.Play || _mediator.GetCurrentState() == Managers.State.Objective))
            {

                _mediator.ToggleObjectiveMode();
            }
            if (_inputReciever.PlayerPause() )
            {
                
                _mediator.TogglePauseMenu();
            }
            if (_inputReciever.PlayerProgressingReader() && _mediator.GetCurrentState() == Managers.State.Text && _mediator.IsReadStateClickable())
            { 
                _mediator.ProgressReader();
            }
            if (_mediator.CanReadInput() && _mediator.GetCurrentState() == Managers.State.Play)
            {
                _sprinting = _inputReciever.PlayerSprinting()&&!_inputReciever.PlayerCrouch();
                _groundedPlayer = _controller.isGrounded;
                if (_groundedPlayer && _playerVelocity.y < 0)
                {
                    _playerVelocity.y = 0f;
                }
                if (_inputReciever.PlayerCrouch() && _groundedPlayer )
                {
                    if (!_crouching)
                    {
                        HandleCrouch(true);
                    }
                }
                else
                {
                    if (_crouching)
                    {
                        HandleCrouch(false);
                    }
                }
                Vector2 movement = _inputReciever.GetPlayerMovement();
                Vector3 move = new Vector3(movement.x, 0f, movement.y);
                move = _cameraTransform.forward * move.z + _cameraTransform.right * movement.x;
                move.y = 0f;
                _controller.Move(move * Time.deltaTime * (_playerSpeed * (_sprinting? _SprintSpeedBonus : 1)));



                // Changes the height position of the player..
                if (_inputReciever.PlayerJumped() && _groundedPlayer)
                {
                    _playerVelocity.y += Mathf.Sqrt(_jumpHeight * -3.0f * _gravityValue);
                }

              

                _playerVelocity.y += _gravityValue * Time.deltaTime;
                _controller.Move(_playerVelocity * Time.deltaTime);
                var state = WalkState.None;
                if (move.magnitude > 0 && _groundedPlayer)
                {
                    if (_sprinting)
                    {
                        state = WalkState.Run;
                    }
                    else
                    {
                        state = WalkState.Walk;
                    }
                }
                UpdateWalkState(state);
            }
  
        }

        public void StopWalkState() 
        {
        _walkState = WalkState.None; ;
        
        }

        public void Init( PlayerControllerMediator mediator)
        {
            _mediator = mediator;
        }

        public void EnableInputPlay(bool enable)
        { 
        _inputReciever.SetIsPlaying(enable);
        }

    }
}
