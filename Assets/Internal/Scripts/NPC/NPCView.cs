using UnityEngine;
using Core;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using Gameplay;

namespace NPC
{
	public class NPCView: MonoBehaviour,IView, Interactable
	{

        ///  INSPECTOR VARIABLES       ///
        [SerializeField] private TextStep[] _steps;
        
        ///  PRIVATE VARIABLES         ///
        private NPCMediator _mediator;
        private int _currentStep=0;
        ///  PRIVATE METHODS           ///
        private void Awake()
        {
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
        public void Init(NPCMediator mediator)
        {
            _mediator = mediator;
        }

        public void DoInteraction()
        {
            
            _mediator.SendStep(_steps[_currentStep],transform);
            if (_currentStep+1 < _steps.Length)
            {
                _currentStep++;
            }
        }

        public void HoverOn()
        {
        }
    }
}
