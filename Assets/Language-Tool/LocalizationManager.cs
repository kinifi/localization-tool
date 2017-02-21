using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

public class LocalizationManager : MonoBehaviour {

    public Keys m_KeyFile;
    public Language m_DefaultLanguage;
    private Language m_CurrentLanguage;
    public List<Language> m_Languages = new List<Language>();

	// Use this for initialization
	void Start () {

        CheckIfKeysFileIsNull();
        SetDefaultLanguage();
        SetLanguages();
        SetKeys();
        GetLanguage();
	
	}

    private void Update()
    {
        GetLanguage();

    }
    
    private void CheckIfKeysFileIsNull()
    {

        //check to see if the key file is set
        if (m_KeyFile == null)
        {
            Debug.LogError("Not Key File Found. Key File Required.");
        }
    }
    
    private void SetKeys()
    {
        LOCManager.Instance.SetKeys(m_KeyFile);
    }

    private void SetLanguages()
    {
        LOCManager.Instance.ClearOtherLangauges();
        LOCManager.Instance.SetOtherLanguages(m_Languages);
    }

    private void SetDefaultLanguage()
    {
        LOCManager.Instance.SetLanguage(m_DefaultLanguage);
    }


    /// <summary>
    /// Check to see if the language has updated
    /// </summary>
    private void GetLanguage()
    {
        if(m_CurrentLanguage != LOCManager.Instance.currentLanguage)
        {
            m_CurrentLanguage = LOCManager.Instance.currentLanguage;
        }
        
    }




}
