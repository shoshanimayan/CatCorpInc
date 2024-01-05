using UnityEngine;
using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using ScriptableObjects;
using Cinemachine;

namespace Gameplay
{
    public class CoffeePotView : MonoBehaviour, IView, Interactable
    {

        ///  INSPECTOR VARIABLES       ///
        [SerializeField] private TextStep[] _steps;
        [SerializeField] private CinemachineVirtualCamera _cam;


        ///  PRIVATE VARIABLES         ///
        private int _currentStep = 0;

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
           
        }

        public void HoverOn()
        {
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
    }
}
