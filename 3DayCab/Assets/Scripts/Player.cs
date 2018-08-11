using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	
	private Vector2 end_pos;

	// Use this for initialization
	void Start () {
		
	}

	private void Update()
	{
		if (Input.GetKeyDown("a"))
			end_pos += Vector2.left;
		if (Input.GetKeyDown("d"))
			end_pos += Vector2.right;
		if (Input.GetKeyDown("w"))
			end_pos += Vector2.up;
		if (Input.GetKeyDown("s"))
			end_pos += Vector2.down;
		transform.position = Vector2.Lerp(transform.position, end_pos, 1f * Time.deltaTime);
	}
		
	
	
}
