using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

	public Transform tilePrefab;
	public Transform obstraclePrefab;
	public Vector2 mapSize;

	[Range(0,1)]
	public float outlinePercent;

	[Range(0,1)]
	public float obstaclePercent;

	List<Coord> allTileCoords;
	Queue<Coord> shuffledTileCoords;

	public int seed = 10;
	Coord mapCentre;

	void Start () {
		GenerateMap ();

	}

	public void GenerateMap () {

		allTileCoords = new List<Coord> ();
		for (int x = 0; x < mapSize.x; x++) {
			for (int y = 0; y < mapSize.y; y++) {
				allTileCoords.Add(new Coord(x,y));
			}
		}

		shuffledTileCoords = new Queue<Coord> (Utility.ShuffleArray(allTileCoords.ToArray(), seed));
		mapCentre = new Coord ((int)mapSize.x / 2, (int)mapSize.y / 2);

		string holderName = "Generated Map";

		if (transform.Find (holderName)){
			DestroyImmediate (transform.Find (holderName).gameObject);
		}

		Transform mapHolder = new GameObject (holderName).transform;
		mapHolder.SetParent (transform);

		for (int x = 0; x < mapSize.x; x++) {
			for (int y = 0; y < mapSize.y; y++) {
				Vector3 tilePosition = CoordToPosition (x, y);
				Transform newTile = Instantiate (tilePrefab, tilePosition, Quaternion.Euler (Vector3.right * 90)) as Transform;
				newTile.localScale = Vector3.one * (1 - outlinePercent);
				newTile.SetParent (mapHolder);
			}
		}

		bool[,] obstacleMap = new bool[(int)mapSize.x, (int)mapSize.y];

		int obstracleCount = (int)(mapSize.x * mapSize.y * obstaclePercent);
		int currentObstracleCount = 0;

		for (int i = 0; i < obstracleCount; i++) {
			Coord randomCoord = GetRandomCoord ();
			obstacleMap [randomCoord.x, randomCoord.y] = true;
			currentObstracleCount += 1;

			if (randomCoord != mapCentre && MapIsFullyAccessible (obstacleMap, currentObstracleCount)) {

				Vector3 obstraclePosition = CoordToPosition (randomCoord.x, randomCoord.y);
				Transform newObstacle = Instantiate (obstraclePrefab, obstraclePosition + Vector3.up * .5f, Quaternion.identity) as Transform;
				newObstacle.SetParent (mapHolder);
			} else {
				obstacleMap [randomCoord.x, randomCoord.y] = false;
				currentObstracleCount -= 1;
			}
		}

	}

	bool MapIsFullyAccessible (bool [,] obstacleMap, int currentObstracleCount)
	{
		bool[,] mapFlags = new bool[obstacleMap.GetLength (0), obstacleMap.GetLength (1)];
		Queue<Coord> queue = new Queue<Coord> ();
		queue.Enqueue (mapCentre);
		mapFlags [mapCentre.x, mapCentre.y] = true;

		int accessibleTileCount = 1;

		while (queue.Count > 0) {
			Coord tile = queue.Dequeue ();

			for (int x = -1; x <= 1; x++) {
				for (int y = -1; y <= 1; y++) {
					int neighbourX = tile.x + x;
					int neighbourY = tile.y + y;
					if (x == 0 || y == 0) {
						if (neighbourX >= 0 && neighbourX < obstacleMap.GetLength (0) && 
								neighbourY >= 0 && neighbourY < obstacleMap.GetLength (1)) {
							if (!mapFlags [neighbourX, neighbourY] && !obstacleMap [neighbourX, neighbourY]) {
								mapFlags [neighbourX, neighbourY] = true;
								queue.Enqueue (new Coord (neighbourX, neighbourY));
								accessibleTileCount += 1;
							}
						}
					}
				}
			}
		}

		int targetAccessibleTileCount = (int)(mapSize.x * mapSize.y - currentObstracleCount);
		return targetAccessibleTileCount == accessibleTileCount;
	}

	Vector3 CoordToPosition (int x, int y) {

		return new Vector3 (-mapSize.x/2 + .5f + x, 0, -mapSize.y/2 + .5f + y);
	}


	public Coord GetRandomCoord () {

		Coord randomCoord = shuffledTileCoords.Dequeue ();
		shuffledTileCoords.Enqueue (randomCoord);
		return randomCoord;
	}

	public struct Coord {

		public int x;
		public int y;

		public Coord (int _x, int _y) {
			x = _x;
			y = _y;
		}

		public static bool operator == (Coord c1, Coord c2) {
			return c1.x == c2.x && c1.y == c2.y;

		}

		public static bool operator != (Coord c1, Coord c2) {
			return !(c1 == c2);

		}
	}

}
