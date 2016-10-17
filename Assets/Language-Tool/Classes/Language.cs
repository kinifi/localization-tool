/* Title:
 * Summary: 
 * Author: @kinifi
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute(fileName = "Language", menuName = "Localization/Language")]
public class Language : ScriptableObject
{
	public List<string> m_Keys = new List<string>();
	public List<string> m_Translations;

}
