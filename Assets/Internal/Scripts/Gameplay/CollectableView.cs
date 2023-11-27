using UnityEngine;
using Core;
using System.Collections;
using System.Collections.Generic;
namespace Gameplay
{
    public class CollectableView : MonoBehaviour, IView, Interactable
    {

        ///  INSPECTOR VARIABLES       ///
        
        ///  PRIVATE VARIABLES         ///
        private CollectableMediator _mediator;
       
        ///  PRIVATE METHODS           ///

        ///  PUBLIC API                ///
        public void DoInteraction()
        {
            _mediator.CollectObject();
            gameObject.SetActive(false);
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

        public void Init(CollectableMediator mediator)
        {
            _mediator = mediator;
        }

    }
}
