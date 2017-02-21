/* Title:
 * Summary: 
 * Author: @kinifi
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;

[CreateAssetMenuAttribute(fileName = "Keys", menuName = "Localization/Keys")]
public class Keys : ScriptableObject
{
	public List<string> m_Keys = new List<string>();
}
