using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dropperSpawner : MonoBehaviour
{
    public GameObject droppedItem;
    public GameObject[] positions;

    void Start()
    {
        // StartCoroutine(RandomDropWave(5));
    }

    public void RandomPositionDrop()
    {
        foreach (GameObject go in positions)
        {
            Vector2 pos = go.transform.position;
            int chanceToSpawn = Random.Range(0, 10);
            if (chanceToSpawn < 4)
            {
                Instantiate(droppedItem, pos, Quaternion.identity);
            }
        }
    }

    public void DropEveryOther()
    {
        int counter = 0;
        foreach (GameObject go in positions)
        {
            counter++;
            if (counter % 2 == 0)
            {
                if (counter == 12)
                {
                    continue;
                }
                else
                {
                    Vector2 pos = go.transform.position;
                    Instantiate(droppedItem, pos, Quaternion.identity);
                }
            }

        }
    }

    public void DropEveryThird()
    {
        int counter = 0;
        foreach (GameObject go in positions)
        {
            counter++;
            if (counter % 3 == 0)
            {
                Vector2 pos = go.transform.position;
                Instantiate(droppedItem, pos, Quaternion.identity);
            }

        }
    }

    public void DropRightHalf()
    {
        int counter = 0;
        foreach (GameObject go in positions)
        {
            counter++;
            if (counter >= 12)
            {
                Vector2 pos = go.transform.position;
                Instantiate(droppedItem, pos, Quaternion.identity);
            }

        }
    }

    public void DropLeftHalf()
    {
        int counter = 0;
        foreach (GameObject go in positions)
        {
            counter++;
            if (counter <= 11)
            {
                Vector2 pos = go.transform.position;
                Instantiate(droppedItem, pos, Quaternion.identity);
            }

        }
    }

    public IEnumerator RandomDropWave(int times)
    {
        int counter = 0;
        foreach (GameObject go in positions)
        {
            counter += 1;
            if (counter >= times)
            {
                break;
            }
            int random = Random.Range(1, 7);
            if (random == 1)
            {
                DropEveryThird();
                yield return new WaitForSeconds(5f);
            }
            else if (random == 2)
            {
                DropEveryOther();
                yield return new WaitForSeconds(5f);
            }
            else if (random == 3)
            {
                RandomPositionDrop();
                yield return new WaitForSeconds(5f);
            }
            else if (random == 4)
            {
                StartCoroutine(LeftWipeDrop());
                yield return new WaitForSeconds(6.2f);
            }
            else if (random == 5)
            {
                StartCoroutine(RightWipeDrop());
                yield return new WaitForSeconds(6.2f);
            }
            else if (random == 6)
            {
                DropLeftHalf();
                yield return new WaitForSeconds(5f);
            }
            else if (random == 7)
            {
                DropRightHalf();
                yield return new WaitForSeconds(5f);
            }

        }

    }

    public IEnumerator LeftWipeDrop()
    {
        int counter = 0;
        foreach (GameObject go in positions)
        {
            counter += 1;
            if (counter == 12 || counter == 11)
            {
                continue;
            }
            else
            {
                Vector2 pos = go.transform.position;
                yield return new WaitForSeconds(.2f);
                Instantiate(droppedItem, pos, Quaternion.identity);
            }

        }
    }

    public IEnumerator RightWipeDrop()
    {
        for (int i = positions.Length - 1; i >= 0; i--)
        {
            GameObject go = positions[i];
            int counter = positions.Length - i;

            if (counter == 12 || counter == 11)
            {
                continue;
            }
            else
            {
                Vector2 pos = go.transform.position;
                yield return new WaitForSeconds(.2f);
                Instantiate(droppedItem, pos, Quaternion.identity);
            }
        }
    }
}
