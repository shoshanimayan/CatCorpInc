using UnityEngine;
using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using DG.Tweening;

namespace Gameplay
{
    public class InteractableView : MonoBehaviour, IView, Interactable
    {

        ///  INSPECTOR VARIABLES       ///
        [SerializeField] private Objective _objective;
        [SerializeField] private UnityEvent _event=null;
        [SerializeField] private float _delay;
        ///  PRIVATE VARIABLES         ///

        ///  PRIVATE METHODS           ///
        private InteractableMediator _mediator;
        ///  PUBLIC API                ///
        public void DoInteraction()
        {
            
            _mediator.PlaySound(transform.position);
            DOTween.Sequence().AppendCallback(() =>
            {
               
                if (_event != null)
                {
                    _event.Invoke();
                }
                else
                { 
                    gameObject.GetComponent<MeshRenderer>().enabled = false;
                    gameObject.GetComponent<Collider>().enabled = false;
                }
            }).AppendInterval(_delay).AppendCallback
            (() =>
            {
                if (_objective != null)
                {
                    _mediator.CompleteObjective(_objective);

                }
            });

           
          
            
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

        public void Initializer(InteractableMediator mediator)
        { 
            _mediator= mediator;
        }
    }
}
