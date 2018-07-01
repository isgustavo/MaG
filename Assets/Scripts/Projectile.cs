using UnityEngine;

namespace ODT.MaG.Gun
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField]
        private float projectileLifetime = 3;
        [SerializeField]
        private float projectileSpeed = 10;
        [SerializeField]
        private float projectileDamage = 1;
        [SerializeField]
        public LayerMask collisionMask;

        private float lifetimeTime = 0;

        private void OnEnable()
        {
            lifetimeTime = projectileLifetime;
        }

        protected void Update()
        {
            if (lifetimeTime < 0)
            {
                gameObject.SetActive(false);
            }
            else
            {
                transform.Translate(Vector3.forward * projectileSpeed * Time.deltaTime);
                lifetimeTime -= Time.deltaTime;
            }
        }
        
    }
}
