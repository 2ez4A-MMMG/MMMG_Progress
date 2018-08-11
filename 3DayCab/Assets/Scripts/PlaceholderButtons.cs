using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    //player/customer walking towards car
    //player/customer dropping off from car

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

    //show overlay cutscene animation
    //in-game animations
    //fadeout to gameover Credits (Laer Swen car radio annoucement)

    //DEBUG: remove all player prefs
    public void DeleteAllPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
    //DEBUG: reset only the chatCounts
    public void ResetAllChatCounts()
    {
        DialogueManager.DialMg.RESET_AllTalkCount();
    }
}
