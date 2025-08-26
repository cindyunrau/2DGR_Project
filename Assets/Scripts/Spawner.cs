using UnityEngine;
using System.Collections;
using static UnityEngine.EventSystems.EventTrigger;

public class Spawner : MonoBehaviour
{

    public float spawnRadius = 5f;
    public GameObject player;

    void Update()
    {
        foreach (Transform childTs in GetComponentInChildren<Transform>())
        {
            SpawnPoint child = childTs.GetComponent<SpawnPoint>();

            if (child.nextTimeToSpawn <= Time.time)
            {
                // Get a random position in a circle around the player
                //float randomAngle = Random.Range(0f, 360f);
                //float spawnX = player.transform.position.x + spawnRadius * Mathf.Cos(randomAngle * Mathf.Deg2Rad);
                //float spawnY = player.transform.position.y + spawnRadius * Mathf.Sin(randomAngle * Mathf.Deg2Rad);
                //child.transform.position = new Vector2 (spawnX, spawnY);

                // Spawn enemy at that location
                SpawnEntity(child);
            }
        }
    }
    void SpawnEntity(SpawnPoint spawnPoint)
    {
        Enemy entity = Instantiate(spawnPoint.GetNextEntity(), spawnPoint.transform.position, spawnPoint.transform.rotation).GetComponent<Enemy>();
    }
}