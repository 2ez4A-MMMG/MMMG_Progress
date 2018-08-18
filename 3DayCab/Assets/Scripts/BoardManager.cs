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
	public GameObject playerHolder;

	public GameObject playerPrefab;
	public static BoardManager boardManagerInstance;

	public Vector3 playerInitialPosition;

	private void Awake()
	{
		if (boardManagerInstance==null)
		{
			boardManagerInstance = this;
		}
	}

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
			for (int y=0; y<rows;y++)
			{
				GameObject toInstantiate = roadTiles[UnityEngine.Random.Range(0, roadTiles.Length)]; //randomly select one of the sprite from the list
				toInstantiate.GetComponent<SpriteRenderer>().color = Color.black;
				if (x == -1 || x == columns)
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
			GameObject instance = Instantiate(spawnObject, randomPosition, Quaternion.identity) as GameObject;
			instance.GetComponent<SpriteRenderer>().color = Color.black;
			instance.transform.SetParent(boardHolder);
		}
	}

	public void SetupScene()
	{		
		StartCoroutine(SetupSceneCoroutine());
		SpawnPlayer();
	}

	public IEnumerator SetupSceneCoroutine()
	{				
		BoardSetup();				
		InitialiseList();				
		SpawnObject(destination, 1);		
		SpawnObject(shortcutTiles, 2);		
		SpawnObject(roadBlocksTiles, 2);		
		SpawnObject(extraTipsTiles, 2);		
		SpawnObject(randomEventsTiles, 1);		
		SpawnObject(speechBubbleTiles, 2);				
		Debug.Log("YOU CAN MOVE NOW");
		yield return null;
	}

	public void SpawnPlayer()
	{
		playerHolder = Instantiate(playerPrefab, new Vector3(0, 0, -0.0002f), Quaternion.identity) as GameObject;
	}

	public void ResetBoard()
	{		
		Player.playerInstance.blockX = 0;
		Player.playerInstance.blockY = 0;
		Player.playerInstance.GetComponent<SpriteRenderer>().flipX = false;
		Destroy(GameObject.Find("Board"));
		playerHolder.transform.position = new Vector3(0, 0, -0.0002f) ;		
		StartCoroutine(SetupSceneCoroutine());
		
	}



}
