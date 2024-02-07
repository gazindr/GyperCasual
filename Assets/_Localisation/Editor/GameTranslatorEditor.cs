using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

namespace KreizTranslation
{
    [CustomEditor(typeof(GameTranslator))]
    public class GameTranslatorEditor : Editor
    {
        GameTranslator myTarget;
        public static bool forceAllMode = false;
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            myTarget = (GameTranslator)target;
            if (GUILayout.Button("Translate All from ENG to RU"))
            {
                if (myTarget.UIHolderObject == null)
                {
                    forceAllMode = false;
                    myTarget.StartTranslationProcess(GetAllObjectsInScene());
                }
                else
                {
                    //forceAllMode = true;
                    myTarget.StartTranslationProcess(GetAllObjectsInChild(myTarget.UIHolderObject));
                }
            }
            if (myTarget.DeleteAllTextLanguages)
            {
                if (GUILayout.Button("Delete All Text Lanugages"))
                {
                    forceAllMode = false;
                    List<GameObject> allObjects = GetAllObjectsInScene();
                    int destrCounter = 0;
                    for (int i = 0; i < allObjects.Count; i++)
                    {
                        if (allObjects[i].TryGetComponent<TextLanguage>(out TextLanguage TL))
                        {
                            DestroyImmediate(TL);
                            destrCounter++;
                        }
                    }
                    Debug.Log("Destroyed:" + destrCounter);
                }
            }
            if (myTarget.TMP_Font != null || myTarget.UnityUI_Font != null)
            {
                if (GUILayout.Button("Change font"))
                {
                    forceAllMode = true;
                    List<GameObject> allObjects = GetAllObjectsInScene();
                    for (int i = 0; i < allObjects.Count; i++)
                    {
                        if (allObjects[i].TryGetComponent<TextLanguage>(out TextLanguage TL))
                        {
                            TL.UpdateFont(myTarget.TMP_Font, myTarget.UnityUI_Font);
                        }
                    }
                    forceAllMode = false;
                }
            }
            if (GUILayout.Button("Switch to RU"))
            {
                forceAllMode = true;
                List<GameObject> allObjects = GetAllObjectsInScene();
                for (int i = 0; i < allObjects.Count; i++)
                {
                    if (allObjects[i].TryGetComponent<TextLanguage>(out TextLanguage TL))
                    {
                        TL.DisplayRU();
                    }
                }
                forceAllMode = false;
            }
            if (GUILayout.Button("Switch to ENG"))
            {
                forceAllMode = true;
                List<GameObject> allObjects = GetAllObjectsInScene();
                for (int i = 0; i < allObjects.Count; i++)
                {
                    if (allObjects[i].TryGetComponent<TextLanguage>(out TextLanguage TL))
                    {
                        TL.DisplayENG();
                    }
                }
                forceAllMode = false;
            }
            GUILayout.Label(myTarget.LabelText);
        }
        private static List<GameObject> GetAllObjectsInScene()
        {
            List<GameObject> objectsInScene = new List<GameObject>();

            GameObject[] rootGOs = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();

            objectsInScene.AddRange(rootGOs);
            for (int i = 0; i < rootGOs.Length; i++)
            {
                if (rootGOs[i].GetComponent<DoNotTranslate>() && !forceAllMode)
                    continue;
                //Debug.Log(i+ "RootGO:"+rootGOs[i].transform.name);
                objectsInScene.Add(rootGOs[i]);
                objectsInScene.AddRange(GetAllObjectsInChild(rootGOs[i]));
            }

            return objectsInScene;
        }
        public static List<GameObject> GetAllObjectsInChild(GameObject go)
        {
            List<GameObject> returnList = new List<GameObject>();
            Transform tr = go.transform;
            int childCount = tr.childCount;
            for (int i = 0; i < childCount; i++)
            {
                if (tr.GetChild(i).GetComponent<DoNotTranslate>() && !forceAllMode)
                    continue;
                returnList.Add(tr.GetChild(i).gameObject);
                returnList.AddRange(GetAllObjectsInChild(tr.GetChild(i).gameObject));
            }
            return returnList;
        }

    }
}
