using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
	
	private float movingDistance=1; // how far the taxi move
	public int blockX=0,blockY=0; //to restrict the movement of the taxi, not going beyond the border	

	private int randomNo;//to determine the action after triggering the random event
	public bool canControl = false; //freeze the control if something is happening
	public static Player playerInstance = null;
	//private GameObject selectiveDisplay; 
	//public GameObject gameManager;


	private void Awake()
	{
		if (playerInstance == null)
			playerInstance = this;
	}

	//private void Start()
	//{
	//	gameManager = GameObject.Find("GameManager");
	//	selectiveDisplay = GameObject.Find("SelectiveDisplay");
	//}

	private void Update()
	{
		#region UserInput
		if (Input.GetKeyDown("a") && blockX > 0)
		{
			transform.Translate(Vector3.left * movingDistance);
			blockX -= 1;
			GetComponent<SpriteRenderer>().flipX = true;
			//move progress bar
		}

		if (Input.GetKeyDown("d") && blockX < 3)
		{
			transform.Translate(Vector3.right  * movingDistance);
			blockX += 1;
			GetComponent<SpriteRenderer>().flipX = false;
			//move progress bar
		}

		if (Input.GetKeyDown("w")&&blockY < 3)
		{
			transform.Translate(Vector3.up *  movingDistance);
			blockY += 1;
			//move progress bar
		}
		if (Input.GetKeyDown("s")&&blockY >0)
		{
			transform.Translate(Vector3.down *  movingDistance);
			blockY -= 1;
			//move progress bar
		}
		#endregion



	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag=="Block")
		{
			Destroy(other.gameObject);
			Debug.Log("You triggered road block");
			//progress bar +2
		}

		if (other.tag == "Chat")
		{
			Destroy(other.gameObject);
			Debug.Log("TALK SHIT");
			//display chat window
		}

		if (other.tag == "Destination")
		{
			Destroy(other.gameObject);
			Debug.Log("You've reached ur destination");
			BoardManager.boardManagerInstance.ResetBoard();			
		}
		if (other.tag == "Event")
		{
			Destroy(other.gameObject);
			Debug.Log("You triggered event");
			randomNo = Random.Range(0, 4); //0 is normal route, 1 is shortcut, 2 is roadblock, 3 is tips
			if (randomNo==1)
			{
				Debug.Log("Shortut Triggered");		
				//progress bar stop moving
			}
			else if (randomNo==2)
			{
				Debug.Log("You triggered road block");		
				//progress bar +2
			}
			else if(randomNo==3)
			{
				Debug.Log("You got some tipss");	
				//generate tips
			}

		}
		if (other.tag == "Shortcut")
		{
			Destroy(other.gameObject);
			Debug.Log("Shortut Triggered");
			//progress bar stop moving

		}
		if (other.tag == "Tips")
		{
			Destroy(other.gameObject);
			Debug.Log("You got some tipss");
			//generate tips
		}
	}



}
