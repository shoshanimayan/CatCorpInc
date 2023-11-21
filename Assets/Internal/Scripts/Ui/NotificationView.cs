using UnityEngine;
using Core;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using DG.Tweening;
using UnityEngine.UIElements;

namespace Ui
{
    [RequireComponent(typeof(RectTransform))]
	public class NotificationView: MonoBehaviour,IView
	{

		///  INSPECTOR VARIABLES       ///
		[SerializeField] TextMeshProUGUI _notificationLabel;
		[SerializeField] private float _yOffset;
        [SerializeField]private  float _popupTime;
        [SerializeField] private float _notificationTime;
        [SerializeField] private RectTransform _offset;
        ///  PRIVATE VARIABLES         ///
        private Vector2 _origin;
        private NotificationMediator _mediator;
        private RectTransform _notificationRectTransform;
        

        ///  PRIVATE METHODS           /// <summary>
		private void OnValidate()
        {
            _popupTime = Mathf.Clamp(_popupTime, 0, float.MaxValue);
            _notificationTime= Mathf.Clamp(_notificationTime, 0, float.MaxValue);
            _yOffset = Mathf.Clamp(_yOffset, float.MinValue, 0);


        }
        private void Awake()
        {

            _notificationRectTransform = GetComponent<RectTransform>();
            _origin = _notificationRectTransform.position;
           

        }
        ///  PUBLIC API                ///
        public void SetText(string text)
		{
			_notificationLabel.text = text;
		}

		public void Notify()
		{
            DOTween.Sequence().Append(_notificationRectTransform.DOMove(_offset.position,_popupTime))
                .AppendInterval(_notificationTime)
                .Append(_notificationRectTransform.DOMove(_origin, _popupTime)).AppendCallback(()=>_mediator.CheckForCompletetion()).SetId("notification");



        }


        public void Initializer(NotificationMediator mediator)
        { 
        _mediator = mediator;
        }

        public void KillNotification()
        {
            DOTween.Kill("notification");
            transform.position= _origin;
        }
	}
}
