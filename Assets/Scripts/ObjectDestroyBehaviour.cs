using ODT.Util.Scriptable;
using UnityEngine;

namespace ODT.Util
{
    public class ObjectDestroyBehaviour : MonoBehaviour
    {
        [SerializeField]
        private bool hasDestroyEffect = false;
        [SerializeField]
        private string destroyPoolTag;

        [Header("Events")]
        [SerializeField]
        private GameEvent OnObjectDestroyEvent;

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

            if (OnObjectDestroyEvent != null)
            {
                OnObjectDestroyEvent.Raise();
            }
            
            gameObject.SetActive(false);
        }
    }
}
