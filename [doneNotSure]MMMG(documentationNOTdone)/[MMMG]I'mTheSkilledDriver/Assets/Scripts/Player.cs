using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
	
	private float movingDistance=1; // how far the taxi move
	public int blockX=0,blockY=0; //to restrict the movement of the taxi, not going beyond the border	

	private int randomNo;//to determine the action after triggering the random event
	public bool canControl = true; //freeze the control if something is happening
	public static Player playerInstance = null;
	public bool destinationReached;

	private Vector3 fp;   //First touch position
	private Vector3 lp;   //Last touch position
	private float dragDistance;  //minimum distance for a swipe to be registered


	private int stepCount = 1;


	private void Awake()
	{
		if (playerInstance == null)
			playerInstance = this;
	}

	private void Start()
	{
		dragDistance = Screen.height * 0.1f; //dragDistance is 15% height of the screen
	}

	private void Update()
	{
        #region MobileInput
        if (canControl)
        {
            if (Input.touchCount == 1) // user is touching the screen with a single touch
            {
                Debug.Log("Touched!");
                Touch touch = Input.GetTouch(0); // get the touch
                if (touch.phase == TouchPhase.Began) //check for the first touch
                {
                    fp = touch.position;
                    lp = touch.position;
                }
                else if (touch.phase == TouchPhase.Moved) // update the last position based on where they moved
                {
                    lp = touch.position;
                }
                else if (touch.phase == TouchPhase.Ended) //check if the finger is removed from the screen
                {
                    lp = touch.position;  //last touch position. Ommitted if you use list

                    //Check if drag distance is greater than 20% of the screen height
                    if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance)
                    {//It's a drag
                     //check if the drag is vertical or horizontal
                        if (Mathf.Abs(lp.x - fp.x) > Mathf.Abs(lp.y - fp.y))
                        {   //If the horizontal movement is greater than the vertical movement...
                            if ((lp.x > fp.x) && blockX < 3)  //If the movement was to the right)
                            {   //Right swipe
                                transform.Translate(Vector3.right * movingDistance);
                                blockX += 1;
                                GetComponent<SpriteRenderer>().flipX = false;
                                LevelManager.LvMg.ProgressBar += stepCount;//move progress bar
                            }
                            else if ((lp.x < fp.x) && blockX > 0)
                            {   //Left swipe
                                transform.Translate(Vector3.left * movingDistance);
                                blockX -= 1;
                                GetComponent<SpriteRenderer>().flipX = true;
                                LevelManager.LvMg.ProgressBar += stepCount;
                            }
                        }
                        else
                        {   //the vertical movement is greater than the horizontal movement
                            if (lp.y > fp.y && blockY < 3)  //If the movement was up
                            {   //Up swipe
                                transform.Translate(Vector3.up * movingDistance);
                                blockY += 1;
                                LevelManager.LvMg.ProgressBar += stepCount;

                            }
                            else if (lp.y < fp.y && blockY > 0)
                            {   //Down swipe
                                transform.Translate(Vector3.down * movingDistance);
                                blockY -= 1;
                                LevelManager.LvMg.ProgressBar += stepCount;
                            }
                        }
                    }
                    else
                    {   //It's a tap as the drag distance is less than 20% of the screen height
                        //transform.Translate(Vector3.zero);
                        Debug.Log("Tap");
                    }
                }
            }
        }
        
        #endregion

        #region UserPCInput
        if (canControl)
		{
			if (Input.GetKeyDown("a") && blockX > 0)
			{
				transform.Translate(Vector3.left * movingDistance);
				blockX -= 1;
				GetComponent<SpriteRenderer>().flipX = true;
				LevelManager.LvMg.ProgressBar += stepCount;
			}

			if (Input.GetKeyDown("d") && blockX < 3)
			{
				transform.Translate(Vector3.right * movingDistance);
				blockX += 1;
				GetComponent<SpriteRenderer>().flipX = false;
				LevelManager.LvMg.ProgressBar += stepCount;//move progress bar
			}

			if (Input.GetKeyDown("w") && blockY < 3)
			{
				transform.Translate(Vector3.up * movingDistance);
				blockY += 1;
				LevelManager.LvMg.ProgressBar += stepCount;
			}
			if (Input.GetKeyDown("s") && blockY > 0)
			{
				transform.Translate(Vector3.down * movingDistance);
				blockY -= 1;
				LevelManager.LvMg.ProgressBar += stepCount;
			}
		}

		#endregion
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag=="Block")
		{
			Destroy(other.gameObject);
			Debug.Log("You triggered road block");
			LevelManager.LvMg.ProgressBar += (stepCount+1);
			Status_PopUp.statusMg.EnterRoadblock();
		}

		if (other.tag == "Chat")
		{
			Destroy(other.gameObject);
			Debug.Log("TALK SHIT");
			LevelManager.LvMg.canTalk = true;
			canControl = false;
			Debug.Log("Can control turn off, chat triggered(player script)");
		}

		if (other.tag == "Destination")
		{
			destinationReached = true;
			canControl = false;
			Debug.Log("Can control turn off, destination reached(player script)");
			Destroy(other.gameObject);
			Debug.Log("You've reached ur destination");
			
			Status_PopUp.statusMg.DestinationReached();
			StartCoroutine(LevelManager.LvMg.DestinationArrived());
			//BoardManager.boardManagerInstance.ResetBoard();


		}
		if (other.tag == "Event")
		{
			Destroy(other.gameObject);
			Debug.Log("You triggered event");
			randomNo = Random.Range(0, 4); //0 is normal route, 1 is shortcut, 2 is roadblock, 3 is tips
            if (randomNo == 0)
            {
                Status_PopUp.statusMg.NothingHappened();
            }
            if (randomNo == 1)
			{
				Debug.Log(randomNo.ToString() + "Shortut");
				Status_PopUp.statusMg.EnterShortcut();
				//progress bar stop moving
			}
			else if (randomNo == 2)
			{
				Debug.Log(randomNo.ToString() + "You triggered road block");
				Status_PopUp.statusMg.EnterRoadblock();
				LevelManager.LvMg.ProgressBar += (stepCount + 1) * 2;
			}
			else if (randomNo == 3)
			{
				Debug.Log(randomNo.ToString() + "You got some tipss");
				tipsGeneration();
				//generate tips
			}

		}
		if (other.tag == "Shortcut")
		{
			Destroy(other.gameObject);
			Debug.Log("Shortut Triggered");
			Status_PopUp.statusMg.EnterShortcut();
			LevelManager.LvMg.ProgressBar -= stepCount;
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

	public IEnumerator EnablePlayerMove()
	{
        canControl = false;
		yield return new WaitForSeconds(0.5f);
		canControl = true;
	}
}
