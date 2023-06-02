using System;
using Patterns.DirtyFlag.GameSaving;
using Patterns.DirtyFlag.GameSaving.DataClass;
using Patterns.DirtyFlag.GameSaving.Interfaces;
using Patterns.DirtyFlag.Interfaces;
using UnityEngine;

[Serializable]
public class TrophyComponent : MonoBehaviour, IPickableTrophy, ISaveableGameObject
{
    [SerializeField] public int Value = 1;

 
    public float Pick()
    {
        dirtyFlag = true;
        gameObject.SetActive(false);
        FindObjectOfType<GameSaver>()?.GetComponent<GameSaver>()?.SetSceneDirty();
        return Value;
    }

    public float GetValue()
    {
        return Value;
    }

    private bool dirtyFlag;

    public bool NeedsSaving()
    {
        return dirtyFlag;
    }

    public ISaveableGameObjectData GetSavedObject()
    {
        dirtyFlag = false;
        return new SaveableTrophyData(this);
    }

    public void ToGameObject(ISaveableGameObjectData data)
    {
        SaveableTrophyData trophyData = (SaveableTrophyData)data;
        trophyData.ToGameObject(this);
    }
}
