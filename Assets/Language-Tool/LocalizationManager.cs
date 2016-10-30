using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

public class LocalizationManager : MonoBehaviour {

	private List<Language> m_Languages = new List<Language>();

	// Use this for initialization
	void Start () {
	
	}

	public void AddLanguage(Language NewLanguage)
	{
		m_Languages.Add(NewLanguage);
	}

	public void RemoveLanguage(string LanguageToRemove)
	{
		for (int i = 0; i < m_Languages.Count; i++)
		{
			if(LanguageToRemove.ToLower() == m_Languages[i].m_DefaultLanguage.ToLower())
			{
				m_Languages.Remove(m_Languages[i]);
			}
		}
	}



}
