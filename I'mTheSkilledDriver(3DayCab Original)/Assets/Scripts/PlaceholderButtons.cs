using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlaceholderButtons : MonoBehaviour {	

	//all these are buttons for showing animations. Nothing more, nothing less

	//move the backgrounds
	public void ChangeBGMove()
    {
        if (!LevelManager.LvMg.canMove)
        {
            LevelManager.LvMg.canMove = true;
        }
        else
        {
            LevelManager.LvMg.canMove = false;
        }
    }

    //display chat
    public void StartTalking()
    {
        LevelManager.LvMg.canTalk = true;
		
    }
    //not sure if needed tho
    public void StopTalking()
    {
        LevelManager.LvMg.canTalk = false;
    }

    //select customers
    public void SelectC1_BusinessMan()
    {
        DialogueManager.DialMg.RideCus01 = true;
        LevelManager.LvMg.oneTime = true;
		GameObject.Find("SelectiveDisplay").SetActive(false);
	}
    public void SelectC2_FemaleStudent()
    {
        DialogueManager.DialMg.RideCus02 = true;
        LevelManager.LvMg.oneTime = true;
		GameObject.Find("SelectiveDisplay").SetActive(false);
	}
    public void SelectC3_OldMan()
    {
        DialogueManager.DialMg.RideCus03 = true;
        LevelManager.LvMg.oneTime = true;
		GameObject.Find("SelectiveDisplay").SetActive(false);
	}
    public void SelectC4_PartTimer()
    {
        DialogueManager.DialMg.RideCus04 = true;
        LevelManager.LvMg.oneTime = true;
		GameObject.Find("SelectiveDisplay").SetActive(false);
	}

    public void ExitLevel()
    {
        StartCoroutine(FadeLoader.FadeSLoad.Fading(FadeLoader.FadeSLoad.TitleScreen));
    }
    

    //Title Screen
    public void StartGame()
    {
        StartCoroutine(FadeLoader.FadeSLoad.Fading(FadeLoader.FadeSLoad.GameScreen));
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void Open_Close_Panel(GameObject panel)
    {
        if (!panel.activeSelf)
            panel.SetActive(true);
        else if (panel.activeSelf)
            panel.SetActive(false);
    }

    //not debug anymore: remove all player prefs & reset game
    public void DeleteAllPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    //DEBUG: reset only the chatCounts
    public void ResetAllChatCounts()
    {
        DialogueManager.DialMg.RESET_AllTalkCount();
    }
    //DEBUG- test game over screen
    public void loadGameOverScreen()
    {
        StartCoroutine(FadeLoader.FadeSLoad.Fading(FadeLoader.FadeSLoad.CarRadioScreen));
    }
}
