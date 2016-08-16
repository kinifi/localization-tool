using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute(fileName = "Language", menuName = "Localization/Language")]
public class Language : ScriptableObject
{
	public List<SystemLanguage> m_LanguageType;
	public List<string> m_Values;
	public List<string> m_Keys;
}
