using UnityEngine;
using Core;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using System.Diagnostics;

namespace Gameplay
{
    public class CollectableView : MonoBehaviour, IView, Interactable
    {

        ///  INSPECTOR VARIABLES       ///
        [SerializeField] private TextStep[] _steps;
        [SerializeField] private int _key;
        ///  PRIVATE VARIABLES         ///
        private CollectableMediator _mediator;
        private int _currentStep = 0;

        ///  PRIVATE METHODS           ///

        ///  PUBLIC API                ///
        public void DoInteraction()
        {
            _mediator.SendStep(_steps[_currentStep], this);

            _mediator.CollectObject(_key);
        }

        public void HoverOn()
        {
        }

        public void IncrementStep()
        {
            gameObject.SetActive(false);

        }

        public void IncrementStepByValue(int increment)
        {
            gameObject.SetActive(false);

        }

        public void Init(CollectableMediator mediator)
        {
            _mediator = mediator;
        }

    }
}
