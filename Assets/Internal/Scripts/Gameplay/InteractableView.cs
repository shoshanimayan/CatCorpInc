using UnityEngine;
using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

namespace Gameplay
{
    public class InteractableView : MonoBehaviour, IView, Interactable
    {

        ///  INSPECTOR VARIABLES       ///
        [SerializeField] private Objective _objective;
        [SerializeField] private UnityEvent _event=null;
        ///  PRIVATE VARIABLES         ///

        ///  PRIVATE METHODS           ///
        private InteractableMediator _mediator;
        ///  PUBLIC API                ///
        public void DoInteraction()
        {
            if (_event != null)
            { 
                _event.Invoke();
            }
            _mediator.CompleteObjective(_objective);
            
        }

        public void HoverOn()
        {
            
        }

        public void Initializer(InteractableMediator mediator)
        { 
            _mediator= mediator;
        }
    }
}
