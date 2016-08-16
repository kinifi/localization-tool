using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Assertions;

public class LanguageEditor : EditorWindow
{

	private Language mLevel = null;
	private bool mIsInitalized = false;
	private string m_NotificationText = "Select A Language Asset";

    private SystemLanguage newSystemLanguage = SystemLanguage.English;

    //temp values used when creating a new key
    private string mNewKey, mNewValue;

	[MenuItem("Window/Language Editor %l")]
	public static void ShowEditor() {

		//create the editor window
		LanguageEditor editor = EditorWindow.GetWindow<LanguageEditor>();
		//the editor window must have a min size
		editor.titleContent = new GUIContent("Language Editor");
		editor.minSize = new Vector2 (800, 600);
		//call the init method after we create our window
		editor.Init();

	}


	private void Init()
	{
		//check if we are initialized or not
		if(mIsInitalized == true)
		{
			return;
		}

		//detect what type of object we have selected
		DetectSelectedFile();
	}

	private void OnGUI()
	{

		if(mIsInitalized == false)
		{
			GUILayout.Label("Language Editor");
			this.ShowNotification(new GUIContent(m_NotificationText));
			return;
		}


        //check if there is a language in mLevel
        if (mLevel.m_LanguageType.Count == 0)
        {
            //display the language selection
            AddLanguageUI();
        }
        else
        {
            //display an enum for the languages we have
            AddLanguageUI();

            //display everything we need if they have selected a language
            GUILayout.BeginHorizontal();
            DisplayKeys();
            GUILayout.EndHorizontal();

            //display the new key and value creation UI
            GUILayout.BeginHorizontal("GroupBox");
            NewKeyCreation();
            GUILayout.EndHorizontal();
        }

	}


    private void AddLanguageUI()
    {
        GUILayout.BeginHorizontal("GroupBox");

        newSystemLanguage = (SystemLanguage)EditorGUILayout.EnumPopup(
            "Language: ",
            newSystemLanguage, "DropDownButton");

        GUILayout.EndHorizontal();
    }


	private void DisplayValues()
	{
		GUILayout.BeginVertical("GroupBox");

		GUILayout.Label("Values");

		//display all the keys
		for (int i = 1; i <= mLevel.m_Values.Count; i++)
		{
			GUILayout.BeginHorizontal("HelpBox", GUILayout.Width(position.width / 2));

			GUILayout.Label(mLevel.m_Values[i - 1].m_Values[i -1]);
			GUILayout.EndHorizontal();
		}

		GUILayout.EndVertical();

	}

	//display all the keys in a vertical list
	private void DisplayKeys()
	{


        if (mLevel.m_Keys.Count == 0)
            return;

        GUILayout.BeginVertical("GroupBox");

		GUILayout.Label("Key & Value's");


		//display all the keys
		for (int i = 1; i <= mLevel.m_Keys.Count; i++)
		{
			GUILayout.BeginHorizontal("HelpBox");
			GUILayout.Label("Key: " + mLevel.m_Keys[i - 1]);
			GUILayout.Label("Value: " + mLevel.m_Keys[i - 1]);
			//delete button
			if(GUILayout.Button("Delete", GUILayout.Width(50)))
			{
				mLevel.m_Keys.RemoveAt(i - 1);
				mLevel.m_Values.RemoveAt(i - 1);
			}

			//edit Button
			if(GUILayout.Button("Edit", GUILayout.Width(50)))
			{
				mNewValue = mLevel.m_Values[i - 1].m_Values[i - 1];
				mNewKey = mLevel.m_Keys[i - 1];
			}

			GUILayout.EndHorizontal();
		}

		GUILayout.EndVertical();

	}

	private void NewKeyCreation()
	{

		GUILayout.BeginHorizontal();
		mNewKey = EditorGUILayout.TextField("New Key: ", mNewKey);
		GUILayout.Space(10);
		mNewValue = EditorGUILayout.TextField("Translated Text: ", mNewValue);
		//submit button to add the translated text to a key
		if(GUILayout.Button("Submit", GUILayout.Width(50)))
		{

			//validate the data
			if(mNewKey != "" && mNewValue != "")
			{
				//add the values
				mLevel.m_Keys.Add(mNewKey);
                //TODO: Add values to the correct language
				//mLevel.m_Values.Add(new LanguageValue(newSystemLanguage, mNewValue));
			}
			else
			{
				Debug.LogError("New Value or Key is Empty or Invalid");
			}

			//clear the value now that we have added it to the Asset
			mNewValue = "";
			mNewKey = "";
			// Debug.Log("Created New Value: " + mNewValue);
		}
		GUILayout.EndHorizontal();

	}

	//UNITY EVENTS START
	//All will reset the editor window

	public void PlaymodeChanged()
	{
		DetectSelectedFile();
		Repaint();
	}

	public void OnLostFocus ()
	{
		DetectSelectedFile();
		Repaint();
	}

	public void OnFocus()
	{
		DetectSelectedFile();
		Repaint();
	}

	public void OnProjectChange ()
	{
		DetectSelectedFile();
		Repaint();
	}

	public void OnSelectionChange ()
	{
		DetectSelectedFile();
		Repaint();
	}
	//UNITY EVENTS END


	//checks what object we have selected and gets the data if its the correct type
	private void DetectSelectedFile()
	{
		Language selectedAsset = null;

		//check if we aren't selecting a game object
		if (Selection.activeObject == null)
		{
			//the selected object is null
			mLevel = null;
			mIsInitalized = false;
			selectedAsset = null;
		}

		//check if the object we have selected is a language file
		if (Selection.activeObject is Language && EditorUtility.IsPersistent(Selection.activeObject))
		{
			//set the language file we have selected to be manipulated
			selectedAsset = Selection.activeObject as Language;
			mLevel = selectedAsset;
			mIsInitalized = true;
            this.RemoveNotification();
		}
		else
		{
			//the selected object is null
			mIsInitalized = false;
			selectedAsset = null;
			mLevel = null;
		}

	}


}
