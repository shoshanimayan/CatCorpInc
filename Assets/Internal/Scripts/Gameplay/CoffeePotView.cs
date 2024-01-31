using UnityEngine;
using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using ScriptableObjects;
using Cinemachine;

namespace Gameplay
{
    public class CoffeePotView : OutlineHandler, IView, Interactable
    {

        ///  INSPECTOR VARIABLES       ///
        [SerializeField] private TextStep[] _steps;
        [SerializeField] private CinemachineVirtualCamera _cam;


        ///  PRIVATE VARIABLES         ///
        private int _currentStep = 0;
        private bool _interacted;
        ///  PRIVATE METHODS           ///
        private CoffeePotMediator _mediator;
        ///  PUBLIC API                ///
        public void Init(CoffeePotMediator mediator)
        { 
        _mediator = mediator;
        }
        
        public void DoInteraction()
        {
            _cam.enabled = true;

            _mediator.SendStep(_steps[_currentStep], this, transform);
            _interacted = true;
        }

        public void HoverOn()
        {
            OutlineEnable();
        }

        public void IncrementStep()
        {
            _mediator.ActivateCoffee();
            _cam.enabled = false;
            transform.parent.gameObject.SetActive(false);

        }

        public void IncrementStepByValue(int increment)
        {
        }
        public bool Interacted
        {
            get { return _interacted; }
            private set { }
        }

        public void HoverOff()
        {
            OutlineDisable();
        }
    }
}
