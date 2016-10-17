using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

[CustomEditor(typeof(LocalizeText))]
[CanEditMultipleObjects]
public class LocalizeTextInspector : Editor
{

    private LocalizeText m_Language;
    public Object source;
    public string selectedKey;
    public bool showKeys = false;
    private int index;
    private Vector2 scrollView;

    public override void OnInspectorGUI()
    {

      LocalizeText m_Language = (LocalizeText)target;

      GUILayout.Space(10);

      //load values 
      selectedKey = m_Language.m_LanguageKey;
      source = m_Language.m_Language;

      //give a dialog for our users to select a language file
      GUILayout.Label("Select the Language File", EditorStyles.helpBox);
      source = EditorGUILayout.ObjectField(source, typeof(Object), false);
      m_Language.m_Language = (Language)source;

      //check to make sure we have a language file selected
      if(m_Language.m_Language == null)
      {
        return;
      }

      string[] Keys = m_Language.m_Language.m_Keys.ToArray();

      GUILayout.BeginHorizontal();
      GUILayout.Label("Select a Key: ");
      //displays a popup of the string array of keys
      index = EditorGUILayout.Popup(index, Keys);
      GUILayout.EndHorizontal();
      GUILayout.Space(10);
    }

    ///<summary>
    ///Pulls the Translation from the Language File and the Selected Key
    ///</summary>
    public string GetTranslation()
    {
      return m_Language.m_Language.m_Translations[index-1];
    }

    public void SetTextToTranslation(Text label)
    {
      label.text = m_Language.m_Language.m_Translations[index-1];
    }

}
