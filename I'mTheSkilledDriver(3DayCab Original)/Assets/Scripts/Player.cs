﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
	
	private float movingDistance=1; // how far the taxi move
	public int blockX=0,blockY=0; //to restrict the movement of the taxi, not going beyond the border	

	private int randomNo;//to determine the action after triggering the random event
	public bool canControl = true; //freeze the control if something is happening
	public static Player playerInstance = null;
	private GameObject selectiveDisplay; 
	//public GameObject gameManager;


	private void Awake()
	{
		if (playerInstance == null)
			playerInstance = this;
	}

	private void Start()
	{
		//gameManager = GameObject.Find("GameManager");
		selectiveDisplay = GameObject.Find("SelectiveDisplay");
	}

	private void Update()
	{
		#region UserInput
		//if (canControl==true)
		//{			
			if (Input.GetKeyDown("a") && blockX > 0)
			{
				transform.Translate(Vector3.left * movingDistance);
				blockX -= 1;
				GetComponent<SpriteRenderer>().flipX = true;
				//LevelManager.LvMg.ProgressBar += 1;
			}

			if (Input.GetKeyDown("d") && blockX < 3)
			{
				transform.Translate(Vector3.right * movingDistance);
				blockX += 1;
				GetComponent<SpriteRenderer>().flipX = false;
				//LevelManager.LvMg.ProgressBar += 1;//move progress bar
			}

			if (Input.GetKeyDown("w") && blockY < 3)
			{
				transform.Translate(Vector3.up * movingDistance);
				blockY += 1;
				//LevelManager.LvMg.ProgressBar += 1;
			}
			if (Input.GetKeyDown("s") && blockY > 0)
			{
				transform.Translate(Vector3.down * movingDistance);
				blockY -= 1;
				//LevelManager.LvMg.ProgressBar += 1;
			}
		//}
		#endregion



	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag=="Block")
		{
			Destroy(other.gameObject);
			Debug.Log("You triggered road block");
			//LevelManager.LvMg.ProgressBar += 2;
			Status_PopUp.statusMg.EnterRoadblock();
		}

		if (other.tag == "Chat")
		{
			Destroy(other.gameObject);
			Debug.Log("TALK SHIT");
			//LevelManager.LvMg.oneTime = true;			
		}

		if (other.tag == "Destination")
		{
			Destroy(other.gameObject);
			Debug.Log("You've reached ur destination");
			BoardManager.boardManagerInstance.ResetBoard();
			Status_PopUp.statusMg.DestinationReached();
		}
		if (other.tag == "Event")
		{
			Destroy(other.gameObject);
			Debug.Log("You triggered event");
			randomNo = Random.Range(0, 4); //0 is normal route, 1 is shortcut, 2 is roadblock, 3 is tips
			if (randomNo==1)
			{
				Debug.Log("Shortut Triggered");
				Status_PopUp.statusMg.EnterShortcut();
				//progress bar stop moving
			}
			else if (randomNo==2)
			{
				Debug.Log("You triggered road block");
				Status_PopUp.statusMg.EnterRoadblock();
				//LevelManager.LvMg.ProgressBar += 2;
			}
			else if(randomNo==3)
			{
				Debug.Log("You got some tipss");
				tipsGeneration();
				//generate tips
			}

		}
		if (other.tag == "Shortcut")
		{
			Destroy(other.gameObject);
			Debug.Log("Shortut Triggered");
			Status_PopUp.statusMg.EnterShortcut();
			//progress bar stop moving

		}
		if (other.tag == "Tips")
		{
			Destroy(other.gameObject);
			Debug.Log("You got some tipss");
			tipsGeneration();
			//generate tips
		}		
	}
	private void tipsGeneration()
	{
		int finalTips = 0;
		if (DialogueManager.DialMg.RideCus01)
		{
			finalTips = Random.Range(CustomerVariables.CusVars.Customer1ExtraTips_Min, CustomerVariables.CusVars.Customer1ExtraTips_Max + 1);
		}
		else if (DialogueManager.DialMg.RideCus02)
		{
			finalTips = Random.Range(CustomerVariables.CusVars.Customer2ExtraTips_Min, CustomerVariables.CusVars.Customer2ExtraTips_Max + 1);
		}
		else if (DialogueManager.DialMg.RideCus03)
		{
			finalTips = Random.Range(CustomerVariables.CusVars.Customer3ExtraTips_Min, CustomerVariables.CusVars.Customer3ExtraTips_Max + 1);
		}
		else if (DialogueManager.DialMg.RideCus04)
		{
			finalTips = Random.Range(CustomerVariables.CusVars.Customer4ExtraTips_Min, CustomerVariables.CusVars.Customer4ExtraTips_Max + 1);
		}
		Status_PopUp.statusMg.AddExtraTips(finalTips);
	}


}
