using System;
using UnityEngine;
using UnityEngine.Events;

namespace ODT.Util
{
    [Serializable]
    public class CollisionEvent : UnityEvent<int, Vector3> { }

    public interface IDamageable
    {
        int Damage();
    }

    public class ObjectCollisionBehaviour : MonoBehaviour, IDamageable
    {
        [SerializeField]
        private int collisionDamage;

        [SerializeField]
        private CollisionEvent OnCollisionEvent;
        
        public int Damage()
        {
            return collisionDamage;
        }

        private void OnTriggerEnter(Collider other)
        {
            IDamageable damageable = other.GetComponent<IDamageable>();
            if (damageable != null)
            {
                OnCollisionEvent.Invoke(other.GetComponent<IDamageable>().Damage(), other.transform.forward);
            }
        }
    }
}
