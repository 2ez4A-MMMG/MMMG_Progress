using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarManager : MonoBehaviour {

    public static ProgressBarManager pBarMg;

    public int progressValue;
    public int minValue;
    public int maxValue;
    private Slider progressBar;

    private void Awake()
    {
        pBarMg = this;
        progressBar = GetComponent<Slider>();
        //progressBar.maxValue = LevelManager.LvMg.ProgressBarLimit;
    }

    void Update()
    {
        progressValue = LevelManager.LvMg.ProgressBar;
        minValue = 0;
        maxValue = LevelManager.LvMg.ProgressBarLimit;
        UpdateProgressBar();
        progressValue = Mathf.Clamp(progressValue, minValue, maxValue);
    }

    void UpdateProgressBar()
    {
        progressBar.value = progressValue;
    }
}
