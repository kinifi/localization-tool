using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LocalizationManager))]
public class LanguageManagerInspector : Editor
{

    private LocalizationManager m_Language;

    public override void OnInspectorGUI()
    {

        //grab the file
        m_Language = (LocalizationManager)target;

        GUILayout.Label("This Component Must Load First Before Any Translations Can Be Used.", EditorStyles.helpBox);
        GUILayout.Space(5);

        DrawDefaultInspector();
    }
}
