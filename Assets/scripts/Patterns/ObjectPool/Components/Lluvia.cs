using System;
using Patterns.ObjectPool.Interfaces;
using UnityEngine;
using UnityEngine.Assertions;
using Random = UnityEngine.Random;

namespace Patterns.ObjectPool.Components
{
    public class Lluvia : MonoBehaviour
    {
        public MonoBehaviour gotaPrototype;
        public int initialNumberOfGotas = 1;
        public bool allowAddNewGotas = false;
        public bool lloviendo = false;
        public float gotasPerSecond;
        public float maxGotaSpeed = 1f;
        public float spreadAreaExtent = 30f;

        private float _halfSpreadAreaExtent;
        private float _lastGota;
        private ObjectPool _gotasPool;

        private void Awake()
        {
            Assert.IsTrue(gotaPrototype is IPooledObject);
            _gotasPool = new ObjectPool((IPooledObject)gotaPrototype, initialNumberOfGotas, allowAddNewGotas);
            _halfSpreadAreaExtent = spreadAreaExtent / 2;
        }

        private void Update()
        {
            if (lloviendo && !MenuPausa.GameIsPaused)
            {
                _lastGota += Time.deltaTime;
                if (_lastGota >= 1)
                {
                    for (int i = 0; i < gotasPerSecond; i++)
                    {
                        Gota gota = CreateGota();
                    }
                }
            }
        }

        private Gota CreateGota()
        {
            Gota gota = (Gota)_gotasPool.Get();

            if (gota)
            {
                gota.pool = _gotasPool;
                float xDisp = Random.value * spreadAreaExtent - _halfSpreadAreaExtent;
                float zDisp = Random.value * spreadAreaExtent - _halfSpreadAreaExtent;
                gota.transform.localPosition = Vector3.zero + Vector3.right * xDisp + Vector3.forward * zDisp;
                gota.speed = maxGotaSpeed;
                gota.transform.SetParent(transform, false);
            }

            return gota;
        }

        // DEBUG
        /*private void OnGUI()
        {
            GUI.Label(new Rect(10, 10, 300, 30), $"Total pool size: {_gotasPool.GetCount()}");
            GUI.Label(new Rect(10, 40, 300, 30), $"Active pool size: {_gotasPool.GetActive()}");
        }*/
    }
}
