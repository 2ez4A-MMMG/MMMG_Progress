using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BoardManager : MonoBehaviour {

	[Serializable]

	public class Count
	{
		public int minimum, maximum;
		
		public Count (int min,int max)
		{
			minimum = min;
			maximum = max; 
		}
	}

	public int columns = 4,rows=4;

	//TBC
	public Count roadBlocksCount = new Count(1, 3);
	public Count extraTipsCount = new Count(1, 3);
	public Count randomEventsCount = new Count(1, 3);
	public Count shortcutCount = new Count(1, 3);

	//TBC
	public GameObject destination;
	public GameObject[] roadTiles;
	public GameObject[] outerWallTiles;

	private Transform boardHolder; //collapse everything in here
	private List<Vector3> gridPositions = new List<Vector3>();

	void InitialiseList()
	{
		gridPositions.Clear();

		for(int x=1;x<columns-1;x++)
		{
			for (int y=1;y<rows-1;y++)
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
				GameObject toInstantiate = roadTiles[UnityEngine.Random.Range(0, roadTiles.Length)];
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
		gridPositions.RemoveAt(randomIndex);
		return randomPosition;
	}

	//Spawn the special tile by getting the random position 
	void LayoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximum)
	{
		int objectCount = UnityEngine.Random.Range(minimum, maximum + 1);

		for(int i=0;i<objectCount;i++)
		{
			Vector3 randomPosition = RandomPosition(); //calling the function to get the random position
			GameObject tileChoice = tileArray[UnityEngine.Random.Range(0, tileArray.Length)];
			Instantiate(tileChoice, randomPosition, Quaternion.Euler(-90, 0, 0));
		}
	}

	public void SetupScene(int level)
	{
		BoardSetup();
		InitialiseList();
		//LayoutObjectAtRandom(,)
		Instantiate(destination, new Vector3(columns - 1, rows - 1, 0F), Quaternion.Euler(-90, 0, 0));
	}
	
}
