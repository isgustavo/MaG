using ODT.Util.Scriptable;
using UnityEngine;

namespace ODT.MaG.Player
{
    public class PlayerComboBehaviour : MonoBehaviour
    {
        [SerializeField]
        private IntVariable comboValue;

        [SerializeField]
        private GameEvent OnComboUpdateEvent;

        public void IncreaseCombo()
        {
            comboValue.Value += 1;
            OnComboUpdateEvent.Raise();
        }
    }
}
