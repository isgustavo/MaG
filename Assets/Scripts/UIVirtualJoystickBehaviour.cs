using UnityEngine;
using UnityEngine.EventSystems;

namespace ODT.UI.Util
{
    public abstract class UIVirtualJoystickBehaviour : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
    {
        //public static string VIRTUAL_JOYSTICK_HORIZONTAL_VALUE = "Horizontal";
        //public static string VIRTUAL_JOYSTICK_VERTICAL_VALUE = "Vertical";

        [Header("Joystick Transforms")]
        [SerializeField]
        protected Transform backgroundTransform;
        [SerializeField]
        protected Transform handleTransform;
        [SerializeField]
        protected float maxMovementRange = 100;

        protected Vector2 pointerStartPosition;

        private void OnEnable()
        {
            UIVirtualInput.AddInput(VirtualJoytickHorizontal(), 0);
            UIVirtualInput.AddInput(VirtualJoytickVertical(), 0);
        }

        private void OnDisable()
        {
            UIVirtualInput.RemoveInput(VirtualJoytickHorizontal());
            UIVirtualInput.RemoveInput(VirtualJoytickVertical());
        }

        private void UpdateUI(Vector3 pointerDelta)
        {
            if (pointerDelta.magnitude > maxMovementRange)
            {
                handleTransform.localPosition = pointerDelta.normalized * maxMovementRange;
            }
            else
            {
                handleTransform.localPosition = pointerDelta;
            }
        }

        private void UpdateInput(Vector3 pointerDelta, float pointerMovementRange)
        {
            pointerDelta.Normalize();
            pointerDelta *= pointerMovementRange;

            UIVirtualInput.UpdateInput(VirtualJoytickHorizontal(), pointerDelta.x);
            UIVirtualInput.UpdateInput(VirtualJoytickVertical(), pointerDelta.y);
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector3 pointerDelta = eventData.position - pointerStartPosition;

            UpdateInput(pointerDelta, Mathf.Min(eventData.position.magnitude / maxMovementRange, 1f));
            UpdateUI(pointerDelta);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            backgroundTransform.transform.position = eventData.position;
            pointerStartPosition = eventData.position;
        }
    
        public void OnPointerUp(PointerEventData eventData)
        {
            handleTransform.position = pointerStartPosition;

            UIVirtualInput.UpdateInput(VirtualJoytickHorizontal(), 0);
            UIVirtualInput.UpdateInput(VirtualJoytickVertical(), 0);
        }

        public abstract string VirtualJoytickHorizontal();
        public abstract string VirtualJoytickVertical();
    }
}

