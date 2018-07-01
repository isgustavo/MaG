using System;
using UnityEngine;
using UnityEngine.Events;

namespace ODT.Util
{
    [Serializable]
    public class DestroyEvent : UnityEvent<Vector3> { }

    public class ObjectHealthBehaviour : MonoBehaviour
    {
        [SerializeField]
        private int initialHealth;
        [SerializeField]
        private DestroyEvent OnDestroyEvent;

        protected int healht;

        private void OnEnable()
        {
            healht = initialHealth;
        }

        public void OnDamageEvent(int value, Vector3 hit)
        {
            healht -= value;
            if (healht <= 0)
            {
                OnDestroyEvent.Invoke(hit);
            }
        }

    }
}
