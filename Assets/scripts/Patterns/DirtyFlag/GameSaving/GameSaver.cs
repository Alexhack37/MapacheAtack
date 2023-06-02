using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Patterns.DirtyFlag.GameSaving.Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Patterns.DirtyFlag.GameSaving
{
    public class GameSaver : MonoBehaviour
    {
        #region Monobehaviour code
        [SerializeField]
        public bool RestoreOnLoad = true;

        private void Awake()
        {
            saveFile = Application.dataPath + "/savedata.save";
            objects = new Dictionary<string, ISaveableGameObjectData>();

            if (RestoreOnLoad)
            {
                LoadGame();
            }

            StartCoroutine(SaveGame());
        }
        #endregion

        #region Dirty Flag Pattern
        private bool sceneIsDirty = false;

        public void SetSceneDirty()
        {
            sceneIsDirty = true;
        }
        #endregion

        #region Saving gameobjects code
        private string saveFile;
        private Dictionary<string, ISaveableGameObjectData> objects;

        private IEnumerator SaveGame()
        {
            while (true)
            {
                if (sceneIsDirty)
                {
                    Debug.Log("Scene is dirty, saving");
                    Scene scene = gameObject.scene;

                    GameObject[] sceneObjects = scene.GetRootGameObjects();
                    foreach (GameObject sceneObject in sceneObjects)
                    {
                        ISaveableGameObject obj = sceneObject.GetComponent<ISaveableGameObject>();
                        if (obj != null && obj.NeedsSaving())
                        {
                            Debug.Log($"Saving {sceneObject.name}: {JsonUtility.ToJson(obj.GetSavedObject())}");
                            objects[sceneObject.name] = obj.GetSavedObject();
                            yield return null;
                        }
                    }

                    BinaryFormatter formatter = new BinaryFormatter();
                    FileStream fileStream = File.Create(saveFile);
                    formatter.Serialize(fileStream, objects);
                    fileStream.Flush();
                    fileStream.Close();
                    sceneIsDirty = false;
                }
                else
                {
                    Debug.Log("No need to save, scene is not dirty");
                }

                yield return new WaitForSecondsRealtime(10);
            }
        }

        public void LoadGame()
        {
            if (File.Exists(saveFile))
            {
                Debug.Log($"{saveFile} found, starting restore.");
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream fileStream = File.OpenRead(saveFile);
                objects = (Dictionary<string, ISaveableGameObjectData>)formatter.Deserialize(fileStream);
                fileStream.Close();

                foreach (var objectData in objects)
                {
                    GameObject gameObject = GameObject.Find(objectData.Key);
                    if (gameObject != null)
                    {
                        ISaveableGameObject saveableGameObject = gameObject.GetComponent<ISaveableGameObject>();
                        if (saveableGameObject != null)
                        {
                            Debug.Log($"Restoring {objectData.Key}: {JsonUtility.ToJson(saveableGameObject)}");
                            saveableGameObject.ToGameObject(objectData.Value);
                        }
                        else
                        {
                            Debug.LogError($"{objectData.Key} is not saveable.");
                        }

                    }
                }
            }
            else
            {
                Debug.Log("No savefile found");
            }
        }
        #endregion
    }
}