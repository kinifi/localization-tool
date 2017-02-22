using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Localize : MonoBehaviour {

    public string TranslationName;
    public Keys m_Keys;
    public int index = 0;
    public string TranslationFromKey;

	// Use this for initialization
	void Start () {

        GetTranslationLanguageFile();


    }
	
    public string GetTranslation()
    {
        return TranslationFromKey;
    }

    public void GetTranslationLanguageFile()
    {
        TranslationFromKey = LOCManager.Instance.GetTranslation(index);
    }

}
