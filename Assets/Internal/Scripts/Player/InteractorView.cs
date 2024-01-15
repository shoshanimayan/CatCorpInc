using UnityEngine;
using Core;
using System.Collections;
using System.Collections.Generic;
using Gameplay;

namespace Player
{
    [RequireComponent(typeof(InputReciever))]
    public class InteractorView: MonoBehaviour,IView
	{

        ///  INSPECTOR VARIABLES       ///
        [SerializeField] private float _inspectionDistance=100f;
        ///  PRIVATE VARIABLES         ///
        private Transform _cam;
        private bool _hovering;
        private InteractorMediator _mediator;
        private InputReciever _inputReciever;
        private Interactable _interactingWith;

        ///  PRIVATE METHODS           /// 
        private void Start()
        {
            _cam= Camera.main.transform;
        }

        private void Awake()
        {
            _inputReciever = GetComponent<InputReciever>();

        }
        private void CheckForInteraction()
        {
            RaycastHit hit;

            if (Physics.Raycast(_cam.position, _cam.forward, out hit, _inspectionDistance))
            {
                if (hit.collider != null)
                {
                    if (hit.collider.gameObject.GetComponent<Interactable>() != null)
                    {
                        _interactingWith = hit.collider.gameObject.GetComponent<Interactable>();

                        Hovering = true;
                    }
                    else
                    {
                        Hovering = false;
                        _interactingWith = null;
                    }
                }
                else
                {
                    Hovering = false;
                    _interactingWith = null;

                }
            }
            else
            {
                Hovering = false;
                _interactingWith = null;
            }
        }

        private void Update()
        {
            CheckForInteraction();
            if (_interactingWith != null && _inputReciever.PlayerInteracted() && _mediator.CanInteract())
            {
                
                _interactingWith.DoInteraction();
                _interactingWith=null;
                Hovering=false;
            }
        }
        ///  PUBLIC API                ///
        public bool Hovering
        {
            get { return _hovering; }
            set {
                if (_hovering == value)
                {
                    return;
                }
                _hovering = value;
                if (_hovering)
                {

                    _mediator.SetHovering(!_interactingWith.Interacted? _interactingWith.gameObject.name:"");
                }
                else
                {
                    _mediator.SetHovering("");

                }


            }
        }


        public void Init(InteractorMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
