using System;
using UnityEngine;
using UnityEngine.Events;

namespace ODT.Util
{
    public class ObjectHealthBehaviour : MonoBehaviour
    {
        [SerializeField]
        private int initialHealth;
        [SerializeField]
        private UnityEvent OnDestroyEvent;

        protected int healht;

        private void OnEnable()
        {
            healht = initialHealth;
        }

        public void OnDamageEvent(int value)
        {
            healht -= value;
            if (healht <= 0)
            {
                OnDestroyEvent.Invoke();
            }
        }

    }
}
