using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetResolution : MonoBehaviour {

    public int width = 360;
    public int height = 640;
    public bool FullScreen = false;

	// Use this for initialization
	void Awake () {
        Screen.SetResolution(width, height, FullScreen);
    }
	
}
