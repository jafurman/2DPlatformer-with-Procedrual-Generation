using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RandomTilemap : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase[] tiles;

    public Vector2Int gridSize;
    public float cellSize = 1f;

    public Vector3Int previousStartPos;
    public Vector3Int startPos;
    public Vector3 newPos;

    public int minY, minX, maxY, maxX;

    public int percentageAgainstBackTracking = 1000;
    public int modulator;

    public int bias;

    private List<Vector3> vectorList = new List<Vector3>();

    public GameObject spawnPrefab;

    public GameObject player;
    public Vector3 playerSpawnPos;

    public GameObject levelEnder;

    void Start()
    {

        modulator = Mathf.RoundToInt((percentageAgainstBackTracking * 10) / 100);
        GenerateLevel();

        //startPosition is middle of the grid x * y
        startPos = new Vector3Int((gridSize.x / 2), (gridSize.y / 2));

        minX = 0;
        maxX = (int)gridSize.x;
        maxY = (int)gridSize.y;
        minX = 0;

        StartCoroutine(DestroyTilesFromMiddle());

        bias = Random.Range(0, 4);
    }

    void GenerateLevel()
    {
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                Vector3Int tilePosition = new Vector3Int(x, y, 0);
                int tileIndex = Random.Range(0, tiles.Length);
                tilemap.SetTile(tilePosition, tiles[tileIndex]);
            }
        }
    }

    public IEnumerator DestroyTilesFromMiddle()
    {
        while (startPos.x < maxX && startPos.y > 0 && startPos.y < maxY && startPos.x > 0)
        {
            if (startPos.x >= maxX - 2 || startPos.y <= 2 || startPos.y >= maxY - 2 || startPos.x <= 2)
            {
                break;
            }
            // add each tiles vector 3 that we go to to a list
            if (!vectorList.Contains(startPos))
            {
                vectorList.Add(startPos);
            }

            TileBase tile = tilemap.GetTile(startPos);

            // 50% chance to continue to find new position where tile isnt null or "backTrack"
            // You can increase percentage of lesser eating by making it easier to be continued.
            // Make percentage of backtracking
            if (tile == null)
            {
                //while not null go right to avoid burnout
                while (tile != null)
                {
                    Debug.Log(bias);
                    switch (bias)
                    {
                        case 0:
                            startPos = new Vector3Int(startPos.x += 1, startPos.y);
                            break;
                        case 1:
                            startPos = new Vector3Int(startPos.x -= 1, startPos.y);
                            break;
                        case 2:
                            startPos = new Vector3Int(startPos.x, startPos.y += 1);
                            break;
                        case 3:
                            startPos = new Vector3Int(startPos.x, startPos.y -= 1);
                            break;

                    }

                }
                int RandomNum = Random.Range(0, modulator);
                if (RandomNum != 1)
                {
                    continue;
                }
            }
            //original "random" algorithm that essentially just eats the tilemap
            tilemap.SetTile(startPos, null);
            int randomValue = Random.Range(1, 5);
            previousStartPos = startPos;
            switch (randomValue)
            {
                case 1:
                    startPos.x += 1;
                    startPos = new Vector3Int(startPos.x, startPos.y);
                    break;
                case 2:
                    startPos.y -= 1;
                    startPos = new Vector3Int(startPos.x, startPos.y);
                    break;
                case 3:
                    startPos.x -= 1;
                    startPos = new Vector3Int(startPos.x, startPos.y);
                    break;
                case 4:
                    startPos.y += 1;
                    startPos = new Vector3Int(startPos.x, startPos.y);
                    break;
            }

            yield return new WaitForSeconds(.01f);
        }

        Debug.Log("Finished");

        placePrefabs();


    }


    public void placePrefabs()
    {
        foreach (Vector3 vector in vectorList)
        {
            int randInt = Random.Range(1, 200);
            if (randInt <= 5)
            {

                GameObject prefab = Instantiate(spawnPrefab, vector, Quaternion.identity);
            }
        }

        //move the player to the start of the level
        playerSpawnPos = new Vector3Int((gridSize.x / 2), (gridSize.y / 2));
        player.transform.position = playerSpawnPos;

        Instantiate(levelEnder, startPos, Quaternion.identity);

    }
}
