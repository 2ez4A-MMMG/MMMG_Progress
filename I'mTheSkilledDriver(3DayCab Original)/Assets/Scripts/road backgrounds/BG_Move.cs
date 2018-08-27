using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG_Move : MonoBehaviour {

    //public float bg_speed;
    public float x_offset;
    private GameObject switchboundary;
    public GameObject movingBG;

	// Use this for initialization
	void Start () {
        switchboundary = GameObject.FindWithTag("boundary");
	}
	
	// Update is called once per frame
	void Update () {
        Move(LevelManager.LvMg.roadMoveSpeed);
	}

    protected void Move(float speed)
    {
        Vector3 pos = transform.position;
        if (pos.x + x_offset/2 < switchboundary.transform.position.x)
        {
            //spawn new plane (and give new name coz why not XD)
            Vector3 newSpawnPos = new Vector3(pos.x + 2 * x_offset, pos.y, pos.z);
            GameObject newEnv = Instantiate(movingBG, newSpawnPos, transform.rotation);
            newEnv.name = this.gameObject.name;
            //destroy old plane(this one)
            Destroy(this.gameObject);
        }
        pos.x -= speed * Time.deltaTime;
        transform.position = pos;
    }
}
