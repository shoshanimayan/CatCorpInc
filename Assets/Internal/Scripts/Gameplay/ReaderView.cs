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
		[SerializeField] private GameObject _TextDisplayPanel;
        [SerializeField] private GameObject _TextChoicePanel;

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
            _TextDisplayPanel.SetActive( false);
            _TextChoicePanel.SetActive(false);
            switch (state)
            {
                case ReadState.Text:
                    _TextDisplayPanel.SetActive(true);
                    break;
                case ReadState.Choice:
                    _TextChoicePanel.SetActive(true);
                    break;


            }
        }
    }
}
