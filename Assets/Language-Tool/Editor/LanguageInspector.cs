using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Language))]
[CanEditMultipleObjects]
public class LanguageInspector : Editor
{

    private Language m_Language;
    private bool m_ShowKeys = true;
    private bool m_ShowTranslations = true;

    //Editor References
    private readonly string languageTitle = "Language: ";
    private readonly string keysTitle = "Keys";
    private readonly string noKeysTitle = "No Keys";
    private readonly string periodSpacing = ". ";
    private readonly string translationsTitle = "Translations";
    private readonly string noTranslationsTitle = "No Translations";
    private readonly string openLocalizationEditor = "Edit Language File";
    private readonly int GUISpacing = 5;


    public override void OnInspectorGUI()
    {

      //grab the file
      m_Language = (Language)target;

      GUILayout.BeginHorizontal(EditorStyles.helpBox);
      GUILayout.Label(languageTitle);
      GUILayout.Label(m_Language.m_DefaultLanguage.ToString());
      GUILayout.EndHorizontal();

      GUILayout.Space(GUISpacing);

      //displays the keys in the selected language file without opening the language Editor
      ShowKeys();

      GUILayout.Space(GUISpacing);

      ShowTranslations ();

      GUILayout.Space(GUISpacing);


      //Open the Editor
      if(GUILayout.Button(openLocalizationEditor))
      {
        LanguageEditor.ShowEditor();
      }
    }

    private void ShowTranslations()
    {
            //display the keys
      m_ShowTranslations = EditorGUILayout.Foldout(m_ShowTranslations, translationsTitle);

      if(m_ShowTranslations)
      {

        GUILayout.BeginVertical(EditorStyles.helpBox);

        if(m_Language.m_Translations.Count != 0)
        {
          for (int i = 1; i <= m_Language.m_Translations.Count; i++)
          {
            GUILayout.Label(i + periodSpacing + m_Language.m_Translations[i-1]);
          }
        }
        else
        {
          GUILayout.Label(noTranslationsTitle);
        }
        GUILayout.EndVertical();
      }
    }

    private void ShowKeys()
    {
            //display the keys
      m_ShowKeys = EditorGUILayout.Foldout(m_ShowKeys, keysTitle);

      if(m_ShowKeys)
      {

        GUILayout.BeginVertical(EditorStyles.helpBox);
        
        if(m_Language.m_Keys.Count != 0)
        {
          for (int i = 1; i <= m_Language.m_Keys.Count; i++)
          {
            GUILayout.Label(i + periodSpacing + m_Language.m_Keys[i-1]);
          }
        }
        else
        {
          GUILayout.Label(noKeysTitle);
        }

        GUILayout.EndVertical();

      }
    }
}
