using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

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
    public int roadMoveSpeed;
    public bool canTalk;
    public bool oneTime = true;

    //UI parts
    [Header("UI Parts")]
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
    public GameObject cSecret_hoodieGirl;

    private bool dayStartRunning;
    private bool dayEnding;

	public GameObject selectiveDisplay;
    public bool gamePaused;
    void Awake () {
        //set tutorial count
        if (!PlayerPrefs.HasKey("TutorialDone") || PlayerPrefs.GetString("TutorialDone") == "false")
        {
            PlayerPrefs.SetString("TutorialDone", "false");
            Debug.Log("tutorialdone = false;");
            gamePaused = true;
        }
        else
            gamePaused = false;
        //PlayerPrefs.SetInt("BadEndCount", 4);
        LvMg = this;
        roadMoveSpeed = 0;
        canTalk = false;
        GameClear = GameOver = false;
        //if cannot reteive saved day count, set a new one
        if (!PlayerPrefs.HasKey("DayCount")) {
            PlayerPrefs.SetInt("DayCount", 1);
            Day = PlayerPrefs.GetInt("DayCount");
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
        if (!BeforeLvDayDisplay.activeSelf)
            BeforeLvDayDisplay.SetActive(true);
        Text BeforeDayDisplayText = BeforeLvDayDisplay.GetComponent<Text>();
        BeforeDayDisplayText.text = "Day " + Day.ToString();
        if (PlayerPrefs.GetInt("BadEndCount") != 5)
            StartCoroutine(dayStart());
        BeforeLvDayDisplay.SetActive(false);
        selectiveDisplay = GameObject.Find("SelectiveDisplay");
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
            DialogueManager.DialMg.RideCus03 || DialogueManager.DialMg.RideCus04 ||
            DialogueManager.DialMg.RideCus05 || canTalk || dayStartRunning || dayEnding)
        {
            SelectCMenu.SetActive(false);
            //but if customer is selected, have to initiate the enter animation
            if (DialogueManager.DialMg.RideCus01 || DialogueManager.DialMg.RideCus02 ||
                DialogueManager.DialMg.RideCus03 || DialogueManager.DialMg.RideCus04 || DialogueManager.DialMg.RideCus05)
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
        if (gamePaused)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
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
        else if (DialogueManager.DialMg.RideCus05)
        {
            //spawn customer 5 (secret)
            customer = Instantiate(cSecret_hoodieGirl, customerSpawner.transform.position, customerSpawner.transform.rotation);
            CusAnim = customer.GetComponent<Animator>();
            CusAnim.Play("HgEnterTaxi");
        }
        yield return new WaitForSeconds(CusAnim.GetCurrentAnimatorStateInfo(0).length - SoundManager.soundMg.closeCarDoor_sfx.length);
        SoundManager.soundMg.sfx_source.PlayOneShot(SoundManager.soundMg.closeCarDoor_sfx, 1.0f);
        yield return new WaitForSeconds(SoundManager.soundMg.closeCarDoor_sfx.length);
        Destroy(customer);

        //moves car & then triggers chat
        roadMoveSpeed = 7;
        yield return new WaitForSeconds(0.5f);
        //canTalk = true;
        if (DialogueManager.DialMg.RideCus05)
        {
            canTalk = true;
        } else {
            BoardManager.boardManagerInstance.board.SetActive(true); 
            Debug.Log("customerselected()-> board.setActive true");
            BoardManager.boardManagerInstance.playerP.SetActive(true);
            Player.playerInstance.canControl = true;
			Debug.Log("Can control turn on, when customer enter the car(level manager)");
		}
	}

	

    public IEnumerator AfterRideProcess()
    {
        dayEnding = true;
		//calculate money earned
		AfterRidePayment();
        yield return new WaitForSeconds(0.5f);

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

		if (DialogueManager.DialMg.RideCus01)
			DialogueManager.DialMg.RideCus01 = false;
		if (DialogueManager.DialMg.RideCus02)
			DialogueManager.DialMg.RideCus02 = false;
		if (DialogueManager.DialMg.RideCus03)
			DialogueManager.DialMg.RideCus03 = false;
		if (DialogueManager.DialMg.RideCus04)
			DialogueManager.DialMg.RideCus04 = false;

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

	public IEnumerator DestinationArrived()
	{
		LevelManager.LvMg.roadMoveSpeed = 5;
		yield return new WaitForSeconds(1.0f);
		LevelManager.LvMg.roadMoveSpeed = 0;
		yield return new WaitForSeconds(0.1f);
		yield return StartCoroutine(LevelManager.LvMg.AfterRideProcess());
		BoardManager.boardManagerInstance.board = GameObject.Find("Board");
		BoardManager.boardManagerInstance.playerP = GameObject.Find("PlayerPrefab(Clone)");
		BoardManager.boardManagerInstance.board.SetActive(false);
		BoardManager.boardManagerInstance.playerP.SetActive(false);
        Debug.Log("destinationarrived()-> board.setActive false");
        selectiveDisplay.SetActive(true);

		oneTime = true;
		Debug.Log("onetime set to TRUE");
		BoardManager.boardManagerInstance.ResetBoard();
	}

	public void AfterRidePayment()
    {
        if (DialogueManager.DialMg.RideCus01) {
            BasicPayment(DialogueManager.DialMg.Cus1TalkCount, CustomerVariables.CusVars.C1_DisplayedPay);
        }
        if (DialogueManager.DialMg.RideCus02) {
            BasicPayment(DialogueManager.DialMg.Cus2TalkCount, CustomerVariables.CusVars.C2_DisplayedPay);
        }
        if (DialogueManager.DialMg.RideCus03) {
            BasicPayment(DialogueManager.DialMg.Cus3TalkCount, CustomerVariables.CusVars.C3_DisplayedPay);
        }
        if (DialogueManager.DialMg.RideCus04) {
            BasicPayment(DialogueManager.DialMg.Cus4TalkCount, CustomerVariables.CusVars.C4_DisplayedPay);
        }
    }

    public void BasicPayment(int CurrentCusTalkCount, int PayMoney)
    {
        Status_PopUp.statusMg.ReceiveMoney(PayMoney); //then resume with normal payouts
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