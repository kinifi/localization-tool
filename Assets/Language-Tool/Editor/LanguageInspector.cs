using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Language))]
[CanEditMultipleObjects]
public class LanguageInspector : Editor
{

    private Language m_Language;
    private bool m_ShowKeys = false;
    private bool m_ShowTranslations = false;

    public override void OnInspectorGUI()
    {

      //grab the file
      m_Language = (Language)target;

      GUILayout.Label("Localization File", EditorStyles.helpBox);

      GUILayout.Space(10);

      //display the keys
      m_ShowKeys = EditorGUILayout.Foldout(m_ShowKeys, "Keys");
      if(m_ShowKeys)
      {
        if(m_Language.m_Keys.Count != 0)
        {
          for (int i = 1; i <= m_Language.m_Keys.Count; i++)
          {
            GUILayout.Label(i + ". " + m_Language.m_Keys[i-1], EditorStyles.helpBox);
          }
        }
        else
        {
          GUILayout.Label("No Keys", EditorStyles.helpBox);
        }

      }

      GUILayout.Space(5);

      //display the keys
      m_ShowTranslations = EditorGUILayout.Foldout(m_ShowTranslations, "Translations");
      if(m_ShowTranslations)
      {

        if(m_Language.m_Translations.Count != 0)
        {

          for (int i = 1; i <= m_Language.m_Translations.Count; i++)
          {
            GUILayout.Label(i + ". " + m_Language.m_Translations[i-1], EditorStyles.helpBox);
          }
        }
        else
        {
          GUILayout.Label("No Translations", EditorStyles.helpBox);
        }

      }

      GUILayout.Space(10);


      //Open the Editor
      if(GUILayout.Button("Edit Language File"))
      {
        LanguageEditor.ShowEditor();
      }
    }
}
