using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	
	private Vector3 start_pos;
	private Vector3 end_pos;
	public float movement;
	public float distance;
	private float inputFreeze=2f;
	private float inputTimer;
	private float range = 0.25f;

	// Use this for initialization
	void Start () {
		
	}

	private void Update()
	{
		start_pos = transform.position;
		//inputTimer += Time.deltaTime;
		//Debug.Log(inputTimer);

		//if (inputTimer < inputFreeze)
		//{
		//	Debug.Log("Cant move");
		//	return;
		//}

		
		if (Input.GetKey(KeyCode.W))
		{
			end_pos += new Vector3(0, range, 0);				
		}
		else if (Input.GetKey(KeyCode.S))
		{
			end_pos += new Vector3(0, -range, 0);				
		}
		else if (Input.GetKey(KeyCode.A))
		{
			end_pos += new Vector3(-range, 0, 0);				
		}
		else if (Input.GetKey(KeyCode.D))
		{
			end_pos += new Vector3(range, 0, 0);				
		}
			
		transform.position = Vector3.Lerp(start_pos, end_pos, 1*Time.deltaTime);
		
		
		
	}
		
	
	private void OnTriggerEnter(Collider other)
	{
		if(other.tag=="Destination")
		{

		}
		//if(other.tag=="")
	}
}
