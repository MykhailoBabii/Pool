using System;
using Core.States;
using Core.Utilities;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace UI
{

    public class ScreenTouchHandler : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        public CustomProperty<Vector3> DragPoint = new(new Vector3());
        [HideInInspector] public bool CanTouch;

        [Inject] private readonly IStateMachine<ApplicationStates> _applicationStateMachine;
        [SerializeField] private LayerMask _touchZoneLayerMask;


        public void OnDrag(PointerEventData eventData)
        {
            SetTouchFromRay(eventData.position);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            
        }

        private void SetTouchFromRay(Vector3 position)
        {
            if (CanTouch == false) return;

            var ray = UnityEngine.Camera.main.ScreenPointToRay(position);
            if (Physics.Raycast(ray, out var hit, 1000f, _touchZoneLayerMask))
            {
                DragPoint.SetValue(hit.point);
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (CanTouch == false) return;
            _applicationStateMachine.SwitchToState(ApplicationStates.BallsMoving);
        }
    }
}
