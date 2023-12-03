using UnityEngine;
using Core;
using System.Collections;
using System.Collections.Generic;
using TMPro;

namespace Ui
{
	public class DragPanelView: MonoBehaviour,IView
	{

        ///  INSPECTOR VARIABLES       ///
        [SerializeField] TextMeshProUGUI _letterLabel;
        ///  PRIVATE VARIABLES         ///
        private DragPanelMediator _mediator;
        ///  PRIVATE METHODS           ///

        ///  PUBLIC API                ///
        public void Init(DragPanelMediator mediator)
        {
            _mediator = mediator;
        }

        public void SetLetterLabel(string prompt)
        {
            _letterLabel.text = prompt;
        }

    }
}
