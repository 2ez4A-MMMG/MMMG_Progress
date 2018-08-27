using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameEndDisplay : MonoBehaviour {

    public GameObject FadeScreen;
    public GameObject GameStatus;
    public GameObject BadNews01;
    public GameObject BadNews02;
    public GameObject BadNews03;
    public GameObject BadNews04;
    public GameObject BadNews05;

    private void Awake()
    {
        GameStatus.SetActive(false);
        BadNews01.SetActive(false);
        BadNews02.SetActive(false);
        BadNews03.SetActive(false);
        BadNews04.SetActive(false);
        BadNews05.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
        StartCoroutine(ShowGameStatus());
        //if game clear/over by normal means, immediately starts coroutine
        //if game over by BAD END, runs talk first, then runs coroutine
	}

    IEnumerator ShowGameStatus()
    {
        if (PlayerPrefs.GetString("GameStatus") == "GameClear")
        {
            GameStatus.SetActive(true);
            Text StatusText = GameStatus.GetComponent<Text>();
            StatusText.text = "GAME CLEAR";
            StatusText.color = Color.green;
            Animator GSAnim = GameStatus.GetComponent<Animator>();
            GSAnim.Play("GameOverDisplay");
            yield return new WaitForSeconds(GSAnim.GetCurrentAnimatorStateInfo(0).length + 0.1f);
        }
        else if (PlayerPrefs.GetString("GameStatus") == "GameOver")
        {
            if (PlayerPrefs.GetString("Cus1BadEnd") == "yes")
            {
                BadNews01.SetActive(true);
                Animator BNAnim = BadNews01.GetComponent<Animator>();
                BNAnim.Play("CarRadioRoll");
                yield return new WaitForSeconds(BNAnim.GetCurrentAnimatorStateInfo(0).length + 0.1f);
                BadNews01.SetActive(false);
                PlayerPrefs.SetString("Cus1BadEnd", "yes_shown");
            }
            else if (PlayerPrefs.GetString("Cus2BadEnd") == "yes")
            {
                BadNews02.SetActive(true);
                Animator BNAnim = BadNews02.GetComponent<Animator>();
                BNAnim.Play("CarRadioRoll");
                yield return new WaitForSeconds(BNAnim.GetCurrentAnimatorStateInfo(0).length + 0.1f);
                BadNews02.SetActive(false);
                PlayerPrefs.SetString("Cus2BadEnd", "yes_shown");
            }
            else if (PlayerPrefs.GetString("Cus3BadEnd") == "yes")
            {
                BadNews03.SetActive(true);
                Animator BNAnim = BadNews03.GetComponent<Animator>();
                BNAnim.Play("CarRadioRoll");
                yield return new WaitForSeconds(BNAnim.GetCurrentAnimatorStateInfo(0).length + 0.1f);
                BadNews03.SetActive(false);
                PlayerPrefs.SetString("Cus3BadEnd", "yes_shown");
            }
            else if (PlayerPrefs.GetString("Cus4BadEnd") == "yes")
            {
                BadNews04.SetActive(true);
                Animator BNAnim = BadNews04.GetComponent<Animator>();
                BNAnim.Play("CarRadioRoll");
                yield return new WaitForSeconds(BNAnim.GetCurrentAnimatorStateInfo(0).length + 0.1f);
                BadNews04.SetActive(false);
                PlayerPrefs.SetString("Cus4BadEnd", "yes_shown");// for checking on cus 05
            }
            GameStatus.SetActive(true);
            Text StatusText = GameStatus.GetComponent<Text>();
            StatusText.text = "GAME OVER";
            StatusText.color = Color.red;
            Animator GSAnim = GameStatus.GetComponent<Animator>();
            GSAnim.Play("GameOverDisplay");
            yield return new WaitForSeconds(GSAnim.GetCurrentAnimatorStateInfo(0).length + 0.1f);

        }
        yield return new WaitForSeconds(1.0f);
        //reset day & money's & game status playerprefs
        PlayerPrefs.DeleteKey("DayCount");
        PlayerPrefs.DeleteKey("MoneyEarned");
        PlayerPrefs.DeleteKey("GameStatus");
        PlayerPrefs.DeleteKey("ProgressBarValue");
        PlayerPrefs.SetInt("Cus1TalkCount", 1);
        PlayerPrefs.SetInt("Cus2TalkCount", 1);
        PlayerPrefs.SetInt("Cus3TalkCount", 1);
        PlayerPrefs.SetInt("Cus4TalkCount", 1);
        //Fading() fade to title screen
        yield return StartCoroutine(FadeLoader.FadeSLoad.Fading(FadeLoader.FadeSLoad.TitleScreen));
    }

}
