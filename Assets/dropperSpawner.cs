using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dropperSpawner : MonoBehaviour
{
    public GameObject droppedItem;
    public GameObject[] positions;
    // Start is called before the first frame update
    void Start()
    {
        RandomPositionDrop();
    }

    public void RandomPositionDrop()
    {
        // randomly choose 10 to drop from above
    }

    public void DropEveryOther()
    {
        int counter = 0;
        foreach (GameObject go in positions)
        {
            counter++;
            if (counter % 2 == 0)
            {
                Vector2 pos = go.transform.position;
                Instantiate(droppedItem, pos, Quaternion.identity);
            }

        }
    }



}
