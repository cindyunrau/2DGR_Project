using UnityEngine;
using System.Collections;
using static UnityEngine.EventSystems.EventTrigger;

public class Spawner : MonoBehaviour
{
    void Update()
    {
        foreach (Transform childTs in GetComponentInChildren<Transform>())
        {
            SpawnPoint child = childTs.GetComponent<SpawnPoint>();

            if (child.nextTimeToSpawn <= Time.time)
            {
                SpawnEntity(child);
            }
        }
    }
    void SpawnEntity(SpawnPoint spawnPoint)
    {
        Enemy entity = Instantiate(spawnPoint.GetNextEntity(), spawnPoint.transform.position, spawnPoint.transform.rotation).GetComponent<Enemy>();
    }
}