using Patterns.Observer.Interfaces;
using TMPro;
using UnityEngine;

namespace Patterns.Observer
{
    public class ObserverComida : MonoBehaviour, IObserver<float>
    {
        private void Awake()
        {
            GameObject player = GameObject.FindWithTag("Player");
            ObserverBarriga observerBarriga= player.GetComponent<ObserverBarriga>();
            observerBarriga.AddObserver(this);
        }

        public void UpdateObserver(float data)
        {
            TextMeshProUGUI text = GetComponent<TextMeshProUGUI>();
            text.text = $"Comida: {data}";
        }
    }
}