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
	public SystemLanguage currentLanguage;

	//Class Level Members End


	//Method Start

	/// <summary>
	/// load all of the data on setup
	/// This should be called on the title screen
	/// </summary>
	public void Setup()
	{	
		//get the current language
		currentLanguage = Application.systemLanguage;
	}

	/// <summary>
	/// Sets the current language. Default is system language
	/// </summary>
	/// <param name="language">Language.</param>
	public void SetLanguage(SystemLanguage language)
	{
		//set the current language
		currentLanguage = language;
	}

	/// <summary>
	/// Gets the current set language
	/// </summary>
	/// <returns>The language.</returns>
	public SystemLanguage GetLanguage()
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