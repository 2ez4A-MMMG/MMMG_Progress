using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BoardManager : MonoBehaviour {

	//[Serializable]

	////public class Count
	////{
	////	public int minimum, maximum;
		
	////	public Count (int min,int max)
	////	{
	////		minimum = min;
	////		maximum = max; 
	////	}
	////}

	public int columns = 4,rows=4;

	//TBC for each maximum and minimum value
	//public Count roadBlocksCount = new Count(1, 2);
	//public Count extraTipsCount = new Count(1, 2);
	//public Count randomEventsCount = new Count(1, 2);
	//public Count shortcutCount = new Count(1, 2);
	//public Count destinationCount = new Count(0, 1);

	//TBC
	//Some of them don't really need an array to store, using array just because the function is using Array format as an input
	public GameObject[] roadTiles;
	public GameObject[] outerWallTiles;
	//public GameObject[] roadBlocksTiles;
	//public GameObject[] extraTipsTiles;
	//public GameObject[] randomEventsTiles;
	//public GameObject[] shortcutTiles;

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
		for (int x=-1;x<columns+1;x++)
		{
			for(int y=-1; y<rows+1;y++)
			{
				GameObject toInstantiate = roadTiles[UnityEngine.Random.Range(0, roadTiles.Length)]; //randomly select one of the sprite from the list
				if(x==0&&y==0)
				{
					Instantiate(playerPrefab, new Vector3(x, y, -0.05f), Quaternion.identity); 
				}
				if(x==-1||x==columns||y==-1||y==rows)
				{
					toInstantiate = outerWallTiles[UnityEngine.Random.Range(0, outerWallTiles.Length)];
				}

				GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.Euler(-90,0,0)) as GameObject;

				instance.transform.SetParent(boardHolder);
			}
		}
	}

	//Calculate a random position for spawning special tiles
	Vector3 RandomPosition()
	{
		int randomIndex = UnityEngine.Random.Range(0, gridPositions.Count);
		Vector3 randomPosition = gridPositions[randomIndex];
		gridPositions.RemoveAt(randomIndex); //remove the position which has been selected
		return randomPosition;
	}

	//Spawn the special tile by getting the random position 
	//void LayoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximum) //to spawn multiple same objects in scene
	//{
	//	int objectCount = UnityEngine.Random.Range(minimum, maximum + 1);

	//	for(int i=0;i<objectCount;i++)
	//	{
	//		Vector3 randomPosition = RandomPosition(); //calling the function to get the random position
	//		GameObject tileChoice = tileArray[UnityEngine.Random.Range(0, tileArray.Length)];
	//		Instantiate(tileChoice, randomPosition, Quaternion.Euler(-90, 0, 0));
	//	}
	//}

	void SpawnObject(GameObject spawnObject, int spawnCount) //to spawn object which is always one in scene
	{
		for (int i = 0; i < spawnCount; i++)
		{
			Vector3 randomPosition = RandomPosition();
			Instantiate(spawnObject, randomPosition, Quaternion.Euler(-90, 0, 0));
		}
	}

	public void SetupScene(int level)
	{
		BoardSetup();
		InitialiseList();		
		SpawnObject(destination,1);
		SpawnObject(shortcutTiles, 2);
		SpawnObject(roadBlocksTiles, 2);
		SpawnObject(extraTipsTiles, 2);
		SpawnObject(randomEventsTiles, 1);
		SpawnObject(speechBubbleTiles, 2);
	}

}
