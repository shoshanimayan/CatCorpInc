using UnityEngine;
using Core;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using Gameplay;
using Cinemachine;
using UnityEngine.UI;
using DG.Tweening;
using Managers;

namespace NPC
{
    public class NPCView : MonoBehaviour, IView, Interactable
    {

        ///  INSPECTOR VARIABLES       ///
        [SerializeField] private TextStep[] _steps;
        [SerializeField] private Transform _face;
        [SerializeField] private CinemachineVirtualCamera _cam;
        [SerializeField] private Canvas _symbolCanvas;
        [SerializeField] private Texture2D[] _symbolTextures;

        ///  PRIVATE VARIABLES         ///
        private NPCMediator _mediator;
        private int _currentStep = 0;
        private bool _hasGoal;
        private Objective _npcObjective;
        private Quaternion originalRotation;
        private bool _interacted;


        ///  PRIVATE METHODS           ///
        private void Awake()
        {
            HideSymbol();
            _cam.enabled = false;
            for (int i = 0; i < _steps.Length; i++)
            {
                if (_steps[i].CompleteGoalOnCompletion)
                {
                    _hasGoal = true;
                    _npcObjective = _steps[i].Objective;
                }
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

        private void Update()
        {
            if (_symbolCanvas.enabled)
            {
                Vector3 targetPostition = new Vector3(Camera.main.transform.position.x,
                                           _symbolCanvas.transform.position.y,
                                           Camera.main.transform.position.z);
                _symbolCanvas.transform.LookAt(targetPostition);
            }
        }

        private void Start()
        {
            _mediator.SetCoffee();
            originalRotation =  _symbolCanvas.transform.rotation;

        }
        ///  PUBLIC API                ///

        public bool Interacted
        {
            get { return _interacted; }
            private set { }
        }
        public void Init(NPCMediator mediator)
        {
            _mediator = mediator;
        }

        public int GetCollectableKey()
        {
            return _steps[_currentStep].CollectableKey;
        }

        public void DoInteraction()
        {
            _cam.enabled = true;
            _mediator.SendStep(_steps[_currentStep], this, _face);

        }

        public void CoffeeInteraction()
        {
            _mediator.GotCoffee();
        }

        public void IncrementStep()
        {
            _cam.enabled = false;

            if (_steps[_currentStep].UnblocksConversation)
            {
                _mediator.SendUnblock(_steps[_currentStep].UnblockStep);
            }
            if (_currentStep + 1 < _steps.Length && !_steps[_currentStep].NeedsCollectable && !_steps[_currentStep].WaitingForAnotherConversation)
            {

                _currentStep++;
            }
        }



        public void IncrementStepByValue(int increment)
        {
            _cam.enabled = false;
            if (_steps[_currentStep].UnblocksConversation)
            {
                _mediator.SendUnblock(_steps[_currentStep].UnblockStep);
            }
            if (_currentStep + increment < _steps.Length && !_steps[_currentStep].NeedsCollectable && !_steps[_currentStep].WaitingForAnotherConversation)
            {

                _currentStep += increment;

                if (_steps[_currentStep - increment].IsMultipleChoice)
                {

                    _mediator.SendStep(_steps[_currentStep], this);

                }
            }


        }

        public bool GetNeedsCollectable()
        {
            return _steps[_currentStep].NeedsCollectable;
        }

        public bool CheckForMatchingStep(TextStep step)
        {
            return step == _steps[_currentStep];
        }

        public bool IsStepBlocked()
        {
            return _steps[_currentStep].WaitingForAnotherConversation;
        }


        public void ForceIncrementStep()
        {
            _cam.enabled = false;
            if (_currentStep + 1 < _steps.Length)
            {
                if (_steps[_currentStep].UnblocksConversation)
                {
                    _mediator.SendUnblock(_steps[_currentStep].UnblockStep);
                }
                _currentStep++;
            }
        }

        public void HideSymbol()
        {
            _symbolCanvas.enabled = false;
        }

        public void ShowQuestSymbol() 
        {
            if (_mediator.GetCurrentState() == State.Text||_hasGoal==false) { return; }
            _symbolCanvas.enabled = true;
            RawImage image =  _symbolCanvas.GetComponentInChildren<RawImage>();
            if (image != null) 
            {
                image.transform.localEulerAngles = new Vector3(0, 0, 0);
                image.texture = _symbolTextures[0];
            }
        }

        public void ShowHeartSymbol()
        {
            _symbolCanvas.transform.localEulerAngles = new Vector3(0, 0, 0);
            _symbolCanvas.enabled = true;
            RawImage image = _symbolCanvas.GetComponentInChildren<RawImage>();
            if (image != null)
            {
                image.texture = _symbolTextures[1];
                
                DOTween.Sequence()
                    .Append(image.transform.DOLocalRotate(new Vector3(0, 0, 90), 0.25f, RotateMode.Fast).SetEase(Ease.Linear))
                    .Append(image.transform.DOLocalRotate(new Vector3(0, 0, -90), 0.5f, RotateMode.Fast).SetEase(Ease.Linear))
                    .Append(image.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.25f, RotateMode.Fast).SetEase(Ease.Linear))
                    .AppendCallback(() => {
                        _symbolCanvas.transform.localEulerAngles = new Vector3(0, 0, 0);
                        if (_hasGoal)
                        {
                            ShowQuestSymbol();
                        }
                        else
                        {
                            HideSymbol();
                        }
                    }).SetId("coffee");
            }
        }

        public void HoverOn()
        {
        }

        public void DeactivateQuest()
        { 
            _hasGoal = false;
            _npcObjective = null;
            HideSymbol();
        }

        public bool CompareObjective(Objective obj)
        {
            if (_hasGoal)
            {
                return _npcObjective.Equals(obj);
            }
            else
            {

                return false;
            }
        }

    }
}
