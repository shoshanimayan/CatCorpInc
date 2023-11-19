using UnityEngine;
using Core;
using System.Collections;
using System.Collections.Generic;
namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(InputReciever))]

    public class PlayerControllerView: MonoBehaviour,IView
	{

        [SerializeField] private float _playerSpeed = 2.5f;
        [SerializeField] private float _SprintSpeedBonus = 1f;

        [SerializeField] private float _jumpHeight = 1.0f;
        [SerializeField] private float _gravityValue = -9.81f;

        private CharacterController _controller;
        private Vector3 _playerVelocity;
        private bool _groundedPlayer;
        private InputReciever _inputReciever;
        private Transform _cameraTransform;
        
       

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
            _inputReciever = GetComponent<InputReciever>();
        }

        private void Start()
        {
            _cameraTransform = Camera.main.transform;

        }

        void Update()
        {
            _groundedPlayer = _controller.isGrounded;
            if (_groundedPlayer && _playerVelocity.y < 0)
            {
                _playerVelocity.y = 0f;
            }

            Vector2 movement = _inputReciever.GetPlayerMovement();
            Vector3 move= new Vector3(movement.x,0f, movement.y);
            move=_cameraTransform.forward*move.z+_cameraTransform.right*movement.x;
            move.y = 0f;
            _controller.Move(move * Time.deltaTime * (_playerSpeed+(_inputReciever.PlayerSprinting()?_SprintSpeedBonus:0)));

            

            // Changes the height position of the player..
            if (_inputReciever.PlayerJumped() && _groundedPlayer)
            {
                _playerVelocity.y += Mathf.Sqrt(_jumpHeight * -3.0f * _gravityValue);
            }

            _playerVelocity.y += _gravityValue * Time.deltaTime;
            _controller.Move(_playerVelocity * Time.deltaTime);
        }

    }
}
