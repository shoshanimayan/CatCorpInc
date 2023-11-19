using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Player
{
	public class InputReciever: MonoBehaviour
	{

		///  INSPECTOR VARIABLES       ///

		///  PRIVATE VARIABLES         ///
		private PlayerControls _playerControls;

        ///  PRIVATE METHODS           ///
        private void Awake()
        {
            _playerControls = new PlayerControls();
        }

        private void OnEnable()
        {
            _playerControls.Enable();
        }

        private void OnDisable()
        {
            _playerControls.Disable();
        }
        ///  LISTNER METHODS           ///

        ///  PUBLIC API                ///

        public Vector2 GetPlayerMovement() {
            return _playerControls.Player.Movement.ReadValue<Vector2>();
        }

        public Vector2 GetMouseDelta()
        {
            return _playerControls.Player.Look.ReadValue<Vector2>();
        }

        public bool PlayerJumped()
        {
            return _playerControls.Player.Jump.triggered;
        }

        public bool PlayerFired()
        {
            return _playerControls.Player.Fire.IsPressed();
        }

        public bool PlayerInteracted()
        {
            return _playerControls.Player.Interact.triggered;
        }

        public bool PlayerPause()
        {
            return _playerControls.Player.Pause.triggered;
        }

        public bool PlayerToggledObjective()
        {
            return _playerControls.Player.ObjectiveMenu.triggered;
        }

        public bool PlayerSprinting() {
           return  _playerControls.Player.Sprint.IsPressed();
        }

        

        ///  IMPLEMENTATION            ///

    }
}
