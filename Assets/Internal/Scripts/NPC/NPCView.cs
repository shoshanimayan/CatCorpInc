using UnityEngine;
using Core;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using Gameplay;
using Cinemachine;

namespace NPC
{
	public class NPCView: MonoBehaviour,IView, Interactable
	{

        ///  INSPECTOR VARIABLES       ///
        [SerializeField] private TextStep[] _steps;
        [SerializeField] private Transform _face;
        [SerializeField] private CinemachineVirtualCamera _cam;
        ///  PRIVATE VARIABLES         ///
        private NPCMediator _mediator;
        private int _currentStep = 0;

        ///  PRIVATE METHODS           ///
        private void Awake()
        {
            _cam.enabled = false;
            for (int i = 0; i < _steps.Length; i++)
            {
                if (i + 1 < _steps.Length)
                {
                    if (_steps[i].StartNextEntryOnCompletion)
                    {
                        _steps[i].SetNextStep(_steps[i + 1]);
                    }
                }
                else
                {
                    if (_steps[i].StartNextEntryOnCompletion)
                    {
                        _steps[i].StartNextEntryOnCompletion = false;
                    }
                }
            }
            
            
        }

        private void Start()
        {
            _mediator.SetCoffee();
        }
        ///  PUBLIC API                ///


        public void Init(NPCMediator mediator)
        {
            _mediator = mediator;
        }

        public int GetCollectableKey()
        {
            return _steps[_currentStep].CollectableKey;
        }

        public void DoInteraction()
        {

            _cam.enabled = true;
            _mediator.SendStep(_steps[_currentStep],this,_face);
            
        }

        public void CoffeeInteraction()
        {
            _mediator.GotCoffee();
        }

        public void IncrementStep()
        {
            _cam.enabled = false;

            if (_steps[_currentStep].UnblocksConversation)
            {
                _mediator.SendUnblock(_steps[_currentStep].UnblockStep);
            }
            if (_currentStep + 1 < _steps.Length && !_steps[_currentStep].NeedsCollectable && !_steps[_currentStep].WaitingForAnotherConversation)
            {
               
                _currentStep++;
            }
        }



        public void IncrementStepByValue(int increment) 
        {
            _cam.enabled = false;
            if (_steps[_currentStep].UnblocksConversation)
            {
                _mediator.SendUnblock(_steps[_currentStep].UnblockStep);
            }
            if (_currentStep + increment < _steps.Length && !_steps[_currentStep].NeedsCollectable && !_steps[_currentStep].WaitingForAnotherConversation)
            {

                _currentStep+=increment;

                if (_steps[_currentStep - increment].IsMultipleChoice)
                {

                    _mediator.SendStep(_steps[_currentStep], this);

                }
            }
            

        }

        public bool GetNeedsCollectable()
        {
            return _steps[_currentStep].NeedsCollectable;
        }

        public bool CheckForMatchingStep(TextStep step)
        { 
            return step == _steps[_currentStep];
        }

        public bool IsStepBlocked()
        {
            return _steps[_currentStep].WaitingForAnotherConversation;
        }


        public void ForceIncrementStep()
        {
            _cam.enabled = false;
            if (_currentStep + 1 < _steps.Length )
            {
                if (_steps[_currentStep].UnblocksConversation)
                {
                    _mediator.SendUnblock(_steps[_currentStep].UnblockStep);
                }
                _currentStep++;
            }
        }

        public void HoverOn()
        {
        }
    }
}
