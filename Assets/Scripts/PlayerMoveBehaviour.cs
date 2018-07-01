using ODT.UI.Util;
using UnityEngine;

namespace ODT.MaG.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMoveBehaviour : MonoBehaviour
    {
        [SerializeField]
        private float playerMoveSpeed;

        private Rigidbody rb;

        private void OnEnable()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            Vector3 moveInput = new Vector3(UIVirtualInput.GetInput(UILeftVirtualJoystickBehaviour.LEFT_VIRTUAL_JOYSTICK_HORIZONTAL_VALUE), 0, UIVirtualInput.GetInput(UILeftVirtualJoystickBehaviour.LEFT_VIRTUAL_JOYSTICK_VERTICAL_VALUE));
            Vector3 moveVelocity = moveInput.normalized * playerMoveSpeed;

            rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
        }
    }
}
