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

  	private Vector2 scrollPos;

	//temp values used when creating a new key
	private string mNewKey, mNewValue;

	[MenuItem("Window/Language Editor %l")]
	public static void ShowEditor()
	{

		//create the editor window
		LanguageEditor editor = EditorWindow.GetWindow<LanguageEditor>();
		//the editor window must have a min size
		editor.titleContent = new GUIContent("Language Editor");
		editor.minSize = new Vector2 (600, 400);
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

		//Set the default language
		mLevel.m_LanguageType = SystemLanguage.English;

		//detect what type of object we have selected
		DetectSelectedFile();
	}

	//draws the UI
	private void OnGUI()
	{

		if(mIsInitalized == false)
		{
			GUILayout.Label("Language Editor");
			this.ShowNotification(new GUIContent(m_NotificationText));
			return;
		}


		CreationPanel();
		DisplayPanel();

	}

	private void DisplayPanel()
	{

		GUILayout.BeginArea(new Rect(position.width/4,0, position.width - position.width/4, position.height), EditorStyles.helpBox);

		DisplayKeys();


		GUILayout.EndArea();

	}

	private void CreationPanel()
	{
		GUILayout.BeginArea(new Rect(0,0, position.width/4, position.height), "GroupBox");

		GUILayout.BeginVertical("GroupBox", GUILayout.Height(position.height));

		//display an enum for the languages we have
		LanguageSelectionDropdown();

		//new key creation
		NewKeyCreation();

		//new value creation
		NewValueCreation();

	  	GUILayout.EndVertical();
		GUILayout.EndArea();
	}

	private void NewValueCreation()
	{

		EditorGUILayout.Space();

		GUILayout.Label("Add the text desired to localize.", "HelpBox");

		GUILayout.Label("Localization Text: ");
		
		//display the text area 
		mNewValue = GUILayout.TextArea(mNewValue, "AS TextArea", GUILayout.Height(100));

		//add the value to the mLevel Object
		if(GUILayout.Button("Submit", "LargeButton"))
		{
			Debug.Log("Entering new Value");
			Debug.Log(mLevel.m_Values.Count);
		}


	}

	//draws the dropdown menu for the language you are writing in
	private void LanguageSelectionDropdown()
	{
	  
		GUILayout.Label("Select A Language To Modify", "HelpBox");
		//display the system language enum selection
		newSystemLanguage = (SystemLanguage)EditorGUILayout.EnumPopup(
		  "Language: ",
		  newSystemLanguage, "DropDownButton");

	  //TODO: check if this value changes so we can update values in the table

	}


	// private void DisplayValues()
	// {
	// 	GUILayout.BeginVertical("GroupBox");

	// 	GUILayout.Label("Values");

	// 	//display all the keys
	// 	for (int i = 1; i <= mLevel.m_Values.Count; i++)
	// 	{
	// 		GUILayout.BeginHorizontal("HelpBox", GUILayout.Width(position.width / 2));

	// 		GUILayout.Label(mLevel.m_Values[i - 1].m_Values[i -1]);
	// 		GUILayout.EndHorizontal();
	// 	}

	// 	GUILayout.EndVertical();

	// }

	//display all the keys in a vertical list
	private void DisplayKeys()
	{

		GUILayout.Label("Keys: ");

		// EditorGUILayout.BeginScrollView(scrollPos);

		//check if we have any keys at all
		if (mLevel.m_Keys.Count != 0)
		{

			//display all the keys
			for (int i = 1; i <= mLevel.m_Keys.Count; i++)
			{
				GUILayout.BeginHorizontal("HelpBox");
				
				//display the keys
				GUILayout.Label("Key: " + mLevel.m_Keys[i - 1] + " | ");

				//delete button
				if(GUILayout.Button("Delete", EditorStyles.miniButton, GUILayout.Width(50)))
				{
					mLevel.m_Keys.RemoveAt(i - 1);
					mLevel.m_Values.RemoveAt(i - 1);
				}

				//edit Button
				if(GUILayout.Button("Edit", EditorStyles.miniButton, GUILayout.Width(50)))
				{
					
					//assign the key to the text field
					mNewKey = mLevel.m_Keys[i - 1];
					mNewValue = mLevel.m_Values[i - 1].m_Text[i - 1];
					
					//remove the key from the list
					// mLevel.m_Keys.RemoveAt(i - 1);

				}

				GUILayout.EndHorizontal();
			}

		}

		// EditorGUILayout.EndScrollView();


	}

	private void NewKeyCreation()
	{

		GUILayout.Space(5);

		GUILayout.Label("Create a new Key for Text to watch", "HelpBox");

		GUILayout.BeginHorizontal();		

		//display the text field for creating a new key
		GUILayout.BeginHorizontal();
		GUILayout.Label("Key: ");
		mNewKey = GUILayout.TextField(mNewKey);
		GUILayout.EndHorizontal();


		//when clicked creates a key value
		if(GUILayout.Button("+", "minibutton", GUILayout.Width(30)))
		{

			//validate the data
			if(mNewKey != " " && mNewKey != "")
			{
				//add the values
				mLevel.m_Keys.Add(mNewKey);

				LanguageValue _newValue = ScriptableObject.CreateInstance<LanguageValue>() as LanguageValue;
				_newValue.m_LanguageType = newSystemLanguage;
				_newValue.m_Text = new List<string>();
				_newValue.m_Text.Add("");

				mLevel.m_Values.Add(_newValue);

				//clear the value now that we have added it to the Asset
				ClearCreationFields();

			}
			else
			{
				Debug.LogError("New Value or Key is Empty or Invalid");
			}

			// Debug.Log("Created New Value: " + mNewValue);
		}

		GUILayout.EndHorizontal();

	}

	//helper methods

	//clears the creation fields to blank strings
	private void ClearCreationFields()
	{
		mNewValue = "";
		mNewKey = "";
		Repaint();
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
