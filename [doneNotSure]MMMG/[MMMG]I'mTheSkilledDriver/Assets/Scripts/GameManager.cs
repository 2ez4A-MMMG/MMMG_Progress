using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public BoardManager boardScript;
	public static GameManager managerInstance = null;

	#region SceneSetup
	// Use this for initialization
	void Awake () {
		if (managerInstance == null)
			managerInstance = this;
		else if (managerInstance != this)
			Destroy(gameObject);

		//DontDestroyOnLoad(gameObject); //make sure it doesn't destroy when scene change
		boardScript = GetComponent<BoardManager>();
		InitGame();
	}

	public void InitGame()
	{
		boardScript.SetupScene();
	}
	#endregion
	
	
}
