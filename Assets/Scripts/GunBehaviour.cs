using ODT.UI.Util;
using System.Collections.Generic;
using UnityEngine;

namespace ODT.MaG.Gun
{
    public class GunBehaviour : MonoBehaviour
    {
        [SerializeField]
        private Transform gunProjectilePoint;
        [SerializeField]
        private GameObject projectilePrefab;
        [SerializeField]
        private float timeBetweenFire = 2;

        private List<Projectile> projectilePool = new List<Projectile>();
        private int initPoolSize = 15;

        protected float nextFireTime;

        private void OnEnable()
        {
            InitPool();
        }

        private void Update()
        {
            Vector3 moveInput = new Vector3(UIVirtualInput.GetInput(UIRightVirtualJoystickBehaviour.RIGHT_VIRTUAL_JOYSTICK_HORIZONTAL_VALUE), 0, UIVirtualInput.GetInput(UIRightVirtualJoystickBehaviour.RIGHT_VIRTUAL_JOYSTICK_VERTICAL_VALUE));
            if (moveInput != Vector3.zero)
            {
                Fire();
            }
        }

        private void Fire()
        {
            if (nextFireTime < 0)
            {
                Projectile obj = GetObjectFromPool();
                obj.transform.position = gunProjectilePoint.position;
                obj.transform.rotation = gunProjectilePoint.rotation;
                obj.gameObject.SetActive(true);
                nextFireTime = timeBetweenFire;
            }
            else
            {
                nextFireTime -= Time.deltaTime;
            }
        }

        private void InitPool()
        {
            for (int i = 0; i < initPoolSize; i++)
            {
                GameObject newObj = Instantiate(projectilePrefab);
                newObj.SetActive(false);
                projectilePool.Add(newObj.GetComponent<Projectile>());
            }
        }

        private Projectile GetObjectFromPool()
        {
            for (int i = 0; i < projectilePool.Count; i++)
            {
                if (!projectilePool[i].isActiveAndEnabled)
                {
                    return projectilePool[i];
                }
            }

            GameObject newObj = Instantiate(projectilePrefab);
            newObj.SetActive(false);
            Projectile projectile = newObj.GetComponent<Projectile>();
            projectilePool.Add(projectile);
            return projectile;

        }
    }
}

