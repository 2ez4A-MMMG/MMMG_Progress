using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour {

    public static DialogueManager DialMg;
    public RPGTalk rpgTalk;

    public bool RideCus01 = false;
    public bool RideCus02 = false;
    public bool RideCus03 = false;
    public bool RideCus04 = false;
    public bool RideCus05 = false;

    private int Cus1TalkCount, Cus2TalkCount, Cus3TalkCount, Cus4TalkCount;
    [SerializeField]private bool InitiateTalk;
    public bool GoodEnd = false;

    // Use this for initialization
    void Awake () {
        if (!PlayerPrefs.HasKey("Cus1TalkCount") || !PlayerPrefs.HasKey("Cus2TalkCount") || 
            !PlayerPrefs.HasKey("Cus3TalkCount") || !PlayerPrefs.HasKey("Cus4TalkCount") ||
            !PlayerPrefs.HasKey("Cus1BadEnd") || !PlayerPrefs.HasKey("Cus2BadEnd") ||
            !PlayerPrefs.HasKey("Cus3BadEnd") || !PlayerPrefs.HasKey("Cus4BadEnd"))
        {
            RESET_AllTalkCount();
        }
        
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
                if (Cus1TalkCount == 5)
                {
                    rpgTalk.NewTalk("Customer1_Talk5_START", "Cus1Talk5_CutScene1Start", rpgTalk.txtToParse, this, "Cus1Talk1CutScene1");
                }
                else
                {
                    rpgTalk.NewTalk("Customer1_Talk" + Cus1TalkCount.ToString() + "_START", 
                        "Customer1_Talk" + Cus1TalkCount.ToString() + "_END",
                        rpgTalk.txtToParse, this, "AfterCusTalk");
                }
                InitiateTalk = false;
            }
        }
	}

    //declare after talk with customers (for all possible conditions)
    public void AfterTalk(int CurrentCusTalkCount, string CustomerTalkCount, int PayMoney, int GoodEndMoney, string badendData)
    {
        if (CurrentCusTalkCount < 5)
        {
            if (GoodEnd) //first check if need to trigger GoodEnd Event (+ money)
                LevelManager.LvMg.ReceiveReward(PayMoney + GoodEndMoney);
            else
                LevelManager.LvMg.ReceiveReward(PayMoney); //then resume with normal payouts (no pay at BadEnd)
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
            PlayerPrefs.SetString(badendData, "yes");//save BadEnd data
            GoodEnd = false;
            //display Game over too
            //might need to refresh data too^
            
        }
        else
        {
            PlayerPrefs.SetInt(CustomerTalkCount, CurrentCusTalkCount);
            GoodEnd = false;
        }

        LevelManager.LvMg.canTalk = false;
        InitiateTalk = true;
    }

    public void AfterCusTalk()
    {
        if (RideCus01)
            AfterTalk(Cus1TalkCount, "Cus1TalkCount", LevelManager.LvMg.Customer1Pay_Min, LevelManager.LvMg.Customer1GoodEndPay, "Cus1BadEnd");//have to change normal pay
        else if (RideCus02)
            AfterTalk(Cus2TalkCount, "Cus2TalkCount", LevelManager.LvMg.Customer2Pay_Min, LevelManager.LvMg.Customer2GoodEndPay, "Cus2BadEnd");
        else if (RideCus03)
            AfterTalk(Cus3TalkCount, "Cus3TalkCount", LevelManager.LvMg.Customer3Pay_Min, LevelManager.LvMg.Customer3GoodEndPay, "Cus3BadEnd");
        else if (RideCus04)
            AfterTalk(Cus4TalkCount, "Cus4TalkCount", LevelManager.LvMg.Customer4Pay_Min, LevelManager.LvMg.Customer4GoodEndPay, "Cus4BadEnd");
        else
            return;
    }
    //END of declare
    //declare of all extra animations in Talk 5
    public void Cus1Talk1CutScene1()
    {
        LevelManager.LvMg.TaxiZigZagAnim();
        rpgTalk.NewTalk("Cus1Talk5_CutScene1End", "Customer1_Talk5_END", rpgTalk.txtToParse, this, "Cus1Talk1CutScene2");
    }
    public void Cus1Talk1CutScene2()
    {
        StartCoroutine(C1T1Cut2Timings());
    }
    public IEnumerator C1T1Cut2Timings()
    {
        LevelManager.LvMg.TaxiRamToWall();
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(LevelManager.LvMg.Fading());
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(LevelManager.LvMg.PlayOnce_Sfx(LevelManager.LvMg.sfx_source, LevelManager.LvMg.carCrash_sfx));
        yield return new WaitForSeconds(LevelManager.LvMg.carCrash_sfx.length - 1.0f);
        StartCoroutine(LevelManager.LvMg.PlayOnce_Sfx(LevelManager.LvMg.sfx_source, LevelManager.LvMg.explosion_sfx));
        yield return new WaitForSeconds(LevelManager.LvMg.explosion_sfx.length);
        AfterCusTalk();
    }
    //end of Cus1 events

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
