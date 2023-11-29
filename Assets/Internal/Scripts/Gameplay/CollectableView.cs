using UnityEngine;
using Core;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using System.Diagnostics;
using Cinemachine;

namespace Gameplay
{
    public class CollectableView : MonoBehaviour, IView, Interactable
    {

        ///  INSPECTOR VARIABLES       ///
        [SerializeField] private TextStep[] _steps;
        [SerializeField] private int _key;
        [SerializeField] private CinemachineVirtualCamera _cam;

        ///  PRIVATE VARIABLES         ///
        private CollectableMediator _mediator;
        private int _currentStep = 0;

        ///  PRIVATE METHODS           ///

        ///  PUBLIC API                ///
        public void DoInteraction()
        {
            _cam.enabled = true;

            _mediator.SendStep(_steps[_currentStep], this,transform);

            _mediator.CollectObject(_key);
        }

        public void HoverOn()
        {
        }

        public void IncrementStep()
        {
            _cam.enabled = false;

            gameObject.SetActive(false);

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

    }
}
