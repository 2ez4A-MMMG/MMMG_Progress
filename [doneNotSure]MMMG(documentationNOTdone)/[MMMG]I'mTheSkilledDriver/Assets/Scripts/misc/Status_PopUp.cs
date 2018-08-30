using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Status_PopUp : MonoBehaviour {

    public static Status_PopUp statusMg;
    public GameObject StatusPopup; //display Status popup

    // Use this for initialization
    void Awake () {
        statusMg = this;
        StatusPopup.SetActive(false);
    }

    public void ReceiveMoney(int amount) //add money to your keep
    {
        LevelManager.LvMg.Money += amount;
        PopupSlideIn("$" + amount.ToString() + " Get", "StatusText_UP");
        Debug.Log("Money Obtained: $" + amount.ToString());
    }

    //nothing happened
    public void NothingHappened()
    {
        PopupSlideIn("nothing", "StatusText_UP");
    }
    //shortcut
    public void EnterShortcut()
    {
        //progress bar didn't move
        PopupSlideIn("Shortcut", "StatusText_UP");
    }
    //roadblock
    public void EnterRoadblock()
    {
        //progress bar move more
        PopupSlideIn("RoadBlocks", "StatusText_DOWN");
    }

	public void DestinationReached()
	{
		//progress bar move more
		PopupSlideIn("Arrived", "StatusText_UP");
	}
	//extra tips
	public void AddExtraTips(int xtraAmount)
    {
		//add extra money into your pay
		LevelManager.LvMg.Money += xtraAmount;
		PopupSlideIn("$" + xtraAmount.ToString() + " Tips", "StatusText_UP");
		Debug.Log("Tips Obtained: $" + xtraAmount.ToString());
	}

    //Animation Controller - status popup
    public void PopupSlideIn(string popup_text, string AnimName)
    {
        StartCoroutine(SpawnPopUp(popup_text, AnimName));
    }

    IEnumerator SpawnPopUp(string popup_text, string playAnim)
    {
		Player.playerInstance.canControl = false;
        StatusPopup.SetActive(true);
        Text PopUpText = StatusPopup.GetComponentInChildren<Text>();
        PopUpText.text = popup_text;
        Animator PopUpAnim = StatusPopup.GetComponent<Animator>();
        PopUpAnim.Play(playAnim);
        yield return new WaitForSeconds(PopUpAnim.GetCurrentAnimatorStateInfo(0).length);
        StatusPopup.SetActive(false);
		if (!Player.playerInstance.destinationReached)
		{
			Player.playerInstance.canControl = true;
			
		}	
	}
}
