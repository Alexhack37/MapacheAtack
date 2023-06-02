using UnityEngine;
using Patterns.Observer.Interfaces;

namespace Patterns.Observer.Components
{
    public class Food : MonoBehaviour, IPickableFood
    {
        [SerializeField]
        public int Value = 1;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Destroy(gameObject);
            }
        }
        public float Pick()
        {
            return Value;
        }


    }
}
