using System;
using UnityEngine;

namespace Patterns.DirtyFlag.GameSaving.DataClass
{
    [Serializable]
    public class SaveableTrophyData : ASaveableGameObjectData
    {
        public int Value;
        public SaveableTrophyData(TrophyComponent trophy) : base(trophy.gameObject)
        {
            Value = trophy.Value;
        }

        new public void ToGameObject(MonoBehaviour trophy)
        {
            base.ToGameObject(trophy);
            ((TrophyComponent)trophy).Value = Value;
        }
    }
}