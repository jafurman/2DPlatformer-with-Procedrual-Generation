using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    public float minSpawnDelay = 1.0f;
    public float maxSpawnDelay = 3.0f;

    private void Start()
    {
        // Generate a random spawn delay within the given range
        float spawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);
        Invoke("SpawnObject", spawnDelay);
    }

    private void SpawnObject()
    {
        // Instantiate the objectToSpawn at the position of the spawner
        Instantiate(objectToSpawn, transform.position, Quaternion.identity);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 0.2f);
    }
}
