using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace KreizTranslation
{
    [CustomEditor(typeof(TranslationsCollector))]
    public class TranslationsCollectorEditor : Editor
    {
        TranslationsCollector myTarget;
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            myTarget = (TranslationsCollector)target;
            if (GUILayout.Button("Get all Texts"))
            {
                if(myTarget.textLanguages.Count>0)
                {
                    Debug.Log("myTarget.textLanguages.Count>0");
                    return;
                }
                List<GameObject> allObjects = GameTranslatorEditor.GetAllObjectsInScene();
                for (int i = 0; i < allObjects.Count; i++)
                {
                    if (myTarget.SearchOnlyActiveObjects && !allObjects[i].activeInHierarchy)
                        continue;
                    if (allObjects[i].TryGetComponent<TextLanguage>(out TextLanguage TL))
                    {
                        myTarget.textLanguages.Add(TL);
                    }
                }
                myTarget.GetTextFromComponents();
                EditorUtility.SetDirty(myTarget);
            }
        }
    }
}