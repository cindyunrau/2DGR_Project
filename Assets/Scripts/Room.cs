using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Room : MonoBehaviour
{
    public bool roomEntered = false;
    public RandomSpawner spawner;
    public Enemy enemy;
    public IntegerValue enemyCount;

    private void Start()
    {
        spawner = FindAnyObjectByType<RandomSpawner>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!roomEntered && collision.gameObject.tag == "Player")
        {
            foreach(Transform spawnPoint in gameObject.transform)
            {
                spawnPoint.gameObject.SetActive(true);
                Instantiate(enemy, spawnPoint);
                enemyCount.value++;
                if (spawner.frequency > spawner.minFreq) 
                {
                    spawner.frequency -= 0.05f;
                }
                
            }

            roomEntered = true;
        }
    }
}
