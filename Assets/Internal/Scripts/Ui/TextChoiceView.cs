using UnityEngine;
using Core;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
namespace Ui
{
	public class TextChoiceView: MonoBehaviour,IView
	{

        ///  INSPECTOR VARIABLES       ///
        [SerializeField] private GameObject _choiceUIPrefab;
        [SerializeField] private Transform _content;
        ///  PRIVATE VARIABLES         ///
        private List<GameObject> _choices;
        private TextChoiceMediator _mediator;
        ///  PRIVATE METHODS           ///
        private void CreateChoice(string choice, int choiceNumber){
            var x=Instantiate(_choiceUIPrefab, _content);
            Button button=null;
            x.TryGetComponent<Button>(out button);
            if (button != null)
            {
                button.onClick.AddListener(() => _mediator.SendChoice(choiceNumber));
            }
            _choices.Add(x);
        }

        private void Awake()
        {
            _choices = new List<GameObject>();
        }

        private void  ClearChoices()
        {
            for (int i = 0; i < _choices.Count; i++)
            {
                Destroy(_choices[i]);
            }
            _choices.Clear();
        }

        ///  PUBLIC API                ///

        public void SetChoices(string[] choices)
        {
            ClearChoices();

            for (int i = 0; i < choices.Length; i++)
            {
                CreateChoice(choices[i],i);
            }
        }

        public void Init(TextChoiceMediator mediator)
        {  
        _mediator = mediator;
        }

    }
}
