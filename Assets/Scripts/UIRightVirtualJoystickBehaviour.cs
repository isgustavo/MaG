namespace ODT.UI.Util
{
    public class UIRightVirtualJoystickBehaviour : UIVirtualJoystickBehaviour
    {
        public static string RIGHT_VIRTUAL_JOYSTICK_HORIZONTAL_VALUE = "RightHorizontal";
        public static string RIGHT_VIRTUAL_JOYSTICK_VERTICAL_VALUE = "RightVertical";

        public override string VirtualJoytickHorizontal()
        {
            return RIGHT_VIRTUAL_JOYSTICK_HORIZONTAL_VALUE;
        }

        public override string VirtualJoytickVertical()
        {
            return RIGHT_VIRTUAL_JOYSTICK_VERTICAL_VALUE;
        }
    }
}