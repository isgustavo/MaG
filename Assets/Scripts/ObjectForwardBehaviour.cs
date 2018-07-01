using UnityEngine;

namespace ODT.Util
{
    public class ObjectForwardBehaviour : MonoBehaviour
    {
        [SerializeField]
        private int ObjectSpeed = 0;

        private void Update()
        {
            transform.position += transform.forward * ObjectSpeed * Time.deltaTime;
        }
    }
}

