using Patterns.Observer.Interfaces;
using UnityEngine;

namespace Patterns.Observer
{
    public class ObserverSoundSystem : MonoBehaviour, IObserver<float>
    {
        private AudioSource comidaAudio;

        private void Awake()
        {
            GameObject player = GameObject.FindWithTag("Player");
            ObserverBarriga observerBarriga = player.GetComponent<ObserverBarriga>();
            observerBarriga.AddObserver(this);

            comidaAudio = GetComponent<AudioSource>();
        }

        public void UpdateObserver(float data)
        {
            comidaAudio?.Play();
        }
    }
}