using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace KreizTranslation
{
    [CustomEditor(typeof(TextLanguage))]
    public class TextLanguageEditor : Editor
    {
        TextLanguage myTarget;
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            myTarget = (TextLanguage)target;
            if (GUILayout.Button("Translate to RU"))
            {
                myTarget.StartTranslationProcessToRU();
            }
            if (GUILayout.Button("Translate to ENG"))
            {
                myTarget.StartTranslationProcessToENG();
            }
        }
    }
}