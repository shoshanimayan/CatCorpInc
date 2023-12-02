using UnityEngine;
using Core;
using System.Collections;
using System.Collections.Generic;
using TMPro;

namespace Ui
{
	public class TextTypeView: MonoBehaviour,IView
	{

		///  INSPECTOR VARIABLES       ///
		[SerializeField] TextMeshProUGUI _promptLabel;
		///  PRIVATE VARIABLES         ///
		private TextTypeMediator _mediator;
		///  PRIVATE METHODS           ///

		///  PUBLIC API                ///
		public void Init( TextTypeMediator mediator)
		{		
			_mediator = mediator;
		}

		public void SetPromptLabel(string prompt)
		{ 
		_promptLabel.text = prompt;
		}

		public void SubmitText(TextMeshProUGUI message)
		{ 
			_mediator.SendTypedMessage(message.text);
		}
	}
}
