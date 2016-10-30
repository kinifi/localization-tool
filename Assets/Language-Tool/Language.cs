using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenuAttribute(fileName = "Language", menuName = "Localization/Language")]
public class Language : ScriptableObject
{
	public string m_DefaultLanguage;
	public List<string> m_Keys = new List<string>();
	public List<string> m_Translations = new List<string>();

	public List<string> m_Comments = new List<string>();

	public int GetTranslationCount()
	{
		return m_Translations.Count;
	}

	public int GetCommentCount()
	{
		return m_Comments.Count;
	}

	public int GetKeyCount()
	{
		return m_Keys.Count;
	}

	public void RemoveTranslation(int index)
	{
		//these must all be removed to keep everything in sync
		m_Translations.RemoveAt(index);
		m_Comments.RemoveAt(index);
		m_Keys.RemoveAt(index);
	}

	public string GetTranslation(int value)
	{
		return m_Translations[value];
	}

	public void SetTranslations(int index, string value)
	{
		m_Translations[index] = value;
	}

	public void AddTranslation(string value)
	{
		m_Translations.Add(value);	
	}

	public void AddComment(string comment)
	{
		m_Comments.Add(comment);
	}
	public void SetComment(int index, string comment)
	{
		m_Comments[index] = comment;	
	}

	public string GetComment(int value)
	{
		return m_Comments[value];
	}
	

}
