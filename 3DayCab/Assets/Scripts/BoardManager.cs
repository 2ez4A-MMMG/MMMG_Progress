using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BoardManager : MonoBehaviour {	

	public int columns = 4,rows=4;
	
	public GameObject[] roadTiles;
	public GameObject[] outerWallTiles;	

	public GameObject destination;	
	public GameObject roadBlocksTiles;
	public GameObject extraTipsTiles;
	public GameObject randomEventsTiles;
	public GameObject shortcutTiles;
	public GameObject speechBubbleTiles;

	private Transform boardHolder; //collapse everything in here
	private List<Vector3> gridPositions = new List<Vector3>();

	public GameObject playerPrefab;

	void InitialiseList()
	{
		gridPositions.Clear();

		for(int x=0;x<columns;x++)
		{			
			for (int y=0;y<rows;y++)
			{				
				gridPositions.Add(new Vector3(x, y, 0f)); //creating possible positions to place object				
			}
		}
	}

	void BoardSetup()
	{
		boardHolder = new GameObject("Board").transform;

		//create a boarder surrounding all tiles
		for (int x = -1; x < columns+1 ; x++)
		{
			for (int y=-1; y<rows+1;y++)
			{
				GameObject toInstantiate = roadTiles[UnityEngine.Random.Range(0, roadTiles.Length)]; //randomly select one of the sprite from the list
				if(x==0&&y==0)
				{
					Instantiate(playerPrefab, new Vector3(x, y, -0.05f), Quaternion.identity); 
				}
				if (x == -1 || x == columns || y == -1 || y == rows)
				{
					toInstantiate = outerWallTiles[UnityEngine.Random.Range(0, outerWallTiles.Length)];
				}

				GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;

				instance.transform.SetParent(boardHolder);
			}
		}
	}

	//Calculate a random position for spawning special tiles
	Vector3 RandomPosition()
	{		
		int randomIndex = UnityEngine.Random.Range(1, gridPositions.Count);
		Vector3 randomPosition = gridPositions[randomIndex];
		gridPositions.RemoveAt(randomIndex); //remove the position which has been selected
		return randomPosition;
	}	

	void SpawnObject(GameObject spawnObject, int spawnCount) //to spawn object which is always one in scene
	{
		for (int i = 0; i < spawnCount; i++)
		{
			Vector3 randomPosition = RandomPosition();
			randomPosition.z = -0.0001f; //make it slightly in front of the road tile
			Instantiate(spawnObject, randomPosition, Quaternion.identity);
		}
	}

	public void SetupScene(int level)
	{
		BoardSetup();
		InitialiseList();
		SpawnObject(destination, 1);
		SpawnObject(shortcutTiles, 2);
		SpawnObject(roadBlocksTiles, 2);
		SpawnObject(extraTipsTiles, 2);
		SpawnObject(randomEventsTiles, 1);
		SpawnObject(speechBubbleTiles, 2);
	}

}
