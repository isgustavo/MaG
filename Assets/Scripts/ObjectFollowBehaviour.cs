using UnityEngine;

namespace ODT.MaG
{
    public class ObjectFollowBehaviour : MonoBehaviour
    {
        [SerializeField]
        private string targetFollowTag;
        [SerializeField]
        private float objectSpeed;

        private Transform targetFollow;

        private void Awake()
        {
            targetFollow = GameObject.FindGameObjectWithTag(targetFollowTag).transform;
        }

        private void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position, targetFollow.position, objectSpeed * Time.deltaTime);
        }

    }
}
