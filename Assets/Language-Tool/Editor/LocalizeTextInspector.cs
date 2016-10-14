using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LocalizeText))]
[CanEditMultipleObjects]
public class LocalizeTextInspector : Editor
{

    private LocalizeText m_Language;
    public Object source;
    public string selectedKey;
    public bool showKeys = false;

    private Vector2 scrollView;

    public override void OnInspectorGUI()
    {

      LocalizeText m_Language = (LocalizeText)target;

      //load values 
      selectedKey = m_Language.m_LanguageKey;
      source = m_Language.m_Language;


      GUILayout.Label("Select the Language File", EditorStyles.helpBox);
      source = EditorGUILayout.ObjectField(source, typeof(Object), false);
      m_Language.m_Language = (Language)source;

      GUILayout.Space(5);

      if(source == null)
      {
        return;
      }

      GUILayout.Space(5);
      if(selectedKey != null || selectedKey != " " || selectedKey != "")
      {
        GUILayout.BeginHorizontal(); 
        GUILayout.Label("Key: " + selectedKey, EditorStyles.helpBox);
        if(GUILayout.Button("Clear Key", EditorStyles.miniButtonRight))
        {
          selectedKey = "";
          m_Language.m_LanguageKey = "";
        }
        GUILayout.EndHorizontal();
      }
      else
      {
        GUILayout.Label("Select A Key Below", EditorStyles.boldLabel);
      }

      GUILayout.Space(5);
      if(GUILayout.Button("Show/Hide Keys", EditorStyles.miniButton))
      {
        showKeys = !showKeys;
      }
      GUILayout.Space(5);

      if(showKeys == false)
      {
        return;
      }


      GUILayout.Label("Select the Key for this Text to Follow", EditorStyles.helpBox);
      GUILayout.BeginVertical(EditorStyles.helpBox);

      //display all the keys in the language object
      for (int i = 1; i <= m_Language.m_Language.m_Keys.Count; i++)
      {
        GUILayout.BeginHorizontal();  
        GUILayout.Label(m_Language.m_Language.m_Keys[i-1]);
        
        if(GUILayout.Button("Select"))
        {
          selectedKey = m_Language.m_Language.m_Keys[i-1];
          m_Language.m_LanguageKey = m_Language.m_Language.m_Keys[i-1];
          m_Language.m_KeyValue = i-1;
          showKeys = false;
        }

        GUILayout.EndHorizontal();
      }

      GUILayout.EndVertical();

    }

}
