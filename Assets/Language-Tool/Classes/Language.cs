﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute(fileName = "Language", menuName = "Localization/Language")]
public class Language : ScriptableObject
{
	public SystemLanguage m_LanguageType;
	public List<LanguageValue> m_Values;
	public List<string> m_Keys;
}
