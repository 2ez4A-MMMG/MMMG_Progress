using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBlockCover : MonoBehaviour {

    public GameObject Change;

    // Use this for initialization
    void Awake () {
        Change.SetActive(false);
    }

    void Start()
    {
        if (PlayerPrefs.GetInt("BadEndCount") == 4)
            Change.SetActive(true);
    }
}
