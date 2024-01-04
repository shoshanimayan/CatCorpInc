using UnityEngine;
using Core;
using System.Collections;
using System.Collections.Generic;
namespace Gameplay
{
    public class CoffeePotView : MonoBehaviour, IView, Interactable
    {

        ///  INSPECTOR VARIABLES       ///

        ///  PRIVATE VARIABLES         ///

        ///  PRIVATE METHODS           ///
        private CoffeePotMediator _mediator;
        ///  PUBLIC API                ///
        public void Init(CoffeePotMediator mediator)
        { 
        _mediator = mediator;
        }
        
        public void DoInteraction()
        {
            _mediator.ActivateCoffee();
        }

        public void HoverOn()
        {
        }

        public void IncrementStep()
        {
        }

        public void IncrementStepByValue(int increment)
        {
        }
    }
}
