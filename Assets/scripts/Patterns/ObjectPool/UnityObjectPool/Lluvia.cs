using System;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

namespace Patterns.ObjectPool.UnityObjectPool
{
    public class LLuvia : MonoBehaviour
    {
        public Gota gotaPrototype;
        public int initialNumberOfGotas = 1;
        public int maxNumberOfGotas = 10;
        public bool snowing = false;
        public float gotasPerSecond;
        public float maxGotaSpeed = 1f;
        public float spreadAreaExtent = 30f;

        private float _halfSpreadAreaExtent;
        private float _lastGota;
        private ObjectPool<Gota> _gotasPool;

        private void Awake()
        {
            _gotasPool = new ObjectPool<Gota>(CreateGota, GetGota, ReleaseGota, DestroyGota, false, initialNumberOfGotas, maxNumberOfGotas);
            _halfSpreadAreaExtent = spreadAreaExtent / 2;
        }

        private void DestroyGota(Gota obj)
        {
            Destroy(obj.gameObject);
        }

        private Gota CreateGota()
        {
            Gota gota = Instantiate(gotaPrototype);
            return gota;
        }

        private void GetGota(Gota obj)
        {
            obj.gameObject.SetActive(true);

            float xDisp = Random.value * spreadAreaExtent - _halfSpreadAreaExtent;
            float zDisp = Random.value * spreadAreaExtent - _halfSpreadAreaExtent;
            obj.transform.localPosition = Vector3.zero + Vector3.right * xDisp + Vector3.forward * zDisp;
            obj.speed = 1 + (Random.value * (maxGotaSpeed - 1));
            obj.transform.SetParent(transform, false);
            obj.OnFloorReached = OnFloorReached;
        }

        private void OnFloorReached(object sender, EventArgs e)
        {
            Gota gota = (Gota)sender;
            _gotasPool.Release(gota);
        }

        private void ReleaseGota(Gota obj)
        {
            obj.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (snowing)
            {
                _lastGota += Time.deltaTime;
                if (_lastGota >= 1)
                {
                    for (int i = 0; i < gotasPerSecond; i++)
                    {
                        _gotasPool.Get();
                    }
                }
            }
        }

        private void OnGUI()
        {
            GUI.Label(new Rect(10, 10, 300, 30), $"Total pool size: {_gotasPool.CountAll}");
            GUI.Label(new Rect(10, 40, 300, 30), $"Active pool size: {_gotasPool.CountActive}");
        }
    }
}
