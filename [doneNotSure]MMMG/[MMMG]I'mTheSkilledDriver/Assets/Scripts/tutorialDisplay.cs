using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tutorialDisplay : MonoBehaviour {

    private GameObject tutorialScreen;

    // Use this for initialization
    void Start () {
        tutorialScreen = this.gameObject;
    }
	
	// Update is called once per frame
	void Update () {
        if (PlayerPrefs.GetString("TutorialDone") == "true")
        {
            Destroy(tutorialScreen);
        }
        else
        {
            if (LevelManager.LvMg.gamePaused)
                tutorialScreen.SetActive(true);
            else if (!LevelManager.LvMg.gamePaused)
                Destroy(tutorialScreen);
        }
	}
}
