using ODT.Util;
using UnityEngine;

namespace ODT.MaG
{
    public class ObjectSpawnBehaviour : MonoBehaviour
    {
        [SerializeField]
        private string objectPoolTag;
        [SerializeField]
        private float timeBetweenSpawn;

        private ObjectPoolBehaviour objectPool;
        private SpawnPointBehaviour[] spawnPoints;

        private void OnEnable()
        {
            objectPool = GameObject.FindGameObjectWithTag(objectPoolTag).GetComponent<ObjectPoolBehaviour>();
            spawnPoints = GetComponentsInChildren<SpawnPointBehaviour>();
            
            InvokeRepeating("SpawnObject", 0, timeBetweenSpawn);
        }

        private void SpawnObject()
        {
            GameObject obj = objectPool.GetFromPool();
            if (obj != null)
            {
                obj.transform.position = GetRandomSpawnPoint();
                obj.SetActive(true);
            }
        }

        private Vector3 GetRandomSpawnPoint()
        {
            int randomPoint = Random.Range(0, spawnPoints.Length);
            return spawnPoints[randomPoint].transform.position;
        }
    }
}
