using UnityEngine;
using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using DG.Tweening;

namespace Gameplay
{
    public class InteractableView : OutlineHandler, IView, Interactable
    {

        ///  INSPECTOR VARIABLES       ///
        [SerializeField] private Objective _objective;
        [SerializeField] private UnityEvent _event=null;
        [SerializeField] private float _delay;
        [SerializeField] private bool _incrementsCollection;
        [SerializeField] private int _collectableKey;
        ///  PRIVATE VARIABLES         ///
        private bool _interacted;
        private InteractableMediator _mediator;

        ///  PRIVATE METHODS           ///
        ///  PUBLIC API                /// <summary>

        public bool Interacted {
            get { return _interacted; }
            private set { }
        }

        public void DoInteraction()
        {
            if (!_mediator.GameStarted()) { return; }

            if (!_interacted)
            {
                _interacted = true;

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
                    if (_incrementsCollection)
                    {
                        _mediator.CollectObject(_collectableKey);
                    }
                });

            }
            
        }

        public void HoverOn()
        {
            if (!_mediator.GameStarted()) { return; }
            OutlineEnable();
        }

        public void IncrementStep()
        {
        }

        public void IncrementStepByValue(int increment)
        {
        }

        public void JumpToLastStep()
        {
        }

        public void Initializer(InteractableMediator mediator)
        { 
            _mediator= mediator;
        }

        public void HoverOff()
        {
            OutlineDisable();
        }
    }
}
