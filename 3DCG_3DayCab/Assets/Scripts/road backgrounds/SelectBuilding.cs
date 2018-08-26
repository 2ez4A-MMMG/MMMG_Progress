using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectBuilding : MonoBehaviour {

    public List<GameObject> buildingList;
    private int buildingIndex;

	// Use this for initialization
	void Start () {
        //remove all unnecessary buildings first - if there's any
        foreach (Transform child in transform)
            Destroy(child.gameObject);

        //then spawn new one
        buildingIndex = Random.Range(0,buildingList.Count);
        GameObject newBuilding = Instantiate(buildingList[buildingIndex], transform.position, transform.rotation);
        newBuilding.transform.parent = this.transform;
	}
}
