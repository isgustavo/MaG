using System.Collections.Generic;
using UnityEngine;

namespace ODT.Util
{
    public class ObjectPoolBehaviour : MonoBehaviour
    {
        [SerializeField]
        private GameObject objectPrefab;
        [SerializeField]
        private bool prewarm;
        [SerializeField]
        private int prewarmCount;
        [SerializeField]
        private int maxSize = -1;

        private List<GameObject> objectPool = new List<GameObject>();

        private void OnEnable()
        {
            if (prewarm)
            {
                for (int i = 0; i < prewarmCount; i++)
                {
                    GameObject obj = CreateObj();
                    obj.transform.position = Random.insideUnitSphere * 11;
                    obj.SetActive(false);
                    
                }
            }   
        }

        public GameObject GetFromPool()
        {
            for (int i = 0; i < objectPool.Count; i++)
            {
                if (!objectPool[i].activeInHierarchy)
                {
                    return objectPool[i];
                }
            }

            if (maxSize == -1 || objectPool.Count < maxSize)
            {
                return CreateObj();
            }

            return null;
            
        }

        private GameObject CreateObj()
        {
            GameObject newObj = Instantiate(objectPrefab, transform);
            objectPool.Add(newObj);
            return newObj;
        }
    }
}


