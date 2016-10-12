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
	private string mNewKey, mNewValue, mOriginText;
    //the value of the text in the current language
    public string newLanguageValue;
    //currently editing key and value
    private int m_editingKey, m_editingValue;
    private SystemLanguage m_currentEditedLanguage;

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

		EditingUI();

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



            //check what language is selected
            switch (newSystemLanguage)
			{
			    case SystemLanguage.Afrikaans:
			        newLanguageValue = mLevel.m_Afrikaans[i-1];
			        m_currentEditedLanguage = SystemLanguage.Afrikaans;
			        break;
			    case SystemLanguage.Arabic:
			        newLanguageValue = mLevel.m_Arabic[i-1];
			        break;
		        case SystemLanguage.Basque:
			        newLanguageValue = mLevel.m_Basque[i-1];
			        break;
		        case SystemLanguage.Belarusian:
			        newLanguageValue = mLevel.m_Belarusian[i-1];
			        break;
		        case SystemLanguage.Catalan:
			        newLanguageValue = mLevel.m_Catalan[i-1];
			        break;
			    case SystemLanguage.Chinese:
			        newLanguageValue = mLevel.m_Chinese[i-1];
			        break;
			    case SystemLanguage.Czech:
			        newLanguageValue = mLevel.m_Czech[i-1];
			        break;
			    case SystemLanguage.Danish:
			        newLanguageValue = mLevel.m_Danish[i-1];
			        break;
			    case SystemLanguage.Dutch:
			        newLanguageValue = mLevel.m_Dutch[i-1];
			        break;
		        case SystemLanguage.English:
			        newLanguageValue = mLevel.m_English[i-1];
			        break;
		        case SystemLanguage.Estonian:
			        newLanguageValue = mLevel.m_Estonian[i-1];
			        break;
		        case SystemLanguage.Faroese:
			        newLanguageValue = mLevel.m_Faroese[i-1];
			        break;
			    case SystemLanguage.Finnish:
			        newLanguageValue = mLevel.m_Finnish[i-1];
			        break;
		        case SystemLanguage.French:
			        newLanguageValue = mLevel.m_French[i-1];
			        break;
			    case SystemLanguage.German:
			        newLanguageValue = mLevel.m_German[i-1];
			        break;
		        case SystemLanguage.Greek:
			        newLanguageValue = mLevel.m_Greek[i-1];
			        break;
		        case SystemLanguage.Hebrew:
			        newLanguageValue = mLevel.m_Hebrew[i-1];
			        break;
		        case SystemLanguage.Icelandic:
			        newLanguageValue = mLevel.m_Icelandic[i-1];
			        break;
		        case SystemLanguage.Indonesian:
			        newLanguageValue = mLevel.m_Indonesian[i-1];
			        break;
		        case SystemLanguage.Italian:
			        newLanguageValue = mLevel.m_Italian[i-1];
			        break;
		        case SystemLanguage.Japanese:
			        newLanguageValue = mLevel.m_Japanese[i-1];
			        break;
		        case SystemLanguage.Korean:
			        newLanguageValue = mLevel.m_Korean[i-1];
			        break;
		        case SystemLanguage.Latvian:
			        newLanguageValue = mLevel.m_Latvian[i-1];
			        break;
		        case SystemLanguage.Lithuanian:
			        newLanguageValue = mLevel.m_Lithuanian[i-1];
			        break;
		        case SystemLanguage.Norwegian:
			        newLanguageValue = mLevel.m_Norwegian[i-1];
			        break;
		        case SystemLanguage.Polish:
			        newLanguageValue = mLevel.m_Polish[i-1];
			        break;
		        case SystemLanguage.Portuguese:
			        newLanguageValue = mLevel.m_Portuguese[i-1];
			        break;
		        case SystemLanguage.Romanian:
			        newLanguageValue = mLevel.m_Romanian[i-1];
			        break;
		        case SystemLanguage.Russian:
			        newLanguageValue = mLevel.m_Russian[i-1];
			        break;
		        case SystemLanguage.SerboCroatian:
			        newLanguageValue = mLevel.m_SerboCroatian[i-1];
			        break;
		        case SystemLanguage.Slovak:
			        newLanguageValue = mLevel.m_Slovak[i-1];
			        break;
		        case SystemLanguage.Slovenian:
			        newLanguageValue = mLevel.m_Slovenian[i-1];
			        break;
		        case SystemLanguage.Spanish:
			        newLanguageValue = mLevel.m_Spanish[i-1];
			        break;
		        case SystemLanguage.Swedish:
			        newLanguageValue = mLevel.m_Swedish[i-1];
			        break;
		        case SystemLanguage.Thai:
			        newLanguageValue = mLevel.m_Thai[i-1];
			        break;
		        case SystemLanguage.Turkish:
			        newLanguageValue = mLevel.m_Turkish[i-1];
			        break;
		        case SystemLanguage.Ukrainian:
			        newLanguageValue = mLevel.m_Ukrainian[i-1];
			        break;
		        case SystemLanguage.Vietnamese:
			        newLanguageValue = mLevel.m_Vietnamese[i-1];
			        break;
		        case SystemLanguage.ChineseSimplified:
			        newLanguageValue = mLevel.m_ChineseSimplified[i-1];
			        break;
		        case SystemLanguage.ChineseTraditional:
			        newLanguageValue = mLevel.m_ChineseTraditional[i-1];
			        break;
			    case SystemLanguage.Hungarian:
			        newLanguageValue = mLevel.m_Hungarian[i-1];
			        break;    
		        case SystemLanguage.Unknown:
			        newLanguageValue = mLevel.m_Unknown[i-1];
			        break;
		    }

		    GUILayout.Label("Key: " + mLevel.m_Keys[i-1] + " | Value: " + newLanguageValue);

            if(GUILayout.Button("Edit", GUILayout.Width(50)))
            {
            	mNewKey = mLevel.m_Keys[i-1];
            	m_editingKey = i-1;
            	mNewValue = newLanguageValue;

            }

			if(GUILayout.Button("Delete", GUILayout.Width(50)))
            {
            	mLevel.m_Keys.RemoveAt(i-1);
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

		// //display the text box that shows the games native language and its original value
		// GUILayout.BeginVertical();
		// GUILayout.Label("Origin Text");
		// mOriginText = EditorGUILayout.TextArea(mOriginText, GUILayout.Height(100), GUILayout.Width(position.width/2));
		// GUILayout.EndVertical();

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

			//set the key to the correct language you are editing
			mLevel.m_Keys[m_editingKey] = mNewKey;


            //set the value to the correct language you are editing
            switch (newSystemLanguage)
			{
			    case SystemLanguage.Afrikaans:
			        mLevel.m_Afrikaans[m_editingKey] = mNewValue;
			        break;
			    case SystemLanguage.Arabic:
			        mLevel.m_Arabic[m_editingKey] = mNewValue;
			        break;
		        case SystemLanguage.Basque:
			        mLevel.m_Basque[m_editingKey] = mNewValue;
			        break;
		        case SystemLanguage.Belarusian:
			        mLevel.m_Belarusian[m_editingKey] = mNewValue;
			        break;
		        case SystemLanguage.Catalan:
			        mLevel.m_Catalan[m_editingKey] = mNewValue;
			        break;
			    case SystemLanguage.Chinese:
			        mLevel.m_Chinese[m_editingKey] = mNewValue;
			        break;
			    case SystemLanguage.Czech:
			        mLevel.m_Czech[m_editingKey] = mNewValue;
			        break;
			    case SystemLanguage.Danish:
			        mLevel.m_Danish[m_editingKey] = mNewValue;
			        break;
			    case SystemLanguage.Dutch:
			        mLevel.m_Dutch[m_editingKey] = mNewValue;
			        break;
		        case SystemLanguage.English:
			        mLevel.m_English[m_editingKey] = mNewValue;
			        break;
		        case SystemLanguage.Estonian:
			        mLevel.m_Estonian[m_editingKey] = mNewValue;
			        break;
		        case SystemLanguage.Faroese:
			        mLevel.m_Faroese[m_editingKey] = mNewValue;
			        break;
			    case SystemLanguage.Finnish:
			        mLevel.m_Finnish[m_editingKey] = mNewValue;
			        break;
		        case SystemLanguage.French:
			        mLevel.m_French[m_editingKey] = mNewValue;
			        break;
			    case SystemLanguage.German:
			        mLevel.m_German[m_editingKey] = mNewValue;
			        break;
		        case SystemLanguage.Greek:
			        mLevel.m_Greek[m_editingKey] = mNewValue;
			        break;
		        case SystemLanguage.Hebrew:
			        mLevel.m_Hebrew[m_editingKey] = mNewValue;
			        break;
		        case SystemLanguage.Icelandic:
			        mLevel.m_Icelandic[m_editingKey] = mNewValue;
			        break;
		        case SystemLanguage.Indonesian:
			        mLevel.m_Indonesian[m_editingKey] = mNewValue;
			        break;
		        case SystemLanguage.Italian:
			        mLevel.m_Italian[m_editingKey] = mNewValue;
			        break;
		        case SystemLanguage.Japanese:
			        mLevel.m_Japanese[m_editingKey] = mNewValue;
			        break;
		        case SystemLanguage.Korean:
			        mLevel.m_Korean[m_editingKey] = mNewValue;
			        break;
		        case SystemLanguage.Latvian:
			        mLevel.m_Latvian[m_editingKey] = mNewValue;
			        break;
		        case SystemLanguage.Lithuanian:
			        mLevel.m_Lithuanian[m_editingKey] = mNewValue;
			        break;
		        case SystemLanguage.Norwegian:
			        mLevel.m_Norwegian[m_editingKey] = mNewValue;
			        break;
		        case SystemLanguage.Polish:
			        mLevel.m_Polish[m_editingKey] = mNewValue;
			        break;
		        case SystemLanguage.Portuguese:
			        mLevel.m_Portuguese[m_editingKey] = mNewValue;
			        break;
		        case SystemLanguage.Romanian:
			        mLevel.m_Romanian[m_editingKey] = mNewValue;
			        break;
		        case SystemLanguage.Russian:
			        mLevel.m_Russian[m_editingKey] = mNewValue;
			        break;
		        case SystemLanguage.SerboCroatian:
			        mLevel.m_SerboCroatian[m_editingKey] = mNewValue;
			        break;
		        case SystemLanguage.Slovak:
			        mLevel.m_Slovak[m_editingKey] = mNewValue;
			        break;
		        case SystemLanguage.Slovenian:
			        mLevel.m_Slovenian[m_editingKey] = mNewValue;
			        break;
		        case SystemLanguage.Spanish:
			        mLevel.m_Spanish[m_editingKey] = mNewValue;
			        break;
		        case SystemLanguage.Swedish:
			        mLevel.m_Swedish[m_editingKey] = mNewValue;
			        break;
		        case SystemLanguage.Thai:
			        mLevel.m_Thai[m_editingKey] = mNewValue;
			        break;
		        case SystemLanguage.Turkish:
			        mLevel.m_Turkish[m_editingKey] = mNewValue;
			        break;
		        case SystemLanguage.Ukrainian:
			        mLevel.m_Ukrainian[m_editingKey] = mNewValue;
			        break;
		        case SystemLanguage.Vietnamese:
			        mLevel.m_Vietnamese[m_editingKey] = mNewValue;
			        break;
		        case SystemLanguage.ChineseSimplified:
			        mLevel.m_ChineseSimplified[m_editingKey] = mNewValue;
			        break;
		        case SystemLanguage.ChineseTraditional:
			        mLevel.m_ChineseTraditional[m_editingKey] = mNewValue;
			        break;
			    case SystemLanguage.Hungarian:
			        mLevel.m_Hungarian[m_editingKey] = mNewValue;
			        break;    
		        case SystemLanguage.Unknown:
			        mLevel.m_Unknown[m_editingKey] = mNewValue;
			        break;
		    }

		    //clear the text box
		    mNewValue = "";

			//save the asset
			AssetDatabase.SaveAssets();

			// Debug.Log("Translated Text: " + mNewValue);
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

		LanguageSelectionDropdown();

		GUILayout.EndHorizontal();
	}

	private void NewKeyCreation()
	{
		mNewKey = EditorGUILayout.TextField("Create A New Key: ", mNewKey, EditorStyles.toolbarTextField, GUILayout.Width(400));

		if(GUILayout.Button("+", EditorStyles.toolbarButton, GUILayout.Width(30)))
		{
			//save the new key here

			//save the key to mLevel.m_Keys
			mLevel.m_Keys.Add(mNewKey);
			//add a new string value to every language
			mLevel.m_Afrikaans.Add("");
			mLevel.m_Arabic.Add("");
			mLevel.m_Basque.Add("");
			mLevel.m_Belarusian.Add("");
			mLevel.m_Belgarian.Add("");
			mLevel.m_Catalan.Add("");
			mLevel.m_Chinese.Add("");
			mLevel.m_Czech.Add("");
			mLevel.m_Danish.Add("");
			mLevel.m_Dutch.Add("");
			mLevel.m_English.Add("");
			mLevel.m_Estonian.Add("");
			mLevel.m_Faroese.Add("");
			mLevel.m_Finnish.Add("");
			mLevel.m_French.Add("");
			mLevel.m_German.Add("");
			mLevel.m_Greek.Add("");
			mLevel.m_Hebrew.Add("");
			mLevel.m_Icelandic.Add("");
			mLevel.m_Indonesian.Add("");
			mLevel.m_Italian.Add("");
			mLevel.m_Japanese.Add("");
			mLevel.m_Korean.Add("");
			mLevel.m_Latvian.Add("");
			mLevel.m_Lithuanian.Add("");
			mLevel.m_Norwegian.Add("");
			mLevel.m_Polish.Add("");
			mLevel.m_Portuguese.Add("");
			mLevel.m_Romanian.Add("");
			mLevel.m_Russian.Add("");
			mLevel.m_SerboCroatian.Add("");
			mLevel.m_Slovak.Add("");
			mLevel.m_Slovenian.Add("");
			mLevel.m_Spanish.Add("");
			mLevel.m_Swedish.Add("");
			mLevel.m_Thai.Add("");
			mLevel.m_Turkish.Add("");
			mLevel.m_Ukrainian.Add("");
			mLevel.m_Vietnamese.Add("");
			mLevel.m_ChineseSimplified.Add("");
			mLevel.m_ChineseTraditional.Add("");
			mLevel.m_Unknown.Add("");


			// Debug.Log("Add Key:" + mNewKey);

			//clear the new key
			mNewKey = "";
		}

	}

	//draws the dropdown menu for the language you are writing in
	private void LanguageSelectionDropdown()
	{

		EditorGUILayout.Space();

		// GUILayout.Label("Select A Language To Modify", EditorStyles.toolbar);

		//save the language settings
		SystemLanguage storedLanguage = newSystemLanguage;

		//display the system language enum selection
		newSystemLanguage = (SystemLanguage)EditorGUILayout.EnumPopup(
		  "Language: ",
		  newSystemLanguage, EditorStyles.toolbarPopup, GUILayout.Width(300));

		//TODO: check if this value changes so we can update values in the table
		if(storedLanguage != newSystemLanguage)
		{
			Debug.Log("Language Enum Changed");
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
