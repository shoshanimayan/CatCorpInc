using UnityEngine;
using Core;
using System.Collections;
using System.Collections.Generic;
namespace Gameplay
{

    public struct TextObject
    {
        public string Name;
        public string BodyText;

        public TextObject(string name, string bodyText)
        { 
            Name = name;
            BodyText = bodyText;
        }

    }

    public enum ReadState {Text,Choice,Draw,Drag }

	public class ReaderView: MonoBehaviour,IView
	{


		///  INSPECTOR VARIABLES       ///
		[SerializeField] private Canvas _TextDisplayCanvas;
        [SerializeField] private Canvas _TextChoiceCanvas;

        ///  PRIVATE VARIABLES         ///
        private ReaderMediator _mediator;
        ///  PRIVATE METHODS           ///

        ///  PUBLIC API                ///
        public void Init(ReaderMediator mediator)
        { 
            _mediator = mediator;
        }

        public void SetReadUI(ReadState state)
        {
            _TextDisplayCanvas.enabled = false;
            _TextChoiceCanvas.enabled=false;
            switch (state)
            {
                case ReadState.Text:
                    _TextDisplayCanvas.enabled = true;
                    break;
                case ReadState.Choice:
                    _TextChoiceCanvas.enabled = true;
                    break;


            }
        }
    }
}
