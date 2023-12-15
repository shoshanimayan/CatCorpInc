using UnityEngine;
using Core;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using System;

namespace Ui
{
	public class TimerView: MonoBehaviour,IView
	{

		///  INSPECTOR VARIABLES       ///
		[SerializeField] private Canvas _timerCanvas;
        [SerializeField] private TextMeshProUGUI _timerText;
        [SerializeField] private int _minutes;
        [SerializeField] private Color _endColor;
        ///  PRIVATE VARIABLES         ///
        private float _totalTime;
        private float _currentTime;
        private TimerMediator _mediator;
        private bool _panicTime;
        ///  PRIVATE METHODS           ///
        private void Awake()
        {
            _totalTime = _minutes*60f;
            _currentTime = _totalTime;
            _timerText.text = "";
        }

        private void Update()
        {
            if (CountDownActive && _mediator.TimerEnabled)
            {
                if (_currentTime > 0)
                {
                    _currentTime -= Time.deltaTime;
                    if (_currentTime < 60&&_timerText.color!=_endColor&&!_panicTime)
                    { 
                        _panicTime = true;
                        _mediator.TransitionToLateMusic();
                        _timerText.color = _endColor;
                    }
                    DisplayTime(_currentTime);
                }
                else
                {
                    _mediator.TimerEnabled = false;
                    DisplayTime(0);

                }
            }
        }

        private void DisplayTime(float time)
        {
            TimeSpan t = TimeSpan.FromSeconds(time);

           
            string formattedTime = (t.Minutes > 0 ? t.Minutes.ToString("00") : "00") + "m:" + (t.Seconds > 0 ? t.Seconds.ToString("00") : "00") + "s";
            _timerText.text = formattedTime;
        }
        ///  PUBLIC API                ///
        public bool CountDownActive;

        public void Init(TimerMediator mediator) 
        { 
            _mediator = mediator;
        }

        public void TimerCanvasEnabled(bool enabled)
        {
        _timerCanvas.enabled = enabled;
        }
    }
}
