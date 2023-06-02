using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Patterns.DirtyFlag.GameSaving.Interfaces
{

    public interface ISaveableGameObject
    {
        public bool NeedsSaving();

        public ISaveableGameObjectData GetSavedObject();
        public void ToGameObject(ISaveableGameObjectData data);
    }
}