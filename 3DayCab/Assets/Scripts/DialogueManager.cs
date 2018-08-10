using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    public static DialogueManager DialMg;

    public RPGTalk rpgTalk;

    public bool RideCus01 = false;
    public bool RideCus02 = false;
    public bool RideCus03 = false;
    public bool RideCus04 = false;
    public bool RideCus05 = false;

    public bool GoodEnd = false;

    // Use this for initialization
    void Awake () {
        //RESET_AllTalkCount();
        DialMg = this;
    }
	
	// Update is called once per frame
	void Update () {
		//customer selected
        if (LevelManager.LvMg.canTalk)
        {
            if (RideCus01)
            {
                int Cus1TalkCount = PlayerPrefs.GetInt("Cus1TalkCount"); //initial = 1;
                if (Cus1TalkCount == 3)
                {
                    //if after talk 3, trigger talk 4 after ride
                    rpgTalk.NewTalk("Customer1_Talk3_START", "Customer1_Talk3_END");//remember to add function
                    GoodEnd = true; //when arrive destination, if (GoodEnd), set canTalk to true
                    Cus1TalkCount += 1;
                    PlayerPrefs.SetInt("Cus1TalkCount", Cus1TalkCount);
                    LevelManager.LvMg.canTalk = false;
                }
                else if (Cus1TalkCount == 4)
                {
                    rpgTalk.NewTalk("Customer1_Talk4_START", "Customer1_Talk4_END");//remember to add function
                    //Player.money += 2000;
                    Cus1TalkCount += 1;
                    PlayerPrefs.SetInt("Cus1TalkCount", Cus1TalkCount);
                    LevelManager.LvMg.canTalk = false;
                }
                else if (Cus1TalkCount == 5)
                {
                    rpgTalk.NewTalk("Customer1_Talk5_START", "Customer1_Talk5_END");//remember to add function
                    //trigger bad end animations
                    //might need to divide chat into different parts for different timed animations
                    PlayerPrefs.SetInt("Cus1TalkCount", 1);
                    LevelManager.LvMg.canTalk = false;
                }
                else
                {
                    rpgTalk.NewTalk("Customer1_Talk"+Cus1TalkCount.ToString()+"_START", "Customer1_Talk"+Cus1TalkCount.ToString()+"_END");
                    Cus1TalkCount += 1;
                    PlayerPrefs.SetInt("Cus1TalkCount", Cus1TalkCount);
                    LevelManager.LvMg.canTalk = false;
                }

            }
            //Cus1TalkCount += 1;
            //PlayerPrefs.SetInt("Cus1TalkCount", Cus1TalkCount);
        }
        //check that customer's talkcount
        //if after talk 3, trigger talk 4 too
        //if after talk 4, trigger reward
        //if after talk 5,  game over & reset this guy's talkcount - DONE
	}

    

    public void ResetTalkCount(string targetTalkCount)
    {
        //PlayerPrefs.DeleteKey(targetTalkCount);
        PlayerPrefs.SetInt(targetTalkCount, 1);
    }

    public void RESET_AllTalkCount()
    {
        PlayerPrefs.SetInt("Cus1TalkCount", 1);
        PlayerPrefs.SetInt("Cus2TalkCount", 1);
        PlayerPrefs.SetInt("Cus3TalkCount", 1);
        PlayerPrefs.SetInt("Cus4TalkCount", 1);

        PlayerPrefs.SetString("Cus1BadEnd", "no");
        PlayerPrefs.SetString("Cus2BadEnd", "no");
        PlayerPrefs.SetString("Cus3BadEnd", "no");
        PlayerPrefs.SetString("Cus4BadEnd", "no");
    }

    //DEBUG ONLY
    public void DELETEALL_PlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
