/*
 *  Game Manager
 *  This is how we will save our persistent data such as: 
 *  Logged in save data from a json file and assigning variables which we access in game
 *  With this Singleton we can store data we need for later use
 *  Example on how to use: LOCManager.Instance.[Variable Name / Method Name]
 *  Methods and Variables must be public
 *  Note: A new singleton should be created per platform for achievements string literals and specific achievement methods 
 *
 */

/// Note: that the more namespaces we use the more loading this screen has to do
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class LOCManager
{

    //Class Level Members Start
    public Language defaultLanguage;
	public Language currentLanguage;
    public Keys currentKeys;
    private List<Language> m_Languages = new List<Language>();


    //Class Level Members End


    //Method Start

    #region Languages

    public void ClearOtherLangauges()
    {
        m_Languages.Clear();
    }

    public void AddOtherLanguage(Language NewLanguage)
    {
        m_Languages.Add(NewLanguage);
    }

    /// <summary>
    /// Saves the other languages to be used. Clears Existing Languages that are stored
    /// </summary>
    /// <param name="Languages"></param>
    public void SetOtherLanguages(List<Language> Languages)
    {
        ClearOtherLangauges();
        m_Languages = Languages;
    }

    #endregion

    public string GetTranslation(int index)
    {
        return currentLanguage.m_Translations[index];
    }

    public void ChangeSceneObjects()
    {
        Localize[] LocalizeTextFiles = GameObject.FindObjectsOfType<Localize>();

    }

	/// <summary>
	/// load all of the data on setup
	/// This should be called on the title screen
	/// </summary>
	public void Setup()
	{	

	}

    #region Keys

    public void ClearKeys()
    {
        currentKeys = null;
    }

    public void SetKeys(Keys keyfile)
    {
        currentKeys = keyfile;
    }

    public Keys GetKeys()
    {
        return currentKeys;
    }

    #endregion

    /// <summary>
    /// Sets the current language. Default is system language
    /// </summary>
    /// <param name="language">Language.</param>
    public void SetLanguage(Language language)
	{
		//set the current language
		currentLanguage = language;
	}

    public void SetDefaultLanguage(Language language)
    {
        defaultLanguage = language;
        currentLanguage = language;
    }

	/// <summary>
	/// Gets the current set language
	/// </summary>
	/// <returns>The language.</returns>
	public Language GetLanguage()
	{
		return currentLanguage;
	}

	//Method End

	//create a local instance of GameManager
	private static LOCManager instance;

	//If there isn't a GameManager instance, create one.
	public static LOCManager Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new LOCManager();
			}
			return instance;
		}
	}

	private void Shutdown()
	{
		if (instance != null) {
			instance = null;
		}	

	}

}