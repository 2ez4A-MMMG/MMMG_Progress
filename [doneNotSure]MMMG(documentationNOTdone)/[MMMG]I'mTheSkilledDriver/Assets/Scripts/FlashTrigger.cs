using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashTrigger : MonoBehaviour {

	private void OnTriggerEnter(Collider other)
	{
        if (other.tag != "Border")
        other.GetComponent<SpriteRenderer>().color = Color.white;
        if (other.tag == "SurfaceCover")
            Destroy(other.gameObject);
    }
}
