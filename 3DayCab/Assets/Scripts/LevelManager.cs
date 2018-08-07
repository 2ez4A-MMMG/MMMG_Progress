using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    //initiate variables needed for outside variables - in PUBLIC form
    //(mostly for those related to button inputs)

    public static LevelManager LvMg;
    public bool canMove;

	// Use this for initialization
	void Awake () {
        LvMg = this;
        canMove = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
