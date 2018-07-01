using ODT.UI.Util;
using UnityEngine;

namespace ODT.MaG.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerRotationBehaviour : MonoBehaviour
    {
        [SerializeField]
        private float playerRotationSpeed;

        private Rigidbody rb;

        private void OnEnable()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            Vector3 moveInput = new Vector3(UIVirtualInput.GetInput(UIRightVirtualJoystickBehaviour.RIGHT_VIRTUAL_JOYSTICK_HORIZONTAL_VALUE), 0, UIVirtualInput.GetInput(UIRightVirtualJoystickBehaviour.RIGHT_VIRTUAL_JOYSTICK_VERTICAL_VALUE));
            if (moveInput != Vector3.zero)
            {
                rb.MoveRotation(Quaternion.LookRotation(moveInput, Vector3.up));
            } 
        }
    }
}
