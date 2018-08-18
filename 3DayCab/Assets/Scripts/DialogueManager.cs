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

	private GameObject selectiveDisplay;

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

	private void Start()
	{
		selectiveDisplay= GameObject.Find("SelectiveDisplay");
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
                    LevelManager.LvMg.bgm_source.Stop();
                    rpgTalk.NewTalk("Customer1_Talk5_START", "Cus1Talk5_CutScene1Start", rpgTalk.txtToParse, this, "Cus1Talk5CutScene1");
                }
                else
                {
                    rpgTalk.NewTalk("Customer1_Talk" + Cus1TalkCount.ToString() + "_START", 
                        "Customer1_Talk" + Cus1TalkCount.ToString() + "_END",
                        rpgTalk.txtToParse, this, "AfterCusTalk");
                }
                InitiateTalk = false;
            }

            if (RideCus02)
            {
                Cus2TalkCount = PlayerPrefs.GetInt("Cus2TalkCount"); //initial = 1;
                if (Cus2TalkCount == 5)
                {
                    LevelManager.LvMg.bgm_source.Stop();
                    rpgTalk.NewTalk("Customer2_Talk5_START", "Cus2Talk5_CutScene1Start", rpgTalk.txtToParse, this, "Cus2Talk5CutScene1");
                }
                else
                {
                    rpgTalk.NewTalk("Customer2_Talk" + Cus2TalkCount.ToString() + "_START",
                        "Customer2_Talk" + Cus2TalkCount.ToString() + "_END",
                        rpgTalk.txtToParse, this, "AfterCusTalk");
                }
                InitiateTalk = false;
            }

            if (RideCus03)
            {
                Cus3TalkCount = PlayerPrefs.GetInt("Cus3TalkCount"); //initial = 1;
                if (Cus3TalkCount == 5)
                {
                    LevelManager.LvMg.bgm_source.Stop();
                    rpgTalk.NewTalk("Customer3_Talk5_START", "Customer3_Talk5_END", rpgTalk.txtToParse, this, "Cus3Talk5CutScene1");
                }
                else
                {
                    rpgTalk.NewTalk("Customer3_Talk" + Cus3TalkCount.ToString() + "_START",
                        "Customer3_Talk" + Cus3TalkCount.ToString() + "_END",
                        rpgTalk.txtToParse, this, "AfterCusTalk");
                }
                InitiateTalk = false;
            }

            if (RideCus04)
            {
                Cus4TalkCount = PlayerPrefs.GetInt("Cus4TalkCount"); //initial = 1;
                if (Cus4TalkCount == 5)
                {
                    LevelManager.LvMg.bgm_source.Stop();
                    rpgTalk.NewTalk("Customer4_Talk5_START", "Cus4Talk5_CutScene1Start", rpgTalk.txtToParse, this, "Cus4Talk5CutScene1");
                }
                else
                {
                    rpgTalk.NewTalk("Customer4_Talk" + Cus4TalkCount.ToString() + "_START",
                        "Customer4_Talk" + Cus4TalkCount.ToString() + "_END",
                        rpgTalk.txtToParse, this, "AfterCusTalk");
                }
                InitiateTalk = false;
            }
        }
	}

    //declare after talk with customers (for all possible conditions)
    public IEnumerator AfterTalk(int CurrentCusTalkCount, string CustomerTalkCount, int PayMoney, int GoodEndMoney, string badendData)
    {
        //need to show stop car animation first (if not bad end)
        //if (CurrentCusTalkCount < 5)
        //{
        //    yield return new WaitForSeconds(1.0f);
        //    LevelManager.LvMg.canMove = false;
        //    yield return new WaitForSeconds(0.1f);
        //    if (GoodEnd) //first check if need to trigger GoodEnd Event (+ money)
        //        LevelManager.LvMg.ReceiveReward(PayMoney + GoodEndMoney);
        //    else
        //        LevelManager.LvMg.ReceiveReward(PayMoney); //then resume with normal payouts (no pay at BadEnd)
        //    yield return new WaitForSeconds(0.5f);
        //    StartCoroutine(LevelManager.LvMg.AfterRideProcess());
        //    yield return new WaitForSeconds(1.0f);
        //    if (RideCus01)
        //        RideCus01 = false;
        //    if (RideCus02)
        //        RideCus02 = false;
        //    if (RideCus03)
        //        RideCus03 = false;
        //    if (RideCus04)
        //        RideCus04 = false;
        //}
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
            PlayerPrefs.SetInt("BadEndCount", PlayerPrefs.GetInt("BadEndCount") + 1);
            GoodEnd = false;
            //display Game over too
            LevelManager.LvMg.GameOver = true;
        }
        else
        {
            PlayerPrefs.SetInt(CustomerTalkCount, CurrentCusTalkCount);
            GoodEnd = false;
        }
        LevelManager.LvMg.canTalk = false;
        InitiateTalk = true;
        //LevelManager.LvMg.oneTime = true;
		selectiveDisplay.SetActive(false);
		yield return null;
    }

	public void ReachTrigger() //use for calling coroutine from other script
	{
		StartCoroutine(DestinationReached(LevelManager.LvMg.C1_DisplayedPay, LevelManager.LvMg.Customer1GoodEndPay));
	}

	public IEnumerator DestinationReached(int PayMoney, int GoodEndMoney)
	{
		LevelManager.LvMg.canMove = false;
		Player.playerInstance.canControl = false;
		selectiveDisplay.SetActive(true);		
		if (GoodEnd) //first check if need to trigger GoodEnd Event (+ money)
			LevelManager.LvMg.ReceiveReward(PayMoney + GoodEndMoney);
		else
			LevelManager.LvMg.ReceiveReward(PayMoney); //then resume with normal payouts (no pay at BadEnd)
		
		yield return new WaitForSeconds(0.5f);
		StartCoroutine(LevelManager.LvMg.AfterRideProcess());
		yield return new WaitForSeconds(1.0f);
		if (RideCus01)
			RideCus01 = false;
		if (RideCus02)
			RideCus02 = false;
		if (RideCus03)
			RideCus03 = false;
		if (RideCus04)
			RideCus04 = false;		
		LevelManager.LvMg.oneTime = true;
	}

	public void AfterCusTalk()
    {
        if (RideCus01)
        {
            StartCoroutine(AfterTalk(Cus1TalkCount, "Cus1TalkCount", LevelManager.LvMg.C1_DisplayedPay, LevelManager.LvMg.Customer1GoodEndPay, "Cus1BadEnd"));
            //RideCus01 = false;
        }

        else if (RideCus02)
        {
            StartCoroutine(AfterTalk(Cus2TalkCount, "Cus2TalkCount", LevelManager.LvMg.C2_DisplayedPay, LevelManager.LvMg.Customer2GoodEndPay, "Cus2BadEnd"));
            //RideCus02 = false;
        }
        else if (RideCus03)
        {
            StartCoroutine(AfterTalk(Cus3TalkCount, "Cus3TalkCount", LevelManager.LvMg.C3_DisplayedPay, LevelManager.LvMg.Customer3GoodEndPay, "Cus3BadEnd"));
            //RideCus03 = false;
        }
        else if (RideCus04)
        {
            StartCoroutine(AfterTalk(Cus4TalkCount, "Cus4TalkCount", LevelManager.LvMg.C4_DisplayedPay, LevelManager.LvMg.Customer4GoodEndPay, "Cus4BadEnd"));
            //RideCus04 = false;
        }
        else
            return;
    }
    //END of declare

    //declare of all extra animations in Talk 5
    //for Cus1, Talk5
    public void Cus1Talk5CutScene1()
    {
        StopAllCoroutines();
        StartCoroutine(C1T5Cut1TImings());
    }
    public void Cus1Talk5CutScene2()
    {
        StopAllCoroutines();
        StartCoroutine(C1T5Cut2Timings());
    }
    public IEnumerator C1T5Cut1TImings()
    {
        LevelManager.LvMg.TaxiZigZagAnim();
        rpgTalk.NewTalk("Cus1Talk5_CutScene1End", "Customer1_Talk5_END", rpgTalk.txtToParse, this, "Cus1Talk5CutScene2");
        StartCoroutine(LevelManager.LvMg.PlayOnce_Sfx(LevelManager.LvMg.sfx_source, LevelManager.LvMg.killerLaugh_sfx, 1.0f));
        yield return new WaitForSeconds(LevelManager.LvMg.killerLaugh_sfx.length / 2);
        //LevelManager.LvMg.sfx_source.Stop();
    }
    public IEnumerator C1T5Cut2Timings()
    {
        LevelManager.LvMg.TaxiRamToWall();
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(FadeLoader.FadeSLoad.Fading("null"));
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(LevelManager.LvMg.PlayOnce_Sfx(LevelManager.LvMg.sfx_source, LevelManager.LvMg.carCrash_sfx, 1.0f));
        yield return new WaitForSeconds(LevelManager.LvMg.carCrash_sfx.length - 1.0f);
        StartCoroutine(LevelManager.LvMg.PlayOnce_Sfx(LevelManager.LvMg.sfx_source, LevelManager.LvMg.explosion_sfx, 1.0f));
        yield return new WaitForSeconds(LevelManager.LvMg.explosion_sfx.length);
        LevelManager.LvMg.sfx_source.Stop();
        AfterCusTalk();
    }

    //for Cus2, Talk5
    public void Cus2Talk5CutScene1()
    {
        StopAllCoroutines();
        StartCoroutine(C2T5Cut1Timings());
    }
    public void Cus2Talk5CutScene2()
    {
        StopAllCoroutines();
        StartCoroutine(C2T5Cut2Timings());
    }
    public IEnumerator C2T5Cut1Timings()
    {
        StartCoroutine(LevelManager.LvMg.PlayOnce_Sfx(LevelManager.LvMg.sfx_source, LevelManager.LvMg.drawDagger_sfx, 1.0f));
        yield return new WaitForSeconds(LevelManager.LvMg.drawDagger_sfx.length);
        LevelManager.LvMg.TaxiZigZagAnim();
        StartCoroutine(LevelManager.LvMg.PlayOnce_Sfx(LevelManager.LvMg.sfx_source, LevelManager.LvMg.fleshRip_sfx, 0.5f));
        yield return new WaitForSeconds(2.0f);
        LevelManager.LvMg.TaxiIdleAnim();
        LevelManager.LvMg.canMove = false;
        yield return new WaitForSeconds(0.5f);
        LevelManager.LvMg.sfx_source.Stop();
        rpgTalk.NewTalk("Cus2Talk5_CutScene1End", "Customer2_Talk5_END", rpgTalk.txtToParse, this, "Cus2Talk5CutScene2");
    }
    public IEnumerator C2T5Cut2Timings()
    {
        StartCoroutine(FadeLoader.FadeSLoad.Fading("null"));
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(LevelManager.LvMg.PlayOnce_Sfx(LevelManager.LvMg.sfx_source, LevelManager.LvMg.fleshRip_sfx, 0.7f));
        StartCoroutine(LevelManager.LvMg.PlayOnce_Sfx(LevelManager.LvMg.sfx_source, LevelManager.LvMg.manScream_sfx, 1.0f));
        yield return new WaitForSeconds(4.0f);
        LevelManager.LvMg.sfx_source.Stop();
        AfterCusTalk();
    }

    //for Cus3, Talk5
    public void Cus3Talk5CutScene1()
    {
        StopAllCoroutines();
        StartCoroutine(C3T5Cut1Timings());
    }

    public IEnumerator C3T5Cut1Timings()
    {
        StartCoroutine(FadeLoader.FadeSLoad.Fading("null"));
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(LevelManager.LvMg.PlayOnce_Sfx(LevelManager.LvMg.sfx_source, LevelManager.LvMg.gunShot_sfx, 0.6f));
        yield return new WaitForSeconds(LevelManager.LvMg.gunShot_sfx.length);
        LevelManager.LvMg.sfx_source.Stop();
        AfterCusTalk();
    }
    
    //for Cus4, Talk5
    public void Cus4Talk5CutScene1()
    {
        StopAllCoroutines();
        StartCoroutine(C4T5Cut1Timings());
    }
    public void Cus4Talk5CutScene2()
    {
        StopAllCoroutines();
        StartCoroutine(C4T5Cut2Timings());
    }
    public void Cus4Talk5CutScene3()
    {
        StopAllCoroutines();
        StartCoroutine(C4T5Cut3Timings());
    }
    public IEnumerator C4T5Cut1Timings()
    {
        StartCoroutine(FadeLoader.FadeSLoad.Fading("null"));
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(LevelManager.LvMg.PlayOnce_Sfx(LevelManager.LvMg.sfx_source, LevelManager.LvMg.openCarDoor_sfx, 1.0f));
        yield return new WaitForSeconds(LevelManager.LvMg.openCarDoor_sfx.length);
        LevelManager.LvMg.sfx_source.Stop();
        rpgTalk.NewTalk("Cus4Talk5_CutScene1End", "Cus4Talk5_CutScene2Start", rpgTalk.txtToParse, this, "Cus4Talk5CutScene2");
    }
    public IEnumerator C4T5Cut2Timings()
    {
        StartCoroutine(LevelManager.LvMg.PlayOnce_Sfx(LevelManager.LvMg.sfx_source, LevelManager.LvMg.gunShot_sfx, 1.0f));
        yield return new WaitForSeconds(LevelManager.LvMg.gunShot_sfx.length / 2);
        StartCoroutine(LevelManager.LvMg.PlayOnce_Sfx(LevelManager.LvMg.sfx_source, LevelManager.LvMg.closeCarDoor_sfx, 1.0f));
        yield return new WaitForSeconds(LevelManager.LvMg.closeCarDoor_sfx.length);
        LevelManager.LvMg.sfx_source.Stop();
        rpgTalk.NewTalk("Cus4Talk5_CutScene2End", "Customer4_Talk5_END", rpgTalk.txtToParse, this, "Cus4Talk5CutScene3");
    }
    public IEnumerator C4T5Cut3Timings()
    {
        for (int t = 0; t<4; t++)
        {
            StartCoroutine(LevelManager.LvMg.PlayOnce_Sfx(LevelManager.LvMg.sfx_source, LevelManager.LvMg.gunShot_sfx, 1.0f));
            yield return new WaitForSeconds(LevelManager.LvMg.gunShot_sfx.length / 8);
        }
        yield return new WaitForSeconds(3.0f);
        LevelManager.LvMg.sfx_source.Stop();
        AfterCusTalk();
    }

    //End of all Talk5 cutscenes

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

        PlayerPrefs.SetInt("BadEndCount", 0);
    }
}
