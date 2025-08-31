using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{

    [Header("References")]
    public GameObject player;
    public GameObject fireSpirit;
    public Transform[] spawnPointList;
    public Enemy[] enemyList;
    public IntegerValue enemyCount;

    [Header("Spawner Variables")]
    public float spawnTimer;
    public float frequency = 2f;
    public float minFreq = 0.2f;
    public int enemyCap = 10;

    private void FixedUpdate()
    {
        if (enemyCount.value < enemyCap)
        {
            if (!player.GetComponent<Player>().isDead())
            {
                if (spawnTimer <= 0)
                {
                    //Debug.Log(spawnPointList.Length + " " + enemyList.Length);

                    int randSP = Random.Range(0, spawnPointList.Length);
                    int randEnemy = Random.Range(0, enemyList.Length);

                    // Check if selected spawnPoint is outside of player's vision and active
                    if (Vector2.Distance(spawnPointList[randSP].position, player.transform.position)
                                            > player.GetComponent<Player>().spotlight.innerRange + 3
                                            && spawnPointList[randSP].gameObject.activeSelf)
                    {
                        SpawnEnemy(spawnPointList[randSP], enemyList[randEnemy]);
                    }
                }
                spawnTimer -= Time.deltaTime;
            }
        }
    }

    private void SpawnEnemy(Transform spawnPoint, Enemy enemy)
    {
        Instantiate(enemy, spawnPoint);
        spawnTimer = frequency;
        enemyCount.value++;
    }
}
