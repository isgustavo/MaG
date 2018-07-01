using System;
using UnityEngine;
using UnityEngine.Events;

namespace ODT.Util
{
    [Serializable]
    public class CollisionEvent : UnityEvent<int> { }

    public interface IDamageable
    {
        int Damage();
    }

    public class ObjectCollisionBehaviour : MonoBehaviour, IDamageable
    {
        [SerializeField]
        private int collisionDamage;
        [SerializeField]
        private bool hasCollisionEffect = false;
        [SerializeField]
        private string collsionPoolTag;

        [Header("Events")]
        [SerializeField]
        private CollisionEvent OnCollisionEvent;

        private ObjectPoolBehaviour collisionPool;

        private void Awake()
        {
            if (hasCollisionEffect)
            {
                collisionPool = GameObject.FindGameObjectWithTag(collsionPoolTag).GetComponent<ObjectPoolBehaviour>();
            }
        }

        public int Damage()
        {
            return collisionDamage;
        }

        private void OnTriggerEnter(Collider other)
        {
            IDamageable damageable = other.GetComponent<IDamageable>();
            if (damageable != null)
            {
                ShowCollisionEffect(other.transform.forward);
                OnCollisionEvent.Invoke(other.GetComponent<IDamageable>().Damage());
            }
        }

        private void ShowCollisionEffect(Vector3 hit)
        {
            if (hasCollisionEffect)
            {
                GameObject obj = collisionPool.GetFromPool();
                if (obj != null)
                {
                    obj.transform.position = transform.position;
                    obj.transform.rotation = Quaternion.FromToRotation(Vector3.forward, hit);
                    obj.SetActive(true);
                }
            }
        }
    }
}
