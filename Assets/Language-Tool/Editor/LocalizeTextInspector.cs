using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Globalization;

[CustomEditor(typeof(Localize))]
[CanEditMultipleObjects]
public class LocalizeTextInspector : Editor
{

    private Localize m_Language;
    public Keys m_Keys;
    public string selectedKey;
    public int index;
    public string UniqueName;

    public override void OnInspectorGUI()
    {

        m_Language = (Localize)target;

        GUILayout.Space(2);

        //load values 
        UniqueName = m_Language.TranslationName;
        index = m_Language.index;
        m_Keys = m_Language.m_Keys;


        //give a dialog for our users to select a language file
        GUILayout.BeginHorizontal();
        GUILayout.Label("Set the Key File: ");
        m_Keys = (Keys)EditorGUILayout.ObjectField(m_Keys, typeof(Keys), false);
        GUILayout.EndHorizontal(); 

        GUILayout.Space(2);

        m_Language.m_Keys = (Keys)m_Keys;

        //check to make sure we have a language file selected
        if(m_Language.m_Keys == null)
        {
            return;
        }

        //display the unique translation text field for users
        UniqueName = EditorGUILayout.TextField("Unique Translation Name: ", UniqueName, GUILayout.ExpandWidth(true));
        if(UniqueName != m_Language.TranslationName)
        {
            m_Language.TranslationName = UniqueName;
            EditorUtility.SetDirty(m_Language);
        }

        GUILayout.Space(2);

        //convert the list of keys to an array so we can display them in the EditorGUILayout.Popup
        string[] Keys = m_Language.m_Keys.m_Keys.ToArray();

        //check to see if we have any keys at all
        if(Keys.Length != 0)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Select a Key: ");
            //displays a popup of the string array of keys
            index = EditorGUILayout.Popup(index, Keys, GUILayout.ExpandWidth(true));

            if (index != m_Language.index)
            {
                //save the index
                m_Language.index = index;
                EditorUtility.SetDirty(m_Language);

            }
            GUILayout.EndHorizontal();

        }
        else
        {
            GUILayout.Label("No Keys in the Language Asset", EditorStyles.helpBox);
        }

    }


}
