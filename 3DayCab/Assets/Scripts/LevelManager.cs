using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    //initiate variables needed for outside variables - in PUBLIC form
    //(mostly for those related to button inputs)

    public static LevelManager LvMg;
    [Header ("basic vars")]
    public int Day;
    public int Money;
    public int ProgressBar;
    public bool GameClear;
    public bool GameOver;

    [Header ("game main var's limits")]
    public int DaysLimit = 3;
    public int EarnTarget = 10000;
    public int ProgressBarLimit = 30;

    [Header("linked vars + gameObjs")]
    public bool canMove;
    public bool canTalk;
    //UI parts
    public GameObject StatusPopup; //display Status popup
    public GameObject FadeScreen;
    //audio parts
    public AudioSource sfx_source;
    public AudioClip carCrash_sfx;
    public AudioClip explosion_sfx;
    //gameObject parts
    public GameObject Taxi;
    //public GameObject Customer;

    [Header("Customer1(BUsinessMan) Variables")] //businessman
    //public GameObject Customer1Model;
    public int Customer1Pay_Min = 300;
    public int Customer1Pay_Max = 500;
    public int Customer1ExtraTips_Min = 50;
    public int Customer1ExtraTips_Max = 100;
    public int Customer1GoodEndPay = 2500;
    [SerializeField] private int C1_DisplayedPay;//set the price before customer selection

    [Header("Customer2(FStudent) Variables")] //female Student
    public int Customer2Pay_Min = 200;
    public int Customer2Pay_Max = 400;
    public int Customer2ExtraTips_Min = 10;
    public int Customer2ExtraTips_Max = 80;
    public int Customer2GoodEndPay = 2000;

    [Header("Customer3(Gramps) Variables")] //old gramps
    public int Customer3Pay_Min = 100;
    public int Customer3Pay_Max = 300;
    public int Customer3ExtraTips_Min = 70;
    public int Customer3ExtraTips_Max = 150;
    public int Customer3GoodEndPay = 3000;

    [Header("Customer4(PartTimer) Variables")] //part-timer
    public int Customer4Pay_Min = 300;
    public int Customer4Pay_Max = 500;
    public int Customer4ExtraTips_Min = 30;
    public int Customer4ExtraTips_Max = 50;
    public int Customer4GoodEndPay = 2000;

    [Header("Customer5(secret) Variables")] //???
    public bool canAppear = false;

    // Use this for initialization
    void Awake () {
        LvMg = this;
        canMove = false;
        canTalk = false;
        GameClear = GameOver = false;
        StatusPopup.SetActive(false);
        //only works when scene is forcefully reset

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
    }
	
	// Update is called once per frame
	void Update () {
		
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

    //Animation Controller - fade screen
    public void Fades()
    {
        StartCoroutine(Fading());
    }
    public IEnumerator Fading()
    {
        Animator FadeAnim = FadeScreen.GetComponent<Animator>();
        FadeAnim.SetBool("Fade", true);
        yield return new WaitForSeconds(FadeAnim.GetCurrentAnimatorStateInfo(0).length);
        //SceneManager.LoadScene(name_of_scene);
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
    public void PlaySfxOnce(AudioClip sound)
    {
        StartCoroutine(PlayOnce_Sfx(sfx_source, sound));
    }
    public IEnumerator PlayOnce_Sfx(AudioSource audiosource, AudioClip sfx)
    {
        audiosource.PlayOneShot(sfx);
        yield return new WaitForSeconds(sfx.length);
    }
}
