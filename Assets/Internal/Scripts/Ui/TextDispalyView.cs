using UnityEngine;
using Core;
using System.Collections;
using System.Collections.Generic;
using TMPro;

namespace Ui
{
	public class TextDispalyView: MonoBehaviour,IView
	{

        ///  INSPECTOR VARIABLES       ///
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _textField;
        ///  PRIVATE VARIABLES         ///

        ///  PRIVATE METHODS           ///

        ///  PUBLIC API                ///
        public void SetName(string name)
        { 
            _name.text = name;
        }

        public void SetText(string text)
        { 
            _textField.text = text;
        }
    }
}
