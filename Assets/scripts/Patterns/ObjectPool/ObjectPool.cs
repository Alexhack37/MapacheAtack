using System.Collections.Generic;
using Patterns.ObjectPool.Interfaces;
using UnityEngine;
///////////////////////////////////////////
/// Version copiada de clase
/// ////////////////////////////////////////
namespace Patterns.ObjectPool
{
    public class ObjectPool : IPool
    {
        private IPooledObject _objectPrototype;
        private readonly bool _allowAddNew;

        private List<IPooledObject> _objects;

        private int _activeObjects;

        public ObjectPool(IPooledObject objectPrototype, int initialNumberOfElements, bool allowAddNew)
        {
            _objectPrototype = objectPrototype;
            _allowAddNew = allowAddNew;
            _objects = new List<IPooledObject>(initialNumberOfElements);
            _activeObjects = 0;

            for (int i = 0; i < initialNumberOfElements; i++)
            {
                _objects.Add(CreateObject());
            }
        }


        public IPooledObject Get()
        {
            for (int i = 0; i < _objects.Count; i++)
            {
                if (!_objects[i].Active)
                {
                    _objects[i].Active = true;
                    _activeObjects += 1;
                    return _objects[i];
                }
            }

            if (_allowAddNew)
            {
                IPooledObject newObj = CreateObject();
                newObj.Active = true;
                _objects.Add(newObj);

                _activeObjects += 1;
                return newObj;
            }

            return null;
        }

        public void Release(IPooledObject obj)
        {
            obj.Active = false;
            _activeObjects -= 1;
            obj.Reset();
        }

        private IPooledObject CreateObject()
        {
            IPooledObject newObj = _objectPrototype.Clone();
            return newObj;
        }

        public int GetCount()
        {
            return _objects.Count;
        }

        public int GetActive()
        {
            return _activeObjects;
        }
    }
}

