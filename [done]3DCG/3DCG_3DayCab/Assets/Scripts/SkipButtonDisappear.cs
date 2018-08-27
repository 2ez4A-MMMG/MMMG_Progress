using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipButtonDisappear : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (DialogueManager.DialMg.Cus1TalkCount == 5 && DialogueManager.DialMg.RideCus01 || 
            DialogueManager.DialMg.Cus2TalkCount == 5 && DialogueManager.DialMg.RideCus02 ||
            DialogueManager.DialMg.Cus3TalkCount == 5 && DialogueManager.DialMg.RideCus03 || 
            DialogueManager.DialMg.Cus4TalkCount == 5 && DialogueManager.DialMg.RideCus04)
        {
            this.gameObject.SetActive(false);
        }
	}
}
