using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizeText : MonoBehaviour {

	public Language m_Language;

	// Use this for initialization
	void Start ()
	{
		if(m_Language == null)
		{
			Debug.LogError("No Language File Assigned in Inspector");
		}
	}



}
