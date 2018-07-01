using EZCameraShake;
using ODT.UI.Util;
using ODT.Util;
using UnityEngine;

namespace ODT.MaG.Gun
{
    public class PlayerGunBehaviour : MonoBehaviour
    {
        [SerializeField]
        private Transform gunProjectilePoint;
        [SerializeField]
        private string projectilePoolTag;
        [SerializeField]
        private float timeBetweenFire = 2;

        private ObjectPoolBehaviour projectilePool;

        protected float nextFireTime;

        private void OnEnable()
        {
            projectilePool = GameObject.FindGameObjectWithTag(projectilePoolTag).GetComponent<ObjectPoolBehaviour>();
        }

        private void Update()
        {
            Vector3 moveInput = new Vector3(UIVirtualInput.GetInput(UIRightVirtualJoystickBehaviour.RIGHT_VIRTUAL_JOYSTICK_HORIZONTAL_VALUE), 0, UIVirtualInput.GetInput(UIRightVirtualJoystickBehaviour.RIGHT_VIRTUAL_JOYSTICK_VERTICAL_VALUE));
            if (moveInput != Vector3.zero)
            {
                Fire();
                CameraShaker.Instance.ShakeOnce(4f, 4f, .1f, 1f);
            }
        }

        private void Fire()
        {
            if (nextFireTime < 0)
            {
                GameObject obj = projectilePool.GetFromPool();
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
    }
}

