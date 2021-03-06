﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
	
	private float movingDistance=1; // how far the taxi move
	private int blockX=0,blockY=0; //to restrict the movement of the taxi	

	private int randomNo;//to determine the action after triggering the random event

	private void Update()
	{
		#region UserInput
		if (Input.GetKeyDown("a") && blockX > 0)
		{
			transform.Translate(Vector3.left * movingDistance);
			blockX -= 1;
			GameManager.managerInstance.stepsCount += 1;
		}

		if (Input.GetKeyDown("d") && blockX < 3)
		{
			transform.Translate(Vector3.right  * movingDistance);
			blockX += 1;
			GameManager.managerInstance.stepsCount += 1;
		}

		if (Input.GetKeyDown("w")&&blockY < 3)
		{
			transform.Translate(Vector3.up *  movingDistance);
			blockY += 1;
			GameManager.managerInstance.stepsCount += 1;
		}
		if (Input.GetKeyDown("s")&&blockY >0)
		{
			transform.Translate(Vector3.down *  movingDistance);
			blockY -= 1;
			GameManager.managerInstance.stepsCount += 1;
		}
		#endregion



	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag=="Block")
		{
			Debug.Log("You triggered road block");
			GameManager.managerInstance.stepsCount += 2;
		}

		if (other.tag == "Chat")
		{
			Debug.Log("TALK SHIT");
		}

		if (other.tag == "Destination")
		{
			Debug.Log("You've reached ur destination");
			GameManager.managerInstance.boardScript.ResetBoard();
			
		}
		if (other.tag == "Event")
		{
			Debug.Log("You triggered event");
			randomNo = Random.Range(0, 4); //0 is normal route, 1 is shortcut, 2 is roadblock, 3 is tips
			if (randomNo==1)
			{
				Debug.Log("Shortut Triggered");
				GameManager.managerInstance.stepsCount -= 2;
			}
			else if (randomNo==2)
			{
				Debug.Log("You triggered road block");
				GameManager.managerInstance.stepsCount += 2;
			}
			else if(randomNo==3)
			{
				Debug.Log("You got some tipss");
				GameManager.managerInstance.TipsGenerator();
			}

		}
		if (other.tag == "Shortcut")
		{
			Debug.Log("Shortut Triggered");
			GameManager.managerInstance.stepsCount -= 2;
		}
		if (other.tag == "Tips")
		{
			Debug.Log("You got some tipss");
			GameManager.managerInstance.TipsGenerator();
		}
	}



}
