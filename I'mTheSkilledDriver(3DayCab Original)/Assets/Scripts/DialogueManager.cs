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

    public GameObject HoodieGirl_jsImg;

    public bool SkipButtonPressed = false;
    public int Cus1TalkCount, Cus2TalkCount, Cus3TalkCount, Cus4TalkCount;
    private bool InitiateTalk;

    // Use this for initialization
    void Awake () {

        HoodieGirl_jsImg.SetActive(false);

        if (!PlayerPrefs.HasKey("Cus1TalkCount") || !PlayerPrefs.HasKey("Cus2TalkCount") || 
            !PlayerPrefs.HasKey("Cus3TalkCount") || !PlayerPrefs.HasKey("Cus4TalkCount"))
        {
            RESET_AllTalkCount();
        }
        if (!PlayerPrefs.HasKey("Cus1BadEnd") || !PlayerPrefs.HasKey("Cus2BadEnd") ||
            !PlayerPrefs.HasKey("Cus3BadEnd") || !PlayerPrefs.HasKey("Cus4BadEnd"))
        {
            RESET_AllBadEnds();
        }

        ResetTempTalkCounts(); //reset the temporary save data on talk counts
        DialMg = this;
        InitiateTalk = true;
    }

    void Start()
    {
        if (PlayerPrefs.GetString("talkCountSAVED") == "true")
        {
            PlayerPrefs.SetInt("C1TC_Temp", PlayerPrefs.GetInt("Cus1TalkCount")); //initial = 1;
            PlayerPrefs.SetInt("C2TC_Temp", PlayerPrefs.GetInt("Cus2TalkCount")); //initial = 1;
            PlayerPrefs.SetInt("C3TC_Temp", PlayerPrefs.GetInt("Cus3TalkCount")); //initial = 1;
            PlayerPrefs.SetInt("C4TC_Temp", PlayerPrefs.GetInt("Cus4TalkCount")); //initial = 1;
            PlayerPrefs.SetString("talkCountSAVED", "false");
        }
    }

    // Update is called once per frame
    void Update () {
		//customer selected
        if (InitiateTalk && LevelManager.LvMg.canTalk)
        {
            if (RideCus01)
            {
                Cus1TalkCount = PlayerPrefs.GetInt("C1TC_Temp"); //initial = 1;

                if (Cus1TalkCount == 5)
                {
                    SoundManager.soundMg.bgm_source.Stop();
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
                Cus2TalkCount = PlayerPrefs.GetInt("C2TC_Temp"); //initial = 1;

                if (Cus2TalkCount == 5)
                {
                    SoundManager.soundMg.bgm_source.Stop();
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
                Cus3TalkCount = PlayerPrefs.GetInt("C3TC_Temp"); //initial = 1;

                if (Cus3TalkCount == 5)
                {
                    SoundManager.soundMg.bgm_source.Stop();
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
                Cus4TalkCount = PlayerPrefs.GetInt("C4TC_Temp"); //initial = 1;

                if (Cus4TalkCount == 5)
                {
                    SoundManager.soundMg.bgm_source.Stop();
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
            if (RideCus05)
            {
                SoundManager.soundMg.bgm_source.Stop();
                rpgTalk.NewTalk("Customer5_START", "Cus5Talk_CutScene1Start", rpgTalk.txtToParse, this, "Cus5FinalTalkCutScene1");
                InitiateTalk = false;
            }
        }

        //if skip button's pressed, skip chat. only bad ends (chats no.5) cannot be skipped
        if (SkipButtonPressed)
        {
            if (Cus1TalkCount <= 4  && RideCus01 || Cus2TalkCount <= 4 && RideCus02 || 
                Cus3TalkCount <= 4 && RideCus03 || Cus4TalkCount <= 4 && RideCus04) //need to fix
            {
                rpgTalk.EndTalk();
            }
            SkipButtonPressed = false;
        }
	}

    //declare after talk with customers (for all possible conditions)
    public IEnumerator AfterTalk(int CurrentCusTalkCount, string CustomerTalkCount, string CTC_Temp, string badendData)
    {
        //need to show stop car animation first (if not bad end)
        if (CurrentCusTalkCount < 5)
        {
            //LevelManager.LvMg.roadMoveSpeed = 5;
            //yield return new WaitForSeconds(1.0f);
            //LevelManager.LvMg.roadMoveSpeed = 0;
            //yield return new WaitForSeconds(0.1f);
            //yield return StartCoroutine(LevelManager.LvMg.AfterRideProcess());
            CurrentCusTalkCount += 1;//ADD talkcount
            PlayerPrefs.SetInt(CTC_Temp, CurrentCusTalkCount); //temp add
            Debug.Log(CustomerTalkCount + "talkcount +1: " + CurrentCusTalkCount); //display customer talk count
        //    if (RideCus01)
        //        RideCus01 = false;
        //    if (RideCus02)
        //        RideCus02 = false;
        //    if (RideCus03)
        //        RideCus03 = false;
        //    if (RideCus04)
        //        RideCus04 = false;
        }
        
        else if (CurrentCusTalkCount >= 5)
        {
            PlayerPrefs.SetInt(CustomerTalkCount, 1); //reset talkcount
            PlayerPrefs.SetString(badendData, "yes");//save BadEnd data
            PlayerPrefs.SetInt("BadEndCount", PlayerPrefs.GetInt("BadEndCount") + 1);
            Debug.Log(CustomerTalkCount + "BadEnd +1: " + PlayerPrefs.GetInt("BadEndCount")); //display bad ends shown (int)
            LevelManager.LvMg.GameOver = true; //display Game over too
        }
		yield return null;
		LevelManager.LvMg.canTalk = false;
        InitiateTalk = true;
		Player.playerInstance.canControl = true;
        //LevelManager.LvMg.oneTime = true;
    }


    public void AfterCusTalk()
    {
        if (RideCus01) {
            StartCoroutine(AfterTalk(Cus1TalkCount, "Cus1TalkCount", "C1TC_Temp", "Cus1BadEnd"));
        }
        else if (RideCus02) {
            StartCoroutine(AfterTalk(Cus2TalkCount, "Cus2TalkCount", "C2TC_Temp", "Cus2BadEnd"));
        }
        else if (RideCus03) {
            StartCoroutine(AfterTalk(Cus3TalkCount, "Cus3TalkCount", "C3TC_Temp", "Cus3BadEnd"));
        }
        else if (RideCus04) {
            StartCoroutine(AfterTalk(Cus4TalkCount, "Cus4TalkCount", "C4TC_Temp", "Cus4BadEnd"));
        }
        else
            return;
    }
    //for customer 05 (hoodie girl only)
    public void AfterFinalTalk()
    {
        PlayerPrefs.SetInt("BadEndCount", PlayerPrefs.GetInt("BadEndCount") + 1);
        Debug.Log("Final BadEnd (5) == " + PlayerPrefs.GetInt("BadEndCount")); //display bad ends shown (int)
        LevelManager.LvMg.GameOver = true; //display Game over too
    }
    //END of declare

    //declare of all extra animations in Talk 5 - by running Coroutines
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
        SoundManager.soundMg.sfx_source.Stop();
        LevelManager.LvMg.roadMoveSpeed = 10;
        LevelManager.LvMg.TaxiZigZagAnim();
        rpgTalk.NewTalk("Cus1Talk5_CutScene1End", "Customer1_Talk5_END", rpgTalk.txtToParse, this, "Cus1Talk5CutScene2");
        SoundManager.soundMg.Play_sfx(SoundManager.soundMg.killerLaugh_sfx, 1.0f);
        yield return new WaitForSeconds(SoundManager.soundMg.killerLaugh_sfx.length);
    }
    public IEnumerator C1T5Cut2Timings()
    {
        SoundManager.soundMg.sfx_source.Stop();
        LevelManager.LvMg.TaxiRamToWall();
        yield return new WaitForSeconds(0.1f);
        yield return StartCoroutine(FadeLoader.FadeSLoad.Fading("null"));
        SoundManager.soundMg.sfx_source.PlayOneShot(SoundManager.soundMg.carCrash_sfx, 1.0f);
        yield return new WaitForSeconds(SoundManager.soundMg.carCrash_sfx.length - 1.0f);
        SoundManager.soundMg.sfx_source.PlayOneShot(SoundManager.soundMg.explosion_sfx, 1.0f);
        yield return new WaitForSeconds(SoundManager.soundMg.explosion_sfx.length);
        SoundManager.soundMg.sfx_source.Stop();
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
        SoundManager.soundMg.sfx_source.Stop();
        SoundManager.soundMg.Play_sfx(SoundManager.soundMg.drawDagger_sfx, 1.0f);
        yield return new WaitForSeconds(SoundManager.soundMg.drawDagger_sfx.length);
        LevelManager.LvMg.TaxiZigZagAnim();
        LevelManager.LvMg.roadMoveSpeed = 9;
        SoundManager.soundMg.Play_sfx(SoundManager.soundMg.fleshRip_sfx, 0.5f);
        yield return new WaitForSeconds(SoundManager.soundMg.fleshRip_sfx.length);
        LevelManager.LvMg.TaxiIdleAnim();
        LevelManager.LvMg.roadMoveSpeed = 7;
        yield return new WaitForSeconds(0.5f);
        LevelManager.LvMg.roadMoveSpeed = 3;
        yield return new WaitForSeconds(0.5f);
        LevelManager.LvMg.roadMoveSpeed = 0;
        yield return new WaitForSeconds(0.5f);
        rpgTalk.NewTalk("Cus2Talk5_CutScene1End", "Customer2_Talk5_END", rpgTalk.txtToParse, this, "Cus2Talk5CutScene2");
    }
    public IEnumerator C2T5Cut2Timings()
    {
        SoundManager.soundMg.sfx_source.Stop();
        yield return StartCoroutine(FadeLoader.FadeSLoad.Fading("null"));
        SoundManager.soundMg.sfx_source.PlayOneShot(SoundManager.soundMg.manScream_sfx, 1.0f);
        for (int t = 0; t < 3; t++)
        {
            SoundManager.soundMg.sfx_source.PlayOneShot(SoundManager.soundMg.fleshRip_sfx, 1.0f);
            yield return new WaitForSeconds(SoundManager.soundMg.gunShot_sfx.length * 0.75f);
        }
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
        SoundManager.soundMg.sfx_source.Stop();
        yield return StartCoroutine(FadeLoader.FadeSLoad.Fading("null"));
        SoundManager.soundMg.Play_sfx(SoundManager.soundMg.gunShot_sfx, 0.6f);
        yield return new WaitForSeconds(SoundManager.soundMg.gunShot_sfx.length * 0.6f);
        SoundManager.soundMg.Play_sfx(SoundManager.soundMg.gunShot_sfx, 0.7f);
        yield return new WaitForSeconds(SoundManager.soundMg.gunShot_sfx.length);
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
        SoundManager.soundMg.sfx_source.Stop();
        yield return StartCoroutine(FadeLoader.FadeSLoad.Fading("null"));
        SoundManager.soundMg.Play_sfx(SoundManager.soundMg.openCarDoor_sfx, 1.0f);
        yield return new WaitForSeconds(SoundManager.soundMg.openCarDoor_sfx.length);
        SoundManager.soundMg.sfx_source.Stop();
        rpgTalk.NewTalk("Cus4Talk5_CutScene1End", "Cus4Talk5_CutScene2Start", rpgTalk.txtToParse, this, "Cus4Talk5CutScene2");
    }
    public IEnumerator C4T5Cut2Timings()
    {
        SoundManager.soundMg.sfx_source.Stop();
        SoundManager.soundMg.Play_sfx(SoundManager.soundMg.gunShot_sfx, 1.0f);
        yield return new WaitForSeconds(SoundManager.soundMg.gunShot_sfx.length / 2);
        SoundManager.soundMg.sfx_source.PlayOneShot(SoundManager.soundMg.closeCarDoor_sfx, 1.0f);
        yield return new WaitForSeconds(SoundManager.soundMg.closeCarDoor_sfx.length);
        SoundManager.soundMg.sfx_source.Stop();
        rpgTalk.NewTalk("Cus4Talk5_CutScene2End", "Customer4_Talk5_END", rpgTalk.txtToParse, this, "Cus4Talk5CutScene3");
    }
    public IEnumerator C4T5Cut3Timings()
    {
        for (int t = 0; t<4; t++)
        {
            SoundManager.soundMg.sfx_source.PlayOneShot(SoundManager.soundMg.gunShot_sfx, 1.0f);
            yield return new WaitForSeconds(SoundManager.soundMg.gunShot_sfx.length / 8);
        }
        yield return new WaitForSeconds(3.0f);
        AfterCusTalk();
    }

    //for Cus5, Final Talk
    public void Cus5FinalTalkCutScene1()
    {
        StopAllCoroutines();
        StartCoroutine(C5FTCut1Timings());
    }

    public void Cus5FinalTalkCutScene2()
    {
        StopAllCoroutines();
        StartCoroutine(C5FTCut2Timings());
    }

    public IEnumerator C5FTCut1Timings()
    {
        SoundManager.soundMg.sfx_source.Stop();
        HoodieGirl_jsImg.SetActive(true);
        SoundManager.soundMg.sfx_source.loop = true;
        SoundManager.soundMg.Play_sfx(SoundManager.soundMg.glitching_sfx, 1.0f);
        yield return new WaitForSeconds(0.1f);
        rpgTalk.NewTalk("Cus5Talk_CutScene1End", "Customer5_END", rpgTalk.txtToParse, this, "Cus5FinalTalkCutScene2");
    }
    public IEnumerator C5FTCut2Timings()
    {
        yield return new WaitForSeconds(0.3f);
        HoodieGirl_jsImg.SetActive(false);
        SoundManager.soundMg.sfx_source.Stop();
        LevelManager.LvMg.roadMoveSpeed = 12;
        yield return new WaitForSeconds(0.5f);
        LevelManager.LvMg.roadMoveSpeed = 20;
        yield return StartCoroutine(FadeLoader.FadeSLoad.Fading("null"));
        SoundManager.soundMg.Play_sfx(SoundManager.soundMg.carCrash_sfx, 1.0f);
        yield return new WaitForSeconds(SoundManager.soundMg.carCrash_sfx.length);
        AfterFinalTalk();
    }

    //save all customertalkCounts on the playerprefs
    public void SaveAll_CusTalkCounts()
    {
        PlayerPrefs.SetInt("Cus1TalkCount", Cus1TalkCount); //PS Only add if day ends
        PlayerPrefs.SetInt("Cus2TalkCount", Cus2TalkCount);
        PlayerPrefs.SetInt("Cus3TalkCount", Cus3TalkCount);
        PlayerPrefs.SetInt("Cus4TalkCount", Cus4TalkCount);
    }

    //End of all Talk5 cutscenes
    public void RESET_AllTalkCount()
    {
        PlayerPrefs.SetInt("Cus1TalkCount", 1);
        PlayerPrefs.SetInt("Cus2TalkCount", 1);
        PlayerPrefs.SetInt("Cus3TalkCount", 1);
        PlayerPrefs.SetInt("Cus4TalkCount", 1);
    }

    public void ResetTempTalkCounts()
    {
        PlayerPrefs.SetInt("C1TC_Temp", 1);
        PlayerPrefs.SetInt("C2TC_Temp", 1);
        PlayerPrefs.SetInt("C3TC_Temp", 1);
        PlayerPrefs.SetInt("C4TC_Temp", 1);

        PlayerPrefs.SetString("talkCountSAVED", "true");
    }

    public void RESET_AllBadEnds()
    {
        PlayerPrefs.SetString("Cus1BadEnd", "no");
        PlayerPrefs.SetString("Cus2BadEnd", "no");
        PlayerPrefs.SetString("Cus3BadEnd", "no");
        PlayerPrefs.SetString("Cus4BadEnd", "no");

        PlayerPrefs.SetInt("BadEndCount", 0);
    }
}
