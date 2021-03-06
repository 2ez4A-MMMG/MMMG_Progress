﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public BoardManager boardScript;
	public static GameManager managerInstance = null;

	public Text dayCountText,stepsCountText;
	public int dayCount, stepsCountMax = 5, stepsCount;

	public int tips, tipsMax = 100, tipsMin = 50;	


	#region SceneSetup
	// Use this for initialization
	void Awake () {
		if (managerInstance == null)
			managerInstance = this;
		else if (managerInstance != this)
			Destroy(gameObject);

		DontDestroyOnLoad(gameObject); //make sure it doesn't destroy when scene change
		boardScript = GetComponent<BoardManager>();
		InitGame();
	}

	public void InitGame()
	{
		boardScript.SetupScene();
	}
	#endregion

	private void Update()
	{
		if (stepsCount >= stepsCountMax)
		{
			stepsCount = 0;
			dayCount += 1;
			//Debug.Log("NewDay");
			if (dayCount >= 3)
			{
				//Debug.Log("GameOver");
			}
		}

		
	}

	public void TipsGenerator()
	{
		tips = Random.Range(tipsMin, tipsMax);
	}


}
