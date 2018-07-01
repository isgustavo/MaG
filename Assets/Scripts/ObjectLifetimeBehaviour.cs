using UnityEngine;

namespace ODT.Util
{
    public class ObjectLifetimeBehaviour : MonoBehaviour
    {
        [SerializeField]
        private float objectLifetime = 0;

        public void OnEnable()
        {
            Invoke("RemoveObject", objectLifetime);
        }

        public void RemoveObject()
        {
            gameObject.SetActive(false);
        }

        public void OnDisable()
        {
            CancelInvoke("RemoveObject");
        }
    }
}