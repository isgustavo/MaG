namespace ODT.UI.Util
{
    public class UILeftVirtualJoystickBehaviour : UIVirtualJoystickBehaviour
    {
        public static string LEFT_VIRTUAL_JOYSTICK_HORIZONTAL_VALUE = "LeftHorizontal";
        public static string LEFT_VIRTUAL_JOYSTICK_VERTICAL_VALUE = "LeftVertical";

        public override string VirtualJoytickHorizontal()
        {
            return LEFT_VIRTUAL_JOYSTICK_HORIZONTAL_VALUE;
        }

        public override string VirtualJoytickVertical()
        {
            return LEFT_VIRTUAL_JOYSTICK_VERTICAL_VALUE;
        }
    }
}
