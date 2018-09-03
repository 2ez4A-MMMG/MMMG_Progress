using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBlockCover : MonoBehaviour {

    public GameObject Change;
    public int badEndCount = 4;

    public bool exactNo = true;
    public bool moreThan_equalTo = false;

    // Use this for initialization
    void Awake () {
        Change.SetActive(false);
    }

    void Start()
    {
        if (exactNo)
            if (PlayerPrefs.GetInt("BadEndCount") == badEndCount)
                Change.SetActive(true);

        if (moreThan_equalTo)
            if (PlayerPrefs.GetInt("BadEndCount") >= badEndCount)
                Change.SetActive(true);
    }
}
