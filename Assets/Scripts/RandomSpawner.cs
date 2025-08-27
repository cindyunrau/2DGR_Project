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

            SpawnEnemy(spawnPointList[randSP], enemyList[randEnemy]);
        }

        spawnTimer -= Time.deltaTime;
    }

    private void SpawnEnemy(Transform spawnPoint, Enemy enemy)
    {
        Instantiate(enemy, spawnPoint);
        spawnTimer = frequency;
    }
}
