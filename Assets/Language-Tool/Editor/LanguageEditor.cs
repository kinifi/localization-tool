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

  	private Vector2 scrollPos;

	//temp values used when creating a new key
	private string mNewKey, mNewValue, mOriginText;
    //the value of the text in the current language
    public string newLanguageValue;
    //currently editing key and value
    private int m_editingKey, m_editingValue;
    private SystemLanguage m_currentEditedLanguage;

    private bool m_isEditing = false;

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

		//detect what type of object we have selected
		DetectSelectedFile();
	}

	//draws the UI
	private void OnGUI()
	{

		if(mIsInitalized == false)
		{
			// GUILayout.Label("Language Editor");
			this.ShowNotification(new GUIContent(m_NotificationText));
			return;
		}

		toolbarUI();

		TableDisplay();

		//only display this is we are actually editing
		if(m_isEditing == true)
		{
			EditingUI();
		}

	}

	private void TableDisplay()
	{

		//make sure the level is not null
		if(mLevel == null)
			return;

		//check if we have any keys to display
		if(mLevel.m_Keys.Count == 0)
		{
			GUILayout.Label("No Keys or Values to Display", EditorStyles.miniBoldLabel);
			return;
		}

		//make a container of these blocks of data
		GUILayout.BeginVertical();

		GUILayout.Label("Localization Table View", EditorStyles.miniLabel);

		//start the scroll view
		scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

		//loop through the keys and add rows for them
		for (int i = 1; i <= mLevel.m_Keys.Count; i++)
        {
            GUILayout.BeginHorizontal(EditorStyles.helpBox);

            //get the translation values and add buttons for them
            newLanguageValue = mLevel.m_Translations[i-1];

            //create a label for the key and values
		    GUILayout.Label("Key: " + mLevel.m_Keys[i-1] + " | Value: " + newLanguageValue);

		    //Add a way to edit these
            if(GUILayout.Button("Edit", GUILayout.Width(50)))
            {
            	mNewKey = mLevel.m_Keys[i-1];
            	m_editingKey = i-1;
            	mNewValue = newLanguageValue;
            	m_isEditing = true;
            }

			if(GUILayout.Button("Delete", GUILayout.Width(50)))
            {
            	mLevel.m_Keys.RemoveAt(i-1);
            	mLevel.m_Translations.RemoveAt(i-1);
            }            

            GUILayout.EndHorizontal();
        }

        //end the scrollview 
        EditorGUILayout.EndScrollView();

		GUILayout.EndVertical();
	}

	private void EditingUI()
	{
		GUILayout.Space(50);
		GUILayout.BeginHorizontal();

		//display the text box for someone to translate the text
		GUILayout.BeginVertical();
		GUILayout.Label("Translated Text");
		mNewValue = EditorGUILayout.TextArea(mNewValue, GUILayout.Height(100), GUILayout.Width(position.width-10));
		
		GUILayout.BeginHorizontal();
		//clear the translated text box
		if(GUILayout.Button("Clear Translated Text"))
		{
			//clear the text
			mNewValue = "";
		}

		//submit button for the translated text
		if(GUILayout.Button("Submit Translated Text"))
		{

			//hide the translation editor
			m_isEditing = false;
			
			//set the key to the correct language you are editing
			mLevel.m_Keys[m_editingKey] = mNewKey;

			mLevel.m_Translations[m_editingKey] = mNewValue;

		    //clear the text box
		    mNewValue = "";

			//save the asset
			AssetDatabase.SaveAssets();

		}
		GUILayout.EndHorizontal();

		GUILayout.Space(20);

		GUILayout.EndVertical();

		GUILayout.EndHorizontal();



	}


	private void toolbarUI()
	{
		GUILayout.BeginHorizontal(EditorStyles.toolbar);

		NewKeyCreation();

		GUILayout.FlexibleSpace();

		m_currentEditedLanguage = (SystemLanguage) EditorGUILayout.EnumPopup("Language Type: ", m_currentEditedLanguage, EditorStyles.toolbarPopup, GUILayout.Width(280));
		mLevel.m_DefaultLanguage = m_currentEditedLanguage;

		GUILayout.EndHorizontal();
	}

	private void NewKeyCreation()
	{
		mNewKey = EditorGUILayout.TextField("Create A New Key: ", mNewKey, EditorStyles.toolbarTextField, GUILayout.Width(400));

		if(GUILayout.Button("+", EditorStyles.toolbarButton, GUILayout.Width(30)))
		{
			//hide the translation editor
			m_isEditing = false;

			//save the new key here

			//save the key to mLevel.m_Keys
			mLevel.m_Keys.Add(mNewKey);

			//add a blank key. This will be edited by the user later
			mLevel.m_Translations.Add("");

			//clear the new key
			mNewKey = "";
		}

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
