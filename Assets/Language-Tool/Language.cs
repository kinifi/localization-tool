/* Title:
 * Summary: 
 * Author: @kinifi
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;

[CreateAssetMenuAttribute(fileName = "Language", menuName = "Localization/Language")]
public class Language : ScriptableObject
{
	public string m_DefaultLanguage;
	public List<string> m_Keys = new List<string>();
	public List<string> m_Translations;

}
