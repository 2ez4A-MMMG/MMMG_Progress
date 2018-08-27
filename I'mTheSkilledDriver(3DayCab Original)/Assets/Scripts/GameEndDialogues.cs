using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEndDialogues : MonoBehaviour {

    public RPGTalk rpgtalk;
    public GameObject FadeScreen;
    public GameObject GameStatus;
    private bool startTalk;
    private bool runOneTime;

    void Awake()
    {
        GameStatus.SetActive(false);
        startTalk = false;
        runOneTime = true;
    }

    void Start()
    {
        if (PlayerPrefs.GetString("GameStatus") == "GameOver")
        {
            if (PlayerPrefs.GetString("Cus1BadEnd") == "yes")
            {
                ChangeTalkContent("Customer1_BEStart", "Customer1_BEEnd");
                ChangeTalkCallback("StartShowingGameStatus");
                startTalk = true;
                runOneTime = false;
                PlayerPrefs.SetString("Cus1BadEnd", "yes_shown");
            }
            else if (PlayerPrefs.GetString("Cus2BadEnd") == "yes")
            {
                ChangeTalkContent("Customer2_BEStart", "Customer2_BEEnd");
                ChangeTalkCallback("StartShowingGameStatus");
                startTalk = true;
                runOneTime = false;
                PlayerPrefs.SetString("Cus2BadEnd", "yes_shown");
            }
            else if (PlayerPrefs.GetString("Cus3BadEnd") == "yes")
            {
                ChangeTalkContent("Customer3_BEStart", "Customer3_BEEnd");
                ChangeTalkCallback("StartShowingGameStatus");
                startTalk = true;
                runOneTime = false;
                PlayerPrefs.SetString("Cus3BadEnd", "yes_shown");
            }
            else if (PlayerPrefs.GetString("Cus4BadEnd") == "yes")
            {
                ChangeTalkContent("Customer4_BEStart", "Customer4_BEEnd");
                ChangeTalkCallback("StartShowingGameStatus");
                startTalk = true;
                runOneTime = false;
                PlayerPrefs.SetString("Cus4BadEnd", "yes_shown");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if game over, runs talk first, then runs coroutine
        if (startTalk)
        {
            rpgtalk.NewTalk();
            startTalk = false;
        }
        else
        {
            if (runOneTime)
            {
                StartCoroutine(ShowGameStatus());
                runOneTime = false;
            }
        }
    }

    protected void StartShowingGameStatus()
    {
        StartCoroutine(ShowGameStatus());
    }

    IEnumerator ShowGameStatus()
    {
        GameStatus.SetActive(true);
        Text StatusText = GameStatus.GetComponent<Text>();
        if (PlayerPrefs.GetString("GameStatus") == "GameClear")
        {
            StatusText.text = "GAME CLEAR";
            StatusText.color = Color.green;
        }
        else if (PlayerPrefs.GetString("GameStatus") == "GameOver")
        {
            StatusText.text = "GAME OVER";
            StatusText.color = Color.red;
        }
        Animator GSAnim = GameStatus.GetComponent<Animator>();
        GSAnim.Play("GameOverDisplay");
        yield return new WaitForSeconds(GSAnim.GetCurrentAnimatorStateInfo(0).length * 3 / 4);

        //reset day & money's & game status playerprefs
        PlayerPrefs.DeleteKey("DayCount");
        PlayerPrefs.DeleteKey("MoneyEarned");
        PlayerPrefs.DeleteKey("GameStatus");
        PlayerPrefs.SetInt("Cus1TalkCount", 1);
        PlayerPrefs.SetInt("Cus2TalkCount", 1);
        PlayerPrefs.SetInt("Cus3TalkCount", 1);
        PlayerPrefs.SetInt("Cus4TalkCount", 1);
        //Fading() fade to title screen
        yield return StartCoroutine(FadeLoader.FadeSLoad.Fading(FadeLoader.FadeSLoad.TitleScreen));
    }

    //rpgtalk stuff. Don't bother
    protected void ChangeTalkContent(string newLineSTART, string newLineEND)
    {
        rpgtalk.lineToStart = newLineSTART;
        rpgtalk.lineToBreak = newLineEND;
    }

    protected void ChangeTalkCallback(string newCallBackFunctionName)
    {
        rpgtalk.callbackScript = this;
        rpgtalk.callbackFunction = newCallBackFunctionName;
    }

    public void SkipRadioCallChat()
    {
        rpgtalk.EndTalk();
    }
}
