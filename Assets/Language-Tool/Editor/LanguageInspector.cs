using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Language))]
[CanEditMultipleObjects]
public class LanguageInspector : Editor
{

    private Language m_Language;
    private bool m_ShowTranslations = true;

    public override void OnInspectorGUI()
    {

      //grab the file
      m_Language = (Language)target;

      GUILayout.Label("Language: " + m_Language.m_DefaultLanguage);

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
