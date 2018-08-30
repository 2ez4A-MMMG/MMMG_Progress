using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverER : MonoBehaviour {

    [Header("For which Part")]
    public bool forTitleScreen;
    public bool forGame;

    [Header("Parts to Add/Change")]
    public GameObject Change00;
    public GameObject Change01;
    public GameObject Change02;
    public GameObject Change03;
    public GameObject Change04;

    private void Awake()
    {
        if (Change00 != null)
            Change00.SetActive(false);
        if (Change01 != null)
            Change01.SetActive(false);
        if (Change02 != null)
            Change02.SetActive(false);
        if (Change03 != null)
            Change03.SetActive(false);
        if (Change04 != null)
            Change04.SetActive(false);
    }

    // Use this for initialization
    void Start () {
		if (forGame)
        {
            if (PlayerPrefs.GetString("Cus1BadEnd") == "yes_shown")
                Change01.SetActive(true);
            if (PlayerPrefs.GetString("Cus2BadEnd") == "yes_shown")
                Change02.SetActive(true);
            if (PlayerPrefs.GetString("Cus3BadEnd") == "yes_shown")
                Change03.SetActive(true);
            if (PlayerPrefs.GetString("Cus4BadEnd") == "yes_shown")
                Change04.SetActive(true);
        }

        if (forTitleScreen)
        {
            if (PlayerPrefs.GetInt("BadEndCount") == 0)
                if (Change00 != null)
                    Change00.SetActive(true);
            if (PlayerPrefs.GetInt("BadEndCount") == 1)
                Change01.SetActive(true);
            if (PlayerPrefs.GetInt("BadEndCount") == 2)
                Change02.SetActive(true);
            if (PlayerPrefs.GetInt("BadEndCount") == 3)
                Change03.SetActive(true);
            if (PlayerPrefs.GetInt("BadEndCount") == 4)
                Change04.SetActive(true);
        }
	}
}
