using ODT.UI.Util;
using ODT.Util;
using UnityEngine;

namespace ODT.MaG.Gun
{
    [RequireComponent(typeof (BoxCollider))]
    public class PlayerGunBehaviour : MonoBehaviour
    {
        [SerializeField]
        private Transform[] gunProjectilePoints;
        [SerializeField]
        private string projectilePoolTag;
        [SerializeField]
        private float timeBetweenFire = 2;

        private ObjectPoolBehaviour projectilePool;

        protected float nextFireTime;

        private Transform playerArm;

        private BoxCollider boxCollider;

        private void OnEnable()
        {
            projectilePool = GameObject.FindGameObjectWithTag(projectilePoolTag).GetComponent<ObjectPoolBehaviour>();
            boxCollider = GetComponent<BoxCollider>();
        }

        public void SetPlayerArm(Transform value)
        {
            boxCollider.enabled = false;
            playerArm = value;
        }

        public void Drop()
        {
            playerArm = null;
            Invoke("EnableCollision", 1f);           
        }

        private void EnableCollision()
        {
            if (gameObject.activeInHierarchy)
            {
                boxCollider.enabled = true;
            }
        }

        private void Update()
        {
            if (playerArm != null)
            {
                transform.position = playerArm.position;
                transform.rotation = playerArm.rotation;

                Vector3 moveInput = new Vector3(UIVirtualInput.GetInput(UIRightVirtualJoystickBehaviour.RIGHT_VIRTUAL_JOYSTICK_HORIZONTAL_VALUE), 0, UIVirtualInput.GetInput(UIRightVirtualJoystickBehaviour.RIGHT_VIRTUAL_JOYSTICK_VERTICAL_VALUE));
                if (moveInput != Vector3.zero)
                {
                    Fire();
                }
            }
        }

        private void Fire()
        {
            if (nextFireTime < 0)
            {
                for (int i = 0; i < gunProjectilePoints.Length; i++)
                {
                    GameObject obj = projectilePool.GetFromPool();
                    if (obj != null)
                    {
                        obj.transform.position = gunProjectilePoints[i].position;
                        obj.transform.rotation = gunProjectilePoints[i].rotation;
                        obj.gameObject.SetActive(true);
                    }
                }

                nextFireTime = timeBetweenFire;
            }
            else
            {
                nextFireTime -= Time.deltaTime;
            }
        }
    }
}

