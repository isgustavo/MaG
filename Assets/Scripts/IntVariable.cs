using UnityEngine;

namespace ODT.Util.Scriptable
{
    [CreateAssetMenu(menuName = "Objects/Int Variable")]
    public class IntVariable : ScriptableObject
    {
        [SerializeField]
        public int Value;

        [SerializeField]
        private bool resetOnEnable = false;

        private void OnEnable()
        {
            if (resetOnEnable)
            {
                Value = 0;
            }
        }
    }
}
