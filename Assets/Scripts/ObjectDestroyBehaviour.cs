using UnityEngine;

namespace ODT.Util
{
    public class ObjectDestroyBehaviour : MonoBehaviour
    {
        [SerializeField]
        private bool hasDestroyEffect = false;
        [SerializeField]
        private string destroyPoolTag;

        private ObjectPoolBehaviour destroyPool;

        private void Awake()
        {
            if (hasDestroyEffect)
            {
                destroyPool = GameObject.FindGameObjectWithTag(destroyPoolTag).GetComponent<ObjectPoolBehaviour>();
            }
        }

        public void OnDestroyEvent()
        {
            if (hasDestroyEffect)
            {
                GameObject obj = destroyPool.GetFromPool();
                if (obj != null)
                {
                    obj.transform.position = transform.position;
                    obj.SetActive(true);
                }
            }
            gameObject.SetActive(false);
        }
    }
}
