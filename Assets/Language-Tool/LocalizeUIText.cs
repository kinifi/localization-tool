using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UnityEditor
using UnityEditor;
#endif


public class LocalizeUIText : MonoBehaviour {

	public Language m_Language;
	public SystemLanguage m_LanguageType;
	public string m_LanguageKey = "";
	public int m_KeyValue;
	public Text UIText;
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
		SetUIText();

	}

	public void SetUIText()
	{
		UIText = GetComponent<Text>();
		if(UIText != null)
		{
			UIText.text = GetTranslation();
		}

	}

	public string GetTranslation()
	{
		return m_Language.m_Translations[m_KeyValue];
	}

	public string GetKey()
	{
		return m_LanguageKey;
	}



}
