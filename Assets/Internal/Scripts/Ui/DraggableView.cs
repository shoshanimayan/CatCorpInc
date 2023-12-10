using UnityEngine;
using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using Player;

namespace Ui
{
    [RequireComponent(typeof(RectTransform))]
    public class DraggableView : MonoBehaviour, IView, IDragHandler, IPointerDownHandler, IEndDragHandler
    {


        ///  INSPECTOR VARIABLES       ///
        [SerializeField] private RectTransform _top;
        ///  PRIVATE VARIABLES         ///
        private Vector2 _lastPos;
        private RectTransform _rectTransform;
        private DraggableMediator _mediator;
        ///  PRIVATE METHODS           ///
        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        private void Update()
        {
            if (_top.position.y<20 && _mediator.CanDrag)
            { 
                _mediator.Complete();
            }
        }

        ///  PUBLIC API                ///
        public void OnDrag(PointerEventData eventData)
        {

            if (eventData.position.y < _lastPos.y && _mediator.CanDrag)
            {
                _mediator.ShakeScreen(true);

                float reduction = _lastPos.y - eventData.position.y;
                _rectTransform.localPosition = new Vector2(_rectTransform.localPosition.x, _rectTransform.localPosition.y - reduction);
            }
            _lastPos = eventData.position;

        } 


       public void Init(DraggableMediator mediator)
        { _mediator = mediator; }

        public void OnPointerDown(PointerEventData eventData)
        {
            _lastPos = eventData.position;

        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _mediator.ShakeScreen(false);
        }
    }
}
