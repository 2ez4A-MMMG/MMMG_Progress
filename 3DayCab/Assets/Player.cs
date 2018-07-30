using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	private int movingSpeed=1;
	private Vector3 pos;

	public int paidMoney;
	public int totalEarnings=0;
	public int tips;

	private int driveProgress;
	private int moveCount; //to update the driveProgress

	private bool deathTrigger;
	private bool canMove;

	void AttemptMove <T> (int xDir,int yDir)
	{
		
	}

	// Use this for initialization
	void Start () {
		
	}

	private void Update()
	{		
		if (Input.GetKey(KeyCode.W))
			pos += Vector3.up;
		else if (Input.GetKey(KeyCode.S))
			pos += Vector3.down;
		else if (Input.GetKey(KeyCode.A))
			pos += Vector3.left;
		else if (Input.GetKey(KeyCode.D))
			pos += Vector3.right;		

		float distance = 0.1f;

		while (distance > 0)
		{
			transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * movingSpeed);
			distance -= Time.deltaTime;

		}

		
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag=="Destination")
		{

		}
		//if(other.tag=="")
	}
}
