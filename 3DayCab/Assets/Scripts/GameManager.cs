using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public BoardManager boardScript;
	public static GameManager instance = null;

	private int level = 1;

	private int dayCount; //to calculate the day
	private int targetSalary; //target salary for three days to determine the winning condition
	private bool gameClear; //to determine the player has won or lost

	//public int salaryRecord;

	// Use this for initialization
	void Awake () {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);

		DontDestroyOnLoad(gameObject); //make sure it doesn't destroy when scene change
		boardScript = GetComponent<BoardManager>();
		InitGame();

	}

	void InitGame()
	{
		boardScript.SetupScene(level);
	}

	public void GameOver()
	{

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
