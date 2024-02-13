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

    private List<Vector3Int> vectorList = new List<Vector3Int>();

    public GameObject spawnPrefab;

    public GameObject player;
    public Vector3Int playerSpawnPos;

    public TilemapCollider2D TMcollider;

    public static bool started;

    public GameObject spawnEffect;
    public GameObject magePrefab;
    public GameObject skellyHandsPrefab;

    public List<Vector3Int> tilesOnField = new List<Vector3Int>();

    public List<Vector3Int> topSpaces = new List<Vector3Int>();

    public GameObject levelStartPrefab;
    public GameObject leverEnderPrefab;

    // bru there is no way you're almost graduated and writing code like this
    public Vector3Int[] topLeft, topMid, topRight, midLeft, midRight, botLeft, botMid, botRight;


    void Start()
    {

        started = false;

        //Make Tilemap Collider false so player doesnt hit it when spectating
        TMcollider = tilemap.GetComponent<TilemapCollider2D>();
        TMcollider.enabled = false;

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
                tilemap.SetTile(tilePosition, tiles[4]);

                tilesOnField.Add(tilePosition);
            }
        }
    }

    public IEnumerator DestroyTilesFromMiddle()
    {
        yield return new WaitForSeconds(3f);
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

        }

        Debug.Log("This iteration took " + vectorList.Count + " tiles.");

        Debug.Log("Finished");

        placePrefabs();


    }


    public void placePrefabs()
    {
        //move the player to the start of the level and enable collider
        playerSpawnPos = new Vector3Int((gridSize.x / 2), gridSize.y / 2);

        cleanUpMap(true);
        cleanUpMap(true);
        cleanUpMap(true);
        SetPlayerSpawnArea();
        SetEndArea();
        cleanUpMap(true);
        cleanUpMap(false);
        spawnTopSpaces();



        foreach (Vector3 vector in vectorList)
        {
            int randInt = Random.Range(1, 200);
            if (randInt <= 5)
            {

                GameObject prefab = Instantiate(spawnPrefab, vector, Quaternion.identity);
            }
        }


        StartCoroutine(spawnPlayer());

        TMcollider.enabled = true;

        started = true;
        

    }


    public void cleanUpMap(bool EraseTiles)
    {
        foreach (Vector3Int vector in tilesOnField)
        {
            TileBase testTile = tilemap.GetTile(vector);
            if (testTile == null)
            {
                continue;
            }
            // Get surrounding values
            Vector3Int tl = new Vector3Int(vector.x - 1, vector.y + 1);
            Vector3Int tm = new Vector3Int(vector.x, vector.y + 1);
            Vector3Int tr = new Vector3Int(vector.x + 1, vector.y + 1);
            Vector3Int ml = new Vector3Int(vector.x - 1, vector.y);
            Vector3Int mr = new Vector3Int(vector.x + 1, vector.y);
            Vector3Int bl = new Vector3Int(vector.x - 1, vector.y - 1);
            Vector3Int bm = new Vector3Int(vector.x, vector.y - 1);
            Vector3Int br = new Vector3Int(vector.x + 1, vector.y - 1);

            TileBase topLeft = tilemap.GetTile(tl);
            TileBase topMid = tilemap.GetTile(tm);
            TileBase topRight = tilemap.GetTile(tr);
            TileBase midLeft = tilemap.GetTile(ml);
            TileBase midRight = tilemap.GetTile(mr);
            TileBase botLeft = tilemap.GetTile(bl);
            TileBase botMid = tilemap.GetTile(bm);
            TileBase botRight = tilemap.GetTile(br);


            if (EraseTiles)
            {
                // all null spaces
                if ((topLeft == null && topMid == null && topRight == null
                    && midLeft == null && midRight == null
                    && botLeft == null && botMid == null && botRight == null)
                    || (topLeft != null && topMid == null && topRight == null
                    && midLeft == null && midRight == null
                    && botLeft == null && botMid == null && botRight == null)
                    || (topLeft == null && topMid == null && topRight != null
                    && midLeft == null && midRight == null
                    && botLeft == null && botMid == null && botRight == null)
                    || (topLeft == null && topMid == null && topRight == null
                    && midLeft == null && midRight == null
                    && botLeft != null && botMid == null && botRight == null)
                    || (topLeft == null && topMid == null && topRight == null
                    && midLeft == null && midRight == null
                    && botLeft == null && botMid == null && botRight != null)
                    || midLeft == null && midRight == null
                    || topMid == null && botMid == null
                    || (topLeft != null && topMid != null && topRight != null
                    && midLeft == null && midRight == null
                    && botLeft == null && botMid == null && botRight == null)
                    || (topLeft == null && topMid == null && topRight == null
                    && midLeft == null && midRight == null
                    && botLeft != null && botMid != null && botRight != null)
                    || (topLeft == null && topMid == null && topRight == null
                    && midLeft == null && midRight == null && botRight == null)
                    || (topLeft == null && midRight == null && botRight == null && botMid == null)
                    || (topLeft == null && topMid == null && botRight == null && midLeft == null)

                    // uniques

                    || (topLeft != null && topMid != null && topRight == null
                    && midLeft == null && midRight != null
                    && botLeft == null && botMid != null && botRight != null)

                    || (topLeft == null && topMid == null && topRight != null
                    && midLeft != null && midRight != null
                    && botLeft != null && botMid != null && botRight == null)

                    || (topLeft == null && topMid != null && topRight != null
                    && midLeft != null && midRight != null
                    && botLeft != null && botMid == null && botRight == null)

                    || (topLeft != null && topMid == null && topRight == null
                    && midLeft == null && midRight != null
                    && botLeft == null && botMid != null && botRight != null)

                    || (topLeft == null && topMid != null && topRight == null
                    && midLeft == null && midRight != null
                    && botLeft != null && botMid != null && botRight != null)

                    || (topLeft != null && topMid == null && topRight == null
                    && midLeft == null && midRight != null
                    && botLeft != null && botMid != null && botRight != null)

                    || (topLeft == null && topMid == null && topRight != null
                    && midLeft != null && midRight == null
                    && botLeft != null && botMid != null && botRight != null)

                    || (topLeft == null && topMid != null && topRight != null
                    && midLeft != null && midRight == null
                    && botLeft != null && botMid != null && botRight == null)

                    || (topLeft != null && topMid != null && topRight == null
                    && midLeft != null && midRight != null
                    && botLeft == null && botMid == null && botRight != null)

                    || (topLeft == null && topMid != null && topRight != null
                    && midLeft == null && midRight != null
                    && botLeft != null && botMid != null && botRight == null)

                    || (topLeft != null && topMid != null && topRight == null
                    && midLeft == null && midRight != null
                    && botLeft == null && botMid == null && botRight != null)

                    || (topLeft == null && topMid != null && topRight != null
                    && midLeft == null && midRight != null
                    && botLeft != null && botMid == null && botRight == null)

                    || (topLeft != null && topMid != null && topRight == null
                    && midLeft == null && midRight != null
                    && botLeft != null && botMid != null && botRight != null)

                    || (topLeft != null && topMid != null && topRight == null
                    && midLeft == null && midRight == null
                    && botLeft == null && botMid == null && botRight == null)

                    || (topLeft != null && topMid == null && topRight == null
                    && midLeft != null && midRight != null
                    && botLeft != null && botMid != null && botRight == null)

                    || (topLeft == null && topMid == null && topRight == null
                    && midLeft == null && midRight != null
                    && botLeft == null && botMid == null && botRight == null))
                {
                    tilemap.SetTile(vector, null);
                }

            }

            //all topLeft spaces
            else if ( midRight != null && botMid != null && botRight != null &&
                topLeft == null && topMid == null && midLeft == null )
            {
                tilemap.SetTile(vector, tiles[0]);
            }

            //all bottomRight spaces
            else if (midRight == null && botMid == null && botRight == null &&
                topLeft != null && topMid != null && midLeft != null)
            {
                tilemap.SetTile(vector, tiles[8]);
            }

            //all topRight spaces
            else if (midLeft != null && botLeft != null && botMid != null &&
                topMid == null && topRight == null && midRight == null)
            {
                tilemap.SetTile(vector, tiles[2]);
            }

            //all botLeft spaces
            else if (midLeft == null && botLeft == null && botMid == null &&
                topMid != null && topRight != null && midRight != null)
            {
                tilemap.SetTile(vector, tiles[6]);
            }

            //all left edge spaces
            else if (midLeft == null && topMid != null & botMid != null
                && topRight != null && midRight != null && botRight != null)
            {
                tilemap.SetTile(vector, tiles[3]);
            }

            //all right edge spaces
            else if (midLeft != null && topMid != null & botMid != null
                && topLeft != null && midRight == null && botLeft != null )
            {
                tilemap.SetTile(vector, tiles[5]);
            }

            //all top edge spaces
            else if (topMid == null && midLeft != null && midRight != null
                && botLeft != null && botMid != null && botRight != null)
            {
                tilemap.SetTile(vector, tiles[1]);
                topSpaces.Add(vector);
            }

            //all bottom edge spaces
            else if (botMid == null && midLeft != null && midRight != null
                && topLeft != null && topMid != null && topRight != null)
            {
                tilemap.SetTile(vector, tiles[7]);
            }

            //all voidWall spaces
            else if (topLeft != null && topMid != null && topRight != null
                && midLeft != null && midRight != null
                && botLeft != null && botMid != null && botRight != null)
            {
                tilemap.SetTile(vector, tiles[4]);
            }




        }
        tilemap.RefreshAllTiles();
    }

    public void spawnTopSpaces()
    {
        foreach (Vector3Int vector in topSpaces)
        {
            int chance = Random.Range(0, 23);

            if (chance == 15)
            {
                Vector3 mageSpawnPos = tilemap.transform.position;
                mageSpawnPos.x = vector.x;
                mageSpawnPos.y = vector.y;
                mageSpawnPos.x += 1.5f;
                mageSpawnPos.y += 2.5f;
                mageSpawnPos.z += 2.5f;
                GameObject mage = Instantiate(magePrefab, mageSpawnPos, Quaternion.identity);
                Debug.Log("Dropping mage on " + newPos);
            }

            if (chance == 17 || chance == 6)
            {
                Vector3 newPos = vector;
                newPos.y += 1.4f;
                GameObject skellyHands = Instantiate(skellyHandsPrefab, newPos, Quaternion.identity);
                Debug.Log("Dropping skelly hands on " + newPos);
            }
        }
    }

    public IEnumerator spawnPlayer()
    {
        Vector3 playerBeginsHere = playerSpawnPos;
        playerBeginsHere.x += 3;
        playerBeginsHere.y += 3;
        player.transform.position = playerBeginsHere;
        GameManager.playerStart = playerBeginsHere;
        player.SetActive(false);
        if (spawnEffect != null)
        {
            yield return new WaitForSeconds(2f);
            GameObject effectInstance = Instantiate(spawnEffect, player.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(1.5f);
            Destroy(effectInstance);
            player.SetActive(true);
        }

        //code to tell which kind of level is generated
        int tilesTravelled = vectorList.Count;
        if (tilesTravelled <= 500 )
        {
            Debug.Log("This level is rated easy");
        } else if (500 < tilesTravelled && tilesTravelled <= 900)
        {
            Debug.Log("This level is rated medium");
        } else if (900 < tilesTravelled)
        {
            Debug.Log("This level is rated hard");
        }
    }


    public void SetPlayerSpawnArea()
    {
        int squareSize = 6;
        Vector3Int startTilePosition = playerSpawnPos;

        // Iterate through the square area and set the tiles to null
        for (int x = 0; x < squareSize; x++)
        {
            for (int y = 0; y < squareSize; y++)
            {
                Vector3Int currentTilePos = startTilePosition + new Vector3Int(x, y, 0);

                if (x == 2 && y == 1)
                {
                    Vector3 prefabSpawnLocation = new Vector3(startTilePosition.x + x + 1f,startTilePosition.y + y + 1.5f, startTilePosition.z + 1.4f);
                    GameObject leverEnder = Instantiate(levelStartPrefab, prefabSpawnLocation, Quaternion.identity);
                    tilemap.SetTile(currentTilePos, tiles[0]);
                }
                else if (x == 3 && y == 1)
                {
                    tilemap.SetTile(currentTilePos, tiles[2]);
                }
                else if (x == 3 && y == 0)
                {
                    tilemap.SetTile(currentTilePos, tiles[8]);
                }
                else if (x == 2 && y == 0)
                {
                    tilemap.SetTile(currentTilePos, tiles[6]);
                }
                else
                {
                    tilemap.SetTile(currentTilePos, null);
                    vectorList.Remove(currentTilePos);
                }
            }
        }

        tilemap.SetTile(playerSpawnPos, null);

    }

    public void SetEndArea()
    {
        int squareSize = 8;
        Vector3Int lastVector = vectorList[vectorList.Count - 9];
        Vector3Int EndTilePosition = lastVector;

        for (int x = -1; x < squareSize + 1; x++)
        {
            for (int y = -1; y < squareSize + 1; y++)
            {
                Vector3Int currentTilePos = EndTilePosition + new Vector3Int(x, y, 0);


                if (x == -1 || x == squareSize || y == -1 || y == squareSize ||
                    x == 0 || x == squareSize -1 || y == 0 || y == squareSize -1)
                {
                    if (!tilesOnField.Contains(currentTilePos))
                    {
                        tilemap.SetTile(currentTilePos, tiles[4]);
                        tilesOnField.Add(currentTilePos);
                        
                    }
                }
                else if (x == 3 && y == 2)
                {
                    Vector3 prefabSpawnLocation = new Vector3(EndTilePosition.x + x + 1, EndTilePosition.y + y + 1.5f, EndTilePosition.z + 1.4f);
                    GameObject leverEnder = Instantiate(leverEnderPrefab, prefabSpawnLocation, Quaternion.identity);
                    tilemap.SetTile(currentTilePos, tiles[0]);
                }
                else if (x == 4 && y == 2)
                {
                    tilemap.SetTile(currentTilePos, tiles[2]);
                }
                else if (x == 4 && y == 1)
                {
                    tilemap.SetTile(currentTilePos, tiles[8]);
                }
                else if (x == 3 && y == 1)
                {
                    tilemap.SetTile(currentTilePos, tiles[6]);
                }
                else
                {
                    tilemap.SetTile(currentTilePos, null);
                    vectorList.Remove(currentTilePos);
                }
            }
        }


        tilemap.SetTile(playerSpawnPos, null);
    }
}

