using UnityEngine;

namespace ODT.Util
{
    public class ParticleBehaviour : MonoBehaviour
    {
        [SerializeField]
        private float lifetime = 1f;

        private ParticleSystem particle;

        private void Awake()
        {
            particle = GetComponent<ParticleSystem>();
        }

        void OnEnable()
        {
            particle.Play();
            Invoke("RemoveObject", lifetime);
        }

        private void RemoveObject()
        {
            gameObject.SetActive(false);
        }
    }
}
