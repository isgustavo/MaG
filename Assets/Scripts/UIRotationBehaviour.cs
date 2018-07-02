using UnityEngine;

namespace ODT.MaG.UI
{
    public class UIRotationBehaviour : MonoBehaviour
    {
        private void OnEnable()
        {
            iTween.RotateBy(gameObject, iTween.Hash(
                     "z", -1.0f,
                     "time", 3f,
                     "easetype", "linear",
                     "looptype", iTween.LoopType.loop
                 ));
        }
    }
}
