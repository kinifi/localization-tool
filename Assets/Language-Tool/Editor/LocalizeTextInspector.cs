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
    public Language source;
    public string selectedKey;
    public bool showKeys = false;
    public int index;
    public SystemLanguage languageType;
    private Vector2 scrollView;

    public override void OnInspectorGUI()
    {

      m_Language = (LocalizeText)target;

      GUILayout.Space(2);

      //load values 
      selectedKey = m_Language.m_LanguageKey;
      index = m_Language.m_KeyValue;
      source = m_Language.m_Language;


      //set the text if we are not null
      if(string.IsNullOrEmpty(selectedKey) == true && source != null)
      {
        m_Language.SetUIText(); 
      }

      //give a dialog for our users to select a language file
      GUILayout.BeginHorizontal();
      GUILayout.Label("Select the Language File: ");
      source = (Language)EditorGUILayout.ObjectField(source, typeof(Language), false);
      GUILayout.EndHorizontal(); 

      GUILayout.Space(2);

      m_Language.m_Language = (Language)source;

      //check to make sure we have a language file selected
      if(m_Language.m_Language == null)
      {
        return;
      }

      //tell what type of language this asset is set to
      languageType = m_Language.m_Language.m_DefaultLanguage;
      GUILayout.Label("Language Type: " + languageType.ToString());
      GUILayout.Space(2);

      //convert the list of keys to an array so we can display them in the EditorGUILayout.Popup
      string[] Keys = m_Language.m_Language.m_Keys.ToArray();

      //check to see if we have any keys at all
      if(Keys.Length != 0)
      {
        GUILayout.BeginHorizontal();
        GUILayout.Label("Select a Key: ");
        //displays a popup of the string array of keys
        index = EditorGUILayout.Popup(index, Keys);

        m_Language.SetUIText();
        SetUITextDirty();
        m_Language.m_KeyValue = index;
        
        GUILayout.EndHorizontal();
      }
      else
      {
        GUILayout.Label("No Keys in the Language Asset", EditorStyles.helpBox);
      }

      GUILayout.Space(2);

      DetectUITextObject();
    }

    public void DetectUITextObject()
    {
      //check if we have a UIText object
      if(m_Language.UIText != null)
      {
        GUILayout.Label("UI Text Object Detected", EditorStyles.helpBox);
      }

    }

    public void SetUITextDirty()
    {
      if(m_Language.UIText != null)
      {
        EditorUtility.SetDirty(m_Language.UIText.gameObject);
      }
    }

    ///<summary>
    ///Pulls the Translation from the Language File and the Selected Key
    ///</summary>
    public string GetTranslation()
    {
      return m_Language.m_Language.m_Translations[index];
    }

    public void SetTextToTranslation(Text label)
    {
      label.text = m_Language.m_Language.m_Translations[index];
    }

}
