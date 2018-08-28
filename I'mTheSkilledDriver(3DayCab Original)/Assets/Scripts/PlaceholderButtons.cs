using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlaceholderButtons : MonoBehaviour {

    //move the backgrounds - not needed?
    public void ChangeBGMove()
    {
        if (LevelManager.LvMg.roadMoveSpeed == 0)
        {
            LevelManager.LvMg.roadMoveSpeed = 7;
        }
        else
        {
            LevelManager.LvMg.roadMoveSpeed = 0;
        }
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
    public void SelectC5_HoodieGirl()
    {
        DialogueManager.DialMg.RideCus05 = true;
        LevelManager.LvMg.oneTime = true;
    }

    //skip chats 1-4
    public void skipChat124()
    {
        DialogueManager.DialMg.SkipButtonPressed = true;
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

    //remove all player prefs & reset game
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
