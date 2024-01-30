using UnityEngine;
using Core;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using ScriptableObjects;
using TMPro;
using NPC;

namespace Gameplay
{
	public class BillBoardView: MonoBehaviour,IView,Interactable
	{

        ///  INSPECTOR VARIABLES       ///
        [SerializeField] private TextStep[] _steps;
        [SerializeField] private Transform _cameraFocus;
        [SerializeField] private CinemachineVirtualCamera _cam;
        [SerializeField] private Canvas _billBoardCanvas;
        [SerializeField] private TextMeshProUGUI _billBoardText;


        ///  PRIVATE VARIABLES         ///
        private BillBoardMediator _mediator;
        private int _currentStep = 0;
        private bool _interacted;


        ///  PRIVATE METHODS           ///
        private void Awake()
        {
            _billBoardCanvas.enabled = false;
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

        ///  PUBLIC API                ///

        public bool Interacted
        {
            get { return _interacted; }
            private set { }
        }

        public void Init(BillBoardMediator mediator)
        {
            _mediator = mediator;
        }

        public void SetText(string text)
        { 
            _billBoardText.text = text;
        }

        public void EnableBillBoard(bool enable)
        { 
        _billBoardCanvas.enabled |= enable;
        }

        public void DoInteraction()
        {

            _cam.enabled = true;
            _mediator.SendStep(_steps[_currentStep], this, _cameraFocus);
        }

        public bool CheckForMatchingStep(TextStep step)
        {
            return step == _steps[_currentStep];
        }

        public void HoverOn()
        {
        }

        public void ForceIncrementStep()
        {
            _cam.enabled = false;
            if (_currentStep + 1 < _steps.Length)
            {
                
                _currentStep++;
            }
        }


        public void IncrementStep()
        {
            _cam.enabled = false;

         
            if (_currentStep + 1 < _steps.Length && !_steps[_currentStep].NeedsCollectable && !_steps[_currentStep].WaitingForAnotherConversation)
            {

                _currentStep++;
            }
        }

        public void IncrementStepByValue(int increment)
        {
        }

        public bool IsStepBlocked()
        {
            return _steps[_currentStep].WaitingForAnotherConversation;
        }



    }
}
