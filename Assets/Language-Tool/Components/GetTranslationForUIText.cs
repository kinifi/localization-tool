using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetTranslationForUIText : MonoBehaviour {

    public Text m_UIText;

	// Use this for initialization
	void Start () {

        m_UIText.text = m_UIText.GetComponent<Localize>().GetTranslation();

	}

}
