using System;
using Patterns.DirtyFlag.GameSaving.DataClass;
using Patterns.DirtyFlag.GameSaving.Interfaces;
using Patterns.DirtyFlag.Interfaces;
using UnityEngine;
namespace Components
{
    public class Trophies : MonoBehaviour
    {
        [SerializeField]
        public float Trophy = 0f;

        public EventHandler<float> TrophyPicked;

        public void OnTriggerEnter(Collider other)
        {
            IPickableTrophy trophy = other.GetComponent<IPickableTrophy>();

            if (trophy != null)
            {
                Trophy += trophy.Pick();
                TrophyPicked?.Invoke(this, Trophy);
               
            }
        }
    }
}
