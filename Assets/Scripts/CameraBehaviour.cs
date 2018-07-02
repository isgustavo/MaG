using UnityEngine;

namespace ODT.Util
{
    public class CameraBehaviour : MonoBehaviour
    {
        [SerializeField]
        private float smoothSpeed = 0.125f;
        [SerializeField]
        private Vector3 offset;
        [SerializeField]
        private bool autoTarget;

        private Transform target;

        private void OnEnable()
        {
            if (autoTarget)
            {
                target = GameObject.FindGameObjectWithTag("Player").transform;
            }
        }

        public void SetTarget(Transform value)
        {
            target = value;
        }

        private void FixedUpdate()
        {
            if (target != null)
            {
                transform.position = Vector3.Lerp(transform.position, target.position + offset, smoothSpeed * Time.deltaTime);
                transform.LookAt(target);
            }
        }
    }
}
