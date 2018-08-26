using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipButtonDisappear : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (PlayerPrefs.GetInt("C1TC_Temp") == 5 || PlayerPrefs.GetInt("C2TC_Temp") == 5 ||
            PlayerPrefs.GetInt("C3TC_Temp") == 5 || PlayerPrefs.GetInt("C4TC_Temp") == 5)
        {
            this.gameObject.SetActive(false);
        }
	}
}
