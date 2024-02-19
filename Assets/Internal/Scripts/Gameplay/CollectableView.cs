using UnityEngine;
using Core;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using System.Diagnostics;
using Cinemachine;

namespace Gameplay
{
    public class CollectableView : OutlineHandler, IView, Interactable
    {

        ///  INSPECTOR VARIABLES       ///
        [SerializeField] private TextStep[] _steps;
        [SerializeField] private int _key;
        [SerializeField] private CinemachineVirtualCamera _cam;
        [SerializeField] private bool _deactivateOnInteract=true;
        ///  PRIVATE VARIABLES         ///
        private CollectableMediator _mediator;
        private int _currentStep = 0;
        private bool _interacted;

        ///  PRIVATE METHODS           ///

        ///  PUBLIC API                ///
        public bool Interacted
        {
            get { return _interacted; }
            private set { }
        }
        public void DoInteraction()
        {
            if (!_mediator.GameStarted()) { return; }

            _cam.enabled = true;

            _mediator.SendStep(_steps[_currentStep], this,transform);

            _mediator.CollectObject(_key);
            _interacted = true;
        }

        public void HoverOn()
        {
            if (!_mediator.GameStarted()) { return; }

            OutlineEnable();
        }

        public void IncrementStep()
        {
            _cam.enabled = false;
            if (_deactivateOnInteract)
            {
                gameObject.SetActive(false);
            }

        }

        public void IncrementStepByValue(int increment)
        {
            _cam.enabled = false;

            gameObject.SetActive(false);

        }

        public void Init(CollectableMediator mediator)
        {
            _mediator = mediator;
        }

        public void HoverOff()
        {
            OutlineDisable();
        }
    }
}
