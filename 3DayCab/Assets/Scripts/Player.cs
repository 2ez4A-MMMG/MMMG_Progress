using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
	
	private float movingDistance=1; // how far the taxi move
	public int blockX=0,blockY=0; //to restrict the movement of the taxi, not going beyond the border	

	private int randomNo;//to determine the action after triggering the random event
	public GameObject gameManager;
	public static Player playerInstance=null;

	//public bool canMove=false;

	private void Awake()
	{
		if (playerInstance == null)
			playerInstance = this;		
	}

	private void Start()
	{
		gameManager = GameObject.Find("GameManager");
	}
	private void Update()
	{
		
		#region UserInput
		if (Input.GetKeyDown("a") && blockX > 0)
		{
			transform.Translate(Vector3.left * movingDistance);
			blockX -= 1;
			GetComponent<SpriteRenderer>().flipX = true;
			LevelManager.LvMg.stepsCount += 1;
		}

		if (Input.GetKeyDown("d") && blockX < 3)
		{
			transform.Translate(Vector3.right * movingDistance);
			blockX += 1;
			GetComponent<SpriteRenderer>().flipX = false;
			LevelManager.LvMg.stepsCount += 1;
		}

		if (Input.GetKeyDown("w") && blockY < 3)
		{
			transform.Translate(Vector3.up * movingDistance);
			blockY += 1;
			LevelManager.LvMg.stepsCount += 1;
		}
		if (Input.GetKeyDown("s") && blockY > 0)
		{
			transform.Translate(Vector3.down * movingDistance);
			blockY -= 1;
			LevelManager.LvMg.stepsCount += 1;
		}
		
		#endregion



	}

	private void OnTriggerEnter(Collider other)
	{
		//if (other.GetComponent<SpriteRenderer>() != null)
		//	other.GetComponent<SpriteRenderer>().color= new Color(10f, 10f, 10f,0.5f);		
		if (other.tag=="Block")
		{
			Destroy(other.gameObject);
			Debug.Log("You triggered road block");
			LevelManager.LvMg.stepsCount += 2;
		}

		if (other.tag == "Chat")
		{
			Destroy(other.gameObject);
			Debug.Log("TALK SHIT");
		}

		if (other.tag == "Destination")
		{
			Destroy(other.gameObject);
			Debug.Log("You've reached ur destination");
			GameManager.managerInstance.boardScript.ResetBoard();
			
		}
		if (other.tag == "Event")
		{
			Destroy(other.gameObject);
			Debug.Log("You triggered event");
			randomNo = Random.Range(0, 4); //0 is normal route, 1 is shortcut, 2 is roadblock, 3 is tips
			if (randomNo == 1)
			{				
				Debug.Log("Shortut Triggered");
				LevelManager.LvMg.stepsCount -= 2;
			}
			else if (randomNo == 2)
			{				
				
				Debug.Log("You triggered road block");
				LevelManager.LvMg.stepsCount += 2;
			}
			else if (randomNo == 3)
			{				
				Debug.Log("You got some tipss");
				//GameManager.managerInstance.TipsGenerator();
			}
			else
			{
				other.GetComponent<SpriteRenderer>().sprite = gameManager.GetComponent<BoardManager>().roadTiles[0].GetComponent<SpriteRenderer>().sprite;
			}
		}
		if (other.tag == "Shortcut")
		{
			Destroy(other.gameObject);
			Debug.Log("Shortut Triggered");
			LevelManager.LvMg.stepsCount -= 2;
		}
		if (other.tag == "Tips")
		{
			Destroy(other.gameObject);
			Debug.Log("You got some tipss");
			//GameManager.managerInstance.TipsGenerator();
		}
	}



}
