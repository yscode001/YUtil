using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace YUnity
{
    public enum YEventType
    {
        PointerDown, PointerUp, PointerClick,
        BeginDrag, Draging, onEndDrag,
    }

    public partial class YEventListener : MonoBehaviour
    {
        private Action<PointerEventData, object[]> onPointerDown;
        private Action<PointerEventData, object[]> onPointerUp;
        private Action<PointerEventData, object[]> onPointerClick;

        private Action<PointerEventData, object[]> onBeginDrag;
        private Action<PointerEventData, object[]> onDrag;
        private Action<PointerEventData, object[]> onEndDrag;

        private object[] args = null;

        public void SetupAction(YEventType eventType, Action<PointerEventData, object[]> action, object[] args)
        {
            this.args = args;
            switch (eventType)
            {
                case YEventType.PointerDown:
                    onPointerDown = action; break;
                case YEventType.PointerUp:
                    onPointerUp = action; break;
                case YEventType.PointerClick:
                    onPointerClick = action; break;
                case YEventType.BeginDrag:
                    onBeginDrag = action; break;
                case YEventType.Draging:
                    onDrag = action; break;
                case YEventType.onEndDrag:
                    onEndDrag = action; break;
                default: break;
            }
        }
    }
    public partial class YEventListener : IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
    {
        public void OnPointerDown(PointerEventData eventData)
        {
            onPointerDown?.Invoke(eventData, args);
        }
        public void OnPointerUp(PointerEventData eventData)
        {
            onPointerUp?.Invoke(eventData, args);
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            onPointerClick?.Invoke(eventData, args);
        }
    }
    public partial class YEventListener : IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public void OnBeginDrag(PointerEventData eventData)
        {
            onBeginDrag?.Invoke(eventData, args);
        }
        public void OnDrag(PointerEventData eventData)
        {
            onDrag?.Invoke(eventData, args);
        }
        public void OnEndDrag(PointerEventData eventData)
        {
            onEndDrag?.Invoke(eventData, args);
        }
    }
}