using UnityEngine;
using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

namespace Gameplay
{
	public class CrosshairView: MonoBehaviour,IView
	{

		///  INSPECTOR VARIABLES       ///
		[SerializeField] private TextMeshProUGUI _label;

        ///  PRIVATE VARIABLES         ///
        private Image[] _crosshairComponents;

        ///  PRIVATE METHODS           ///
        private void Awake()
        {
			_label.text = "";
			_crosshairComponents= GetComponentsInChildren<Image>();	
        }

		private void SetColor(Color color) 
		{
		foreach (Image img in _crosshairComponents)
			{
				img.color = color;
			}
		}
		///  PUBLIC API                ///

		public void Hovering(bool hover)
		{
			if (hover)
			{ 
				SetColor(_label.color);
			}
			else {
				SetColor(Color.white);
			}
		}

		public void SetLabelText(string text)
		{
			_label.text = text;
		}

    }
}
