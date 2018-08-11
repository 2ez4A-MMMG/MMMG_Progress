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

    private int Cus1TalkCount;
    [SerializeField]private bool InitiateTalk;
    public bool GoodEnd = false;

    // Use this for initialization
    void Awake () {
        RESET_AllTalkCount();
        DialMg = this;
        InitiateTalk = true;
    }
	
	// Update is called once per frame
	void Update () {
		//customer selected
        if (InitiateTalk && LevelManager.LvMg.canTalk)
        {
            if (RideCus01)
            {
                Cus1TalkCount = PlayerPrefs.GetInt("Cus1TalkCount"); //initial = 1;
                if (Cus1TalkCount == 3)
                {
                    //if after talk 3, trigger talk 4 after ride
                    rpgTalk.NewTalk("Customer1_Talk3_START", "Customer1_Talk3_END", rpgTalk.txtToParse, this, "AfterCus1Talk");
                    //InitiateTalk = false;
                }
                else if (Cus1TalkCount == 4)
                {
                    rpgTalk.NewTalk("Customer1_Talk4_START", "Customer1_Talk4_END", rpgTalk.txtToParse, this, "AfterCus1Talk");
                    //InitiateTalk = false;
                }
                else if (Cus1TalkCount == 5)
                {
                    rpgTalk.NewTalk("Customer1_Talk5_START", "Customer1_Talk5_END", rpgTalk.txtToParse, this, "AfterCus1Talk");
                    //trigger bad end animations
                    //might need to divide chat into different parts for different timed animations
                    //InitiateTalk = false;
                }
                else
                {
                    rpgTalk.NewTalk("Customer1_Talk" + Cus1TalkCount.ToString() + "_START", 
                        "Customer1_Talk" + Cus1TalkCount.ToString() + "_END",
                        rpgTalk.txtToParse, this, "AfterCus1Talk");
                    //InitiateTalk = false;
                }
                InitiateTalk = false;
            }
        }
        //check that customer's talkcount
        //if after talk 3, trigger talk 4 too
        //if after talk 4, trigger reward
        //if after talk 5,  game over & reset this guy's talkcount - DONE
	}

    //declare after talk with customers (for all possible conditions)
    public void AfterTalk(int CurrentCusTalkCount, string CustomerTalkCount, int PayMoney, int GoodEndMoney)
    {
        if (GoodEnd) //first check if need to trigger GoodEnd Event (+ money)
        {
            LevelManager.LvMg.ReceiveReward(GoodEndMoney);
            //add status message
        }

        if (CurrentCusTalkCount < 5)
        {
            LevelManager.LvMg.ReceiveReward(PayMoney); //then resume with normal payouts (no pay at BadEnd)
            //add status message
        }

        CurrentCusTalkCount += 1;//ADD talkcount

        if (CurrentCusTalkCount == 4) //check talkcount after adding
        {
            PlayerPrefs.SetInt(CustomerTalkCount, CurrentCusTalkCount);
            GoodEnd = true;
        }
        else if (CurrentCusTalkCount > 5)
        {
            PlayerPrefs.SetInt(CustomerTalkCount, 1);//reset talkcount
            GoodEnd = false;
        }
        else
        {
            PlayerPrefs.SetInt(CustomerTalkCount, CurrentCusTalkCount);
            GoodEnd = false;
        }

        LevelManager.LvMg.canTalk = false;
        InitiateTalk = true;
    }

    void AfterCus1Talk()
    {
        AfterTalk(Cus1TalkCount, "Cus1TalkCount", LevelManager.LvMg.Customer1Pay_Min, LevelManager.LvMg.Customer1GoodEndPay);
    }
    //end of declare

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
}
