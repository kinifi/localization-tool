﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LocalizeText : MonoBehaviour {

	public Language m_Language;

	public string m_LanguageKey = "";
	public int m_KeyValue;

	public Text m_Display;

	// Use this for initialization
	void Start ()
	{
		if(m_Language == null)
		{
			Debug.LogError("No Language File Assigned in Inspector");
		}

		if(m_LanguageKey == null || m_LanguageKey == "" || m_LanguageKey == " " )
		{
			Debug.LogError("No Language Key Assigned in Inspector");
		}

		//set the text with the given values we have
		SetText();

	}

	private void SetText()
	{
		
	}

	public string GetKey()
	{
		return m_LanguageKey;
	}



}
