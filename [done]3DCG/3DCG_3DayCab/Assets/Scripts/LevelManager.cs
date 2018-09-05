using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    //initiate variables needed for outside variables - in PUBLIC form
    //(mostly for those related to button inputs)

    public static LevelManager LvMg;
    [Header("basic vars")]
    public int Day;
    public int Money;
    public int ProgressBar;
    public bool GameClear;
    public bool GameOver;

    [Header("game main var's limits")]
    public int DaysLimit = 3;
    public int EarnTarget = 10000;
    public int ProgressBarLimit = 30;

    [Header("linked vars + gameObjs")]
    public int roadMoveSpeed; //roadMoveSpeed replaces bool canMove. (roadMoveSpeed = 0) == (canMove = false)
    public bool canTalk;
    public bool oneTime = true;

    //UI parts
    [Header("UI Parts")]
    public GameObject StatusPopup; //display Status popup
    public GameObject BeforeLvDayDisplay;
    public Text DayDisplay;
    public Text MoneyDisplay;
    public GameObject SelectCMenu;

    //gameObject parts
    [Header("gameObjects parts")]
    public GameObject Taxi;
    public GameObject driverSpawner;
    public GameObject customerSpawner;
    public GameObject driver;
    public GameObject c1_businessman;
    public GameObject c2_femaleStudent;
    public GameObject c3_oldMan;
    public GameObject c4_partTimer;

    private bool dayStartRunning;
    private bool dayEnding;

    void Awake () {
        LvMg = this;
        roadMoveSpeed = 0;
        canTalk = false;
        GameClear = GameOver = false;
        StatusPopup.SetActive(false);
        //if cannot reteive saved day count, set a new one
        if (!PlayerPrefs.HasKey("DayCount")) {
            PlayerPrefs.SetInt("DayCount", 1);
            Day = 1;
        } else {
            Day = PlayerPrefs.GetInt("DayCount");
        }
        //same goes for money earned
        if (!PlayerPrefs.HasKey("MoneyEarned")) {
            PlayerPrefs.SetInt("MoneyEarned", 0);
            Money = 0;
        } else {
            Money = PlayerPrefs.GetInt("MoneyEarned");
        }
        if (PlayerPrefs.HasKey("GameStatus"))
        {
            PlayerPrefs.DeleteKey("GameStatus");
        }
        Text BeforeDayDisplayText = BeforeLvDayDisplay.GetComponent<Text>();
        BeforeDayDisplayText.text = "Day " + Day.ToString();
        if (PlayerPrefs.GetInt("BadEndCount") != 4)
            StartCoroutine(dayStart());
    }
	
	// Update is called once per frame
	void Update () {
        DayDisplay.text = Day.ToString();
        MoneyDisplay.text = "$ " + Money.ToString();

        if (GameClear || GameOver)
        {
            if (GameClear)
                PlayerPrefs.SetString("GameStatus", "GameClear");//for setting game over/clear screen display
            if (GameOver)
                PlayerPrefs.SetString("GameStatus", "GameOver");
            StartCoroutine(FadeLoader.FadeSLoad.Fading(FadeLoader.FadeSLoad.CarRadioScreen));
            Debug.Log("load gameOver Screen");
        }

        if (DialogueManager.DialMg.RideCus01 || DialogueManager.DialMg.RideCus02 ||
            DialogueManager.DialMg.RideCus03 || DialogueManager.DialMg.RideCus04 || canTalk || dayStartRunning || dayEnding)
        {
            SelectCMenu.SetActive(false);
            //but if customer is selected, have to initiate the enter animation
            if (DialogueManager.DialMg.RideCus01 || DialogueManager.DialMg.RideCus02 ||
                DialogueManager.DialMg.RideCus03 || DialogueManager.DialMg.RideCus04)
            {
                if (oneTime)
                {
                    StartCoroutine(CustomerSelected());
                    Debug.Log("customer selected");
                    oneTime = false;
                }
            }
        } else {
            SelectCMenu.SetActive(true);
            if (oneTime) //P/S: set to true after every ride
            {
                CustomerVariables.CusVars.CustomersPriceSet();
                oneTime = false;
            }
        }
	}

    public IEnumerator dayStart()
    {
        dayStartRunning = true;
        yield return new WaitForSeconds(0.5f);
        //spawn driver & driver enters car
        GameObject Driver = Instantiate(driver, driverSpawner.transform.position, driverSpawner.transform.rotation);
        Animator driverAnim = Driver.GetComponent<Animator>();
        driverAnim.Play("DriverEnterTaxi");
        yield return new WaitForSeconds(driverAnim.GetCurrentAnimatorStateInfo(0).length - SoundManager.soundMg.closeCarDoor_sfx.length);
        SoundManager.soundMg.sfx_source.PlayOneShot(SoundManager.soundMg.closeCarDoor_sfx, 1.0f);
        yield return new WaitForSeconds(SoundManager.soundMg.closeCarDoor_sfx.length);
        Destroy(Driver);
        Destroy(BeforeLvDayDisplay);
        SoundManager.soundMg.bgm_source.Play();
        dayStartRunning = false;
    }

    public IEnumerator CustomerSelected()
    {
        GameObject customer = null;
        Animator CusAnim = null;
        if (DialogueManager.DialMg.RideCus01)
        {
            //spawn customer 1
            customer = Instantiate(c1_businessman, customerSpawner.transform.position, customerSpawner.transform.rotation);
            CusAnim = customer.GetComponent<Animator>();
            CusAnim.Play("BmEnterTaxi");
        }
        else if (DialogueManager.DialMg.RideCus02)
        {
            //spawn customer 2
            customer = Instantiate(c2_femaleStudent, customerSpawner.transform.position, customerSpawner.transform.rotation);
            CusAnim = customer.GetComponent<Animator>();
            CusAnim.Play("SgEnterTaxi");
        }
        else if (DialogueManager.DialMg.RideCus03)
        {
            //spawn customer 3
            customer = Instantiate(c3_oldMan, customerSpawner.transform.position, customerSpawner.transform.rotation);
            CusAnim = customer.GetComponent<Animator>();
            CusAnim.Play("OmEnterTaxi");
        }
        else if (DialogueManager.DialMg.RideCus04)
        {
            //spawn customer 4
            customer = Instantiate(c4_partTimer, customerSpawner.transform.position, customerSpawner.transform.rotation);
            CusAnim = customer.GetComponent<Animator>();
            CusAnim.Play("PTEnterTaxi");
        }
        yield return new WaitForSeconds(CusAnim.GetCurrentAnimatorStateInfo(0).length - SoundManager.soundMg.closeCarDoor_sfx.length);
        SoundManager.soundMg.sfx_source.PlayOneShot(SoundManager.soundMg.closeCarDoor_sfx, 1.0f);
        yield return new WaitForSeconds(SoundManager.soundMg.closeCarDoor_sfx.length);
        Destroy(customer);

        //moves car & then triggers chat
        roadMoveSpeed = 7;
        yield return new WaitForSeconds(2.0f);
        canTalk = true;
    }

    public IEnumerator AfterRideProcess()
    {
        dayEnding = true;

        //progress bar
        int pBValue = Random.Range(5, 10); //pBValue = 1 step initially
        ProgressBar += pBValue; //initial progressValue

        SoundManager.soundMg.sfx_source.PlayOneShot(SoundManager.soundMg.closeCarDoor_sfx, 1.0f);
        yield return new WaitForSeconds(1.0f);
        GameObject Customer = null;
        Animator cusAnim = null;
        if (DialogueManager.DialMg.RideCus01)
        {
            //spawn customer 1
            Customer = Instantiate(c1_businessman, customerSpawner.transform.position, customerSpawner.transform.rotation);
            cusAnim = Customer.GetComponent<Animator>();
            cusAnim.Play("BmExitTaxi"); 
        }
        else if (DialogueManager.DialMg.RideCus02)
        {
            //spawn customer 2
            Customer = Instantiate(c2_femaleStudent, customerSpawner.transform.position, customerSpawner.transform.rotation);
            cusAnim = Customer.GetComponent<Animator>();
            cusAnim.Play("SgExitTaxi");
        }
        else if (DialogueManager.DialMg.RideCus03)
        {
            //spawn customer 3
            Customer = Instantiate(c3_oldMan, customerSpawner.transform.position, customerSpawner.transform.rotation);
            cusAnim = Customer.GetComponent<Animator>();
            cusAnim.Play("OmExitTaxi");
        }
        else if (DialogueManager.DialMg.RideCus04)
        {
            //spawn customer 4
            Customer = Instantiate(c4_partTimer, customerSpawner.transform.position, customerSpawner.transform.rotation);
            cusAnim = Customer.GetComponent<Animator>();
            cusAnim.Play("PTExitTaxi");
        }
        yield return new WaitForSeconds(cusAnim.GetCurrentAnimatorStateInfo(0).length - SoundManager.soundMg.closeCarDoor_sfx.length / 2);
        Destroy(Customer);

        if (ProgressBar >= ProgressBarLimit)
        {
            //car moves, then fades
            roadMoveSpeed = 7;
            yield return new WaitForSeconds(1.0f);
            SoundManager.soundMg.bgm_source.Stop();
            //end current day & start new day
            if (Day == DaysLimit)
            {
                if (Money >= EarnTarget)
                    GameClear = true;
                else
                    GameOver = true;
            } else {
                PlayerPrefs.SetInt("Cus1TalkCount", PlayerPrefs.GetInt("C1TC_Temp")); 
                PlayerPrefs.SetInt("Cus2TalkCount", PlayerPrefs.GetInt("C2TC_Temp")); 
                PlayerPrefs.SetInt("Cus3TalkCount", PlayerPrefs.GetInt("C3TC_Temp")); 
                PlayerPrefs.SetInt("Cus4TalkCount", PlayerPrefs.GetInt("C4TC_Temp")); 
                PlayerPrefs.SetString("talkCountSAVED", "true"); //PS Only add if day ends

                PlayerPrefs.SetInt("MoneyEarned", Money); //save money & daycount
                PlayerPrefs.SetInt("DayCount", PlayerPrefs.GetInt("DayCount") + 1);
                StartCoroutine(FadeLoader.FadeSLoad.Fading(SceneManager.GetActiveScene().name));
            }
        } //if day not end yet, skip & resume
        dayEnding = false;
    }

    public void ReceiveReward(int amount) //add money to your keep - temporary
    {
        Money += amount;
        PopupSlideIn("$" + amount.ToString() + " Get", "StatusText_UP");
    }

    //Animation Controller - status popup
    public void PopupSlideIn(string popup_text, string AnimName)
    {
        StartCoroutine(SpawnPopUp(popup_text, AnimName));
    }

    IEnumerator SpawnPopUp(string popup_text, string playAnim)
    {
        StatusPopup.SetActive(true);
        Text PopUpText = StatusPopup.GetComponentInChildren<Text>();
        PopUpText.text = popup_text;
        Animator PopUpAnim = StatusPopup.GetComponent<Animator>();
        PopUpAnim.Play(playAnim);
        yield return new WaitForSeconds(PopUpAnim.GetCurrentAnimatorStateInfo(0).length);
        StatusPopup.SetActive(false);
    }

    //Animation Controller - taxi model
    public void ChangeTaxiAnim(string playAnim)
    {
        Animator TaxiAnim = Taxi.GetComponent<Animator>();
        TaxiAnim.Play(playAnim);
    }
    public void TaxiIdleAnim()
    {
        ChangeTaxiAnim("taxt_idle");
    }
    public void TaxiZigZagAnim()
    {
        ChangeTaxiAnim("taxi_zigzag");
    }
    public void TaxiRamToWall()
    {
        ChangeTaxiAnim("taxi_ramToWall");
    }

}
