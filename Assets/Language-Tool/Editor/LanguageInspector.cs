using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Language))]
[CanEditMultipleObjects]
public class LanguageInspector : Editor
{

    private Language m_Language;
    private bool mIsInitalized = false;

    public override void OnInspectorGUI()
    {

      GUILayout.Label("Edit The Language File", EditorStyles.helpBox);
       //Open the Editor
      if(GUILayout.Button("Edit Language File", EditorStyles.miniButton))
      {
        LanguageEditor.ShowEditor();
      }
    }
}
