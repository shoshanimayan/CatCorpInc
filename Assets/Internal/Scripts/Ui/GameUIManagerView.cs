using UnityEngine;
using Core;
using System.Collections;
using System.Collections.Generic;
using Managers;

namespace Ui
{
	public class GameUIManagerView: MonoBehaviour,IView
	{

		///  INSPECTOR VARIABLES       ///
		[SerializeField] Canvas _playCanvas;
		[SerializeField] Canvas _pauseCanvas;
		[SerializeField] Canvas _objectiveCanvas;
		[SerializeField] Canvas _readerCanvas;
        [SerializeField] Canvas _endingCanvas;

		///  PRIVATE VARIABLES         ///
		private GameUIManagerMediator _mediator;
        ///  PRIVATE METHODS           ///
		private void Awake()
        {
            _endingCanvas.enabled = false;
        }
		///  PUBLIC API                ///

		public void OnEnd()
		{
            _playCanvas.enabled = false;
            _pauseCanvas.enabled = false;
            _objectiveCanvas.enabled = false;
            _readerCanvas.enabled = false;
            _endingCanvas.enabled = true;

        }

		

		public void Init(GameUIManagerMediator mediator)
		{ 
			_mediator = mediator;
		}

        public void SetCanvas(State state)
		{
			_playCanvas.enabled = false;
			_pauseCanvas.enabled = false;
			_objectiveCanvas.enabled = false;
			_readerCanvas.enabled = false;
			switch (state) {
				case State.Play: 
					_playCanvas.enabled = true;
					break;
				case State.Paused:
					_pauseCanvas.enabled = true;
					break;
				case State.Text: 
					_readerCanvas.enabled = true;
					break;
				case State.Objective:
					_objectiveCanvas.enabled = true;
					break;
			}
		}
	}
}
