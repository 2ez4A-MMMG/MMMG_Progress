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
	public int stepsCount;

    [Header("game main var's limits")]
    public int DaysLimit = 3;
    public int EarnTarget = 10000;
    public int ProgressBarLimit = 30;

    [Header("linked vars + gameObjs")]
    public bool canMove;
    public bool canTalk;
    public bool oneTime = true;

    //UI parts
    public GameObject StatusPopup; //display Status popup
    public GameObject BeforeLvDayDisplay;
    public Text DayDisplay;
    public Text MoneyDisplay;
    public GameObject SelectCMenu;
    public GameObject C1_Button;
    public GameObject C2_Button;
    public GameObject C3_Button;
    public GameObject C4_Button;
    //audio parts
    public AudioSource sfx_source;
    public AudioSource bgm_source;
    public AudioClip killerLaugh_sfx;
    public AudioClip carCrash_sfx;
    public AudioClip explosion_sfx;
    public AudioClip drawDagger_sfx;
    public AudioClip fleshRip_sfx;
    public AudioClip manScream_sfx;
    public AudioClip gunShot_sfx;
    public AudioClip openCarDoor_sfx;
    public AudioClip closeCarDoor_sfx;
    //gameObject parts
    public GameObject Taxi;
    public GameObject driverSpawner;
    public GameObject customerSpawner;
    public GameObject driver;
    public GameObject c1_businessman;
    public GameObject c2_femaleStudent;
    public GameObject c3_oldMan;
    public GameObject c4_partTimer;

    [Header("Customer1(BUsinessMan) Variables")] //businessman
    //public GameObject Customer1Model;
    public int Customer1Pay_Min = 300;
    public int Customer1Pay_Max = 500;
    public int Customer1ExtraTips_Min = 50;
    public int Customer1ExtraTips_Max = 100;
    public int Customer1GoodEndPay = 2500;
    public int C1_DisplayedPay;//set the price before customer selection

    [Header("Customer2(FStudent) Variables")] //female Student
    public int Customer2Pay_Min = 200;
    public int Customer2Pay_Max = 400;
    public int Customer2ExtraTips_Min = 10;
    public int Customer2ExtraTips_Max = 80;
    public int Customer2GoodEndPay = 2000;
    public int C2_DisplayedPay;//set the price before customer selection

    [Header("Customer3(Gramps) Variables")] //old gramps
    public int Customer3Pay_Min = 100;
    public int Customer3Pay_Max = 300;
    public int Customer3ExtraTips_Min = 70;
    public int Customer3ExtraTips_Max = 150;
    public int Customer3GoodEndPay = 3000;
    public int C3_DisplayedPay;//set the price before customer selection

    [Header("Customer4(PartTimer) Variables")] //part-timer
    public int Customer4Pay_Min = 300;
    public int Customer4Pay_Max = 500;
    public int Customer4ExtraTips_Min = 30;
    public int Customer4ExtraTips_Max = 50;
    public int Customer4GoodEndPay = 2000;
    public int C4_DisplayedPay;//set the price before customer selection

    [Header("Customer5(secret) Variables")] //???
    public bool canAppear = false;

    private bool dayStartRunning;
    private bool dayEnding;
    // Use this for initialization
    void Awake () {
        LvMg = this;
        canMove = false;
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
        //same goes for progress bar
        if (!PlayerPrefs.HasKey("ProgressBarValue"))
        {
            PlayerPrefs.SetInt("ProgressBarValue", 0);
            ProgressBar = 0;
        }
        else
        {
            ProgressBar = PlayerPrefs.GetInt("ProgressBarValue");
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
        }
        else
        {
            SelectCMenu.SetActive(true);
            if (oneTime) //P/S: set to true after every ride
            {
                CustomersPriceSet();
                oneTime = false;
            }
        }

		if(stepsCount>=ProgressBarLimit)
		{
			Day += 1;
			stepsCount = 0;
		}
	}

    public IEnumerator dayStart()
    {
        dayStartRunning = true;
        yield return new WaitForSeconds(0.5f);
        //spawn driver
        GameObject Driver = Instantiate(driver, driverSpawner.transform.position, driverSpawner.transform.rotation);
        //driver enters the car
        Animator driverAnim = Driver.GetComponent<Animator>();
        driverAnim.Play("DriverEnterTaxi");
        yield return new WaitForSeconds(driverAnim.GetCurrentAnimatorStateInfo(0).length - closeCarDoor_sfx.length);
        StartCoroutine(PlayOnce_Sfx(sfx_source, closeCarDoor_sfx, 1.0f));
        yield return new WaitForSeconds(closeCarDoor_sfx.length);
        Destroy(Driver);
        Destroy(BeforeLvDayDisplay);
        bgm_source.Play();
        dayStartRunning = false;
    }

    //customer selection functions + set price per ride
    public void CustomersPriceSet()
    {
        //randomize values for each customer's pay
        Text C1_RandomPayText = C1_Button.GetComponentInChildren<Text>();
        C1_DisplayedPay = Random.Range(Customer1Pay_Min, Customer1Pay_Max + 1);
        C1_RandomPayText.text = "$ " + C1_DisplayedPay.ToString();

        Text C2_RandomPayText = C2_Button.GetComponentInChildren<Text>();
        C2_DisplayedPay = Random.Range(Customer2Pay_Min, Customer2Pay_Max + 1);
        C2_RandomPayText.text = "$ " + C2_DisplayedPay.ToString();

        Text C3_RandomPayText = C3_Button.GetComponentInChildren<Text>();
        C3_DisplayedPay = Random.Range(Customer3Pay_Min, Customer3Pay_Max + 1);
        C3_RandomPayText.text = "$ " + C3_DisplayedPay.ToString();

        Text C4_RandomPayText = C4_Button.GetComponentInChildren<Text>();
        C4_DisplayedPay = Random.Range(Customer4Pay_Min, Customer4Pay_Max + 1);
        C4_RandomPayText.text = "$ " + C4_DisplayedPay.ToString();
    }

    public IEnumerator CustomerSelected() //chg into Ienumerator
    {
        if (DialogueManager.DialMg.RideCus01)
        {
            //spawn customer 1
            GameObject Customer1 = Instantiate(c1_businessman, customerSpawner.transform.position, customerSpawner.transform.rotation);
            Animator cus1Anim = Customer1.GetComponent<Animator>();
            cus1Anim.Play("BmEnterTaxi");
            yield return new WaitForSeconds(cus1Anim.GetCurrentAnimatorStateInfo(0).length - closeCarDoor_sfx.length);
            StartCoroutine(PlayOnce_Sfx(sfx_source, closeCarDoor_sfx, 1.0f));
            yield return new WaitForSeconds(closeCarDoor_sfx.length);
            Destroy(Customer1);
        }
        else if (DialogueManager.DialMg.RideCus02)
        {
            //spawn customer 2
            GameObject Customer2 = Instantiate(c2_femaleStudent, customerSpawner.transform.position, customerSpawner.transform.rotation);
            Animator cus2Anim = Customer2.GetComponent<Animator>();
            cus2Anim.Play("SgEnterTaxi");
            yield return new WaitForSeconds(cus2Anim.GetCurrentAnimatorStateInfo(0).length - closeCarDoor_sfx.length);
            StartCoroutine(PlayOnce_Sfx(sfx_source, closeCarDoor_sfx, 1.0f));
            yield return new WaitForSeconds(closeCarDoor_sfx.length);
            Destroy(Customer2);
        }
        else if (DialogueManager.DialMg.RideCus03)
        {
            //spawn customer 3
            GameObject Customer3 = Instantiate(c3_oldMan, customerSpawner.transform.position, customerSpawner.transform.rotation);
            Animator cus3Anim = Customer3.GetComponent<Animator>();
            cus3Anim.Play("OmEnterTaxi");
            yield return new WaitForSeconds(cus3Anim.GetCurrentAnimatorStateInfo(0).length - closeCarDoor_sfx.length);
            StartCoroutine(PlayOnce_Sfx(sfx_source, closeCarDoor_sfx, 1.0f));
            yield return new WaitForSeconds(closeCarDoor_sfx.length);
            Destroy(Customer3);
        }
        else if (DialogueManager.DialMg.RideCus04)
        {
            //spawn customer 4
            GameObject Customer4 = Instantiate(c4_partTimer, customerSpawner.transform.position, customerSpawner.transform.rotation);
            Animator cus4Anim = Customer4.GetComponent<Animator>();
            cus4Anim.Play("PTEnterTaxi");
            yield return new WaitForSeconds(cus4Anim.GetCurrentAnimatorStateInfo(0).length - closeCarDoor_sfx.length);
            StartCoroutine(PlayOnce_Sfx(sfx_source, closeCarDoor_sfx, 1.0f));
            yield return new WaitForSeconds(closeCarDoor_sfx.length);
            Destroy(Customer4);
        }
        //moves car & then triggers chat
        canMove = true;
        yield return new WaitForSeconds(2.0f);
        canTalk = true;
    }

    public IEnumerator AfterRideProcess()
    {
        dayEnding = true;
        int pBValue = Random.Range(5, 10);
        ProgressBar += pBValue; //initial progressValue
        PlayerPrefs.SetInt("ProgressBarValue", ProgressBar);
        if (DialogueManager.DialMg.RideCus01)
        {
            //spawn customer 1
            sfx_source.PlayOneShot(closeCarDoor_sfx, 1.0f);
            yield return new WaitForSeconds(1.0f);
            GameObject Customer1 = Instantiate(c1_businessman, customerSpawner.transform.position, customerSpawner.transform.rotation);
            Animator cus1Anim = Customer1.GetComponent<Animator>();
            cus1Anim.Play("BmExitTaxi"); 
            yield return new WaitForSeconds(cus1Anim.GetCurrentAnimatorStateInfo(0).length - closeCarDoor_sfx.length / 2);
            Destroy(Customer1);
        }
        else if (DialogueManager.DialMg.RideCus02)
        {
            //spawn customer 2
            sfx_source.PlayOneShot(closeCarDoor_sfx, 1.0f);
            yield return new WaitForSeconds(1.0f);
            GameObject Customer2 = Instantiate(c2_femaleStudent, customerSpawner.transform.position, customerSpawner.transform.rotation);
            Animator cus2Anim = Customer2.GetComponent<Animator>();
            cus2Anim.Play("SgExitTaxi");
            yield return new WaitForSeconds(cus2Anim.GetCurrentAnimatorStateInfo(0).length - closeCarDoor_sfx.length / 2);
            Destroy(Customer2);
        }
        else if (DialogueManager.DialMg.RideCus03)
        {
            //spawn customer 3
            sfx_source.PlayOneShot(closeCarDoor_sfx, 1.0f);
            yield return new WaitForSeconds(1.0f);
            GameObject Customer3 = Instantiate(c3_oldMan, customerSpawner.transform.position, customerSpawner.transform.rotation);
            Animator cus3Anim = Customer3.GetComponent<Animator>();
            cus3Anim.Play("OmExitTaxi");
            yield return new WaitForSeconds(cus3Anim.GetCurrentAnimatorStateInfo(0).length - closeCarDoor_sfx.length / 2);
            Destroy(Customer3);
        }
        else if (DialogueManager.DialMg.RideCus04)
        {
            //spawn customer 4
            sfx_source.PlayOneShot(closeCarDoor_sfx, 1.0f);
            yield return new WaitForSeconds(1.0f);
            GameObject Customer4 = Instantiate(c4_partTimer, customerSpawner.transform.position, customerSpawner.transform.rotation);
            Animator cus4Anim = Customer4.GetComponent<Animator>();
            cus4Anim.Play("PTExitTaxi");
            yield return new WaitForSeconds(cus4Anim.GetCurrentAnimatorStateInfo(0).length - closeCarDoor_sfx.length / 2);
            Destroy(Customer4);
        }
        if (ProgressBar >= ProgressBarLimit)
        {
            //car moves, then fades
            canMove = true;
            PlayerPrefs.DeleteKey("ProgressBarValue");
            yield return new WaitForSeconds(1.0f);
            bgm_source.Stop();
            //end current day & start new day
            if (Day == DaysLimit)
            {
                if (Money >= EarnTarget)
                    GameClear = true;
                else
                    GameOver = true;
            } else
            {
                PlayerPrefs.SetInt("DayCount", PlayerPrefs.GetInt("DayCount") + 1);
                StartCoroutine(FadeLoader.FadeSLoad.Fading(SceneManager.GetActiveScene().name));
            }
        } //else resumes selection
        dayEnding = false;
    }

    public void ReceiveReward(int amount)
    {
        //add money to your keep
        Money += amount;
        PopupSlideIn("$" + amount.ToString() + " GET", "StatusText_UP");
        PlayerPrefs.SetInt("MoneyEarned", Money); //only occur after ride ends
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

    //Audio Controller - playsoundeffectsONCE
    public void PlaySfxOnce(AudioClip sound, float volume)
    {
        StartCoroutine(PlayOnce_Sfx(sfx_source, sound, volume));
    }
    public IEnumerator PlayOnce_Sfx(AudioSource audiosource, AudioClip sfx, float volume)
    {
        audiosource.PlayOneShot(sfx, volume);
        yield return new WaitForSeconds(sfx.length);
    }
}
