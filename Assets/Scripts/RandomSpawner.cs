using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{

    public GameObject player;
    public Transform[] spawnPointList;
    public Enemy[] enemyList;
    public float spawnTimer;
    public float frequency = 2f;

    private void FixedUpdate()
    {
        if(spawnTimer <= 0)
        {
            //Debug.Log(spawnPointList.Length + " " + enemyList.Length);

            int randSP = Random.Range(0, spawnPointList.Length);
            int randEnemy = Random.Range(0, enemyList.Length);

            // Check if selected spawnPoint is outside of player's vision and active
            if (Vector2.Distance(spawnPointList[randSP].position, player.transform.position) 
                                    > player.GetComponent<Player>().spotlight.innerRange + 2.5
                                    && spawnPointList[randSP].gameObject.activeSelf)
            {
                SpawnEnemy(spawnPointList[randSP], enemyList[randEnemy]);
            }
        }

        spawnTimer -= Time.deltaTime;
    }

    private void SpawnEnemy(Transform spawnPoint, Enemy enemy)
    {
        Instantiate(enemy, spawnPoint);
        spawnTimer = frequency;
    }
}
