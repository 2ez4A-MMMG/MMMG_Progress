using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

    //initiate variables needed for outside variables - in PUBLIC form
    //(mostly for those related to button inputs)

    public static LevelManager LvMg;
    [Header ("basic vars")]
    public int Day;
    public int Money;
    public int ProgressBar;

    [Header ("game main var's limits")]
    public int DaysLimit = 3;
    public int EarnTarget = 10000;
    public int ProgressBarLimit = 30;
    public GameObject StatusPopup; //??

    [Header("linked vars")]
    public bool canMove;
    public bool canTalk;

	// Use this for initialization
	void Awake () {
        LvMg = this;
        canMove = false;
        canTalk = false;
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

    void ReceiveReward(int amount)
    {
        //add money to your keep
        Money += amount;
        //show animation
    }
}
