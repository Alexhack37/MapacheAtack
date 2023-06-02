using System.Collections.Generic;
using Patterns.Observer.Interfaces;
using UnityEngine;

namespace Patterns.Observer
{
    public class ObserverBarriga : MonoBehaviour, ISubject<float>
    {
        [SerializeField] public float Food = 0f;

       
        public void OnTriggerEnter(Collider other)
        {
            IPickableFood food = other.GetComponent<IPickableFood>();

            if (food != null)
            {
                Food += food.Pick();
                NotifyObservers();
            }
        }

        private List<IObserver<float>> _observers = new List<IObserver<float>>();

        public void AddObserver(IObserver<float> observer)
        {
            _observers.Add(observer);
        }

        public void RemoveObserver(IObserver<float> observer)
        {
            _observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (IObserver<float> observer in _observers)
            {
                observer?.UpdateObserver(Food);
            }
        }
    }
}