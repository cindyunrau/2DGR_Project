using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawning : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject[] weaponPrefabs;
    public GameObject ammoPrefab;
    public GameObject fuelPrefab;

    private List<Transform> spawnPoints = new List<Transform>();
    private Transform startRoomSpawn;

    private void Start()
    {
        // Collect all objects tagged with ItemSpawnPoint
        foreach (Transform child in transform)
        {
            if (child.tag == "ItemSpawnPoint")
            {
                if (child.name == "StartItemLoc")
                {
                    startRoomSpawn = child;
                } else
                {
                    spawnPoints.Add(child);
                } 
            }
                
        }

        SpawnItems();
    }

    private void SpawnItems()
    {
        if (spawnPoints.Count == 0)
        {
            Debug.LogWarning("No spawn points found under " + gameObject.name);
            return;
        }

        List<Transform> availablePoints = new List<Transform>(spawnPoints);

        if (startRoomSpawn != null)
        {
            // Pick a weapon to put in the player's starting room
            int weaponIndex = Random.Range(0, weaponPrefabs.Length);
            Instantiate(weaponPrefabs[weaponIndex], startRoomSpawn.position, Quaternion.identity);
            availablePoints.Remove(startRoomSpawn);

            // Remove that weapon from the pool of spawnable weapons
            List<GameObject> remainingWeapons = new List<GameObject>(weaponPrefabs);
            remainingWeapons.RemoveAt(weaponIndex);

            // Place remaining weapons in random other spots
            foreach (GameObject weapon in remainingWeapons)
            {
                if (availablePoints.Count == 0) break;

                int randIndex = Random.Range(0, availablePoints.Count);
                Transform point = availablePoints[randIndex];

                Instantiate(weapon, point.position, Quaternion.identity);
                availablePoints.RemoveAt(randIndex);
            }
        }
        else
        {
            Debug.LogWarning("PlayerRoomSpawn not found! All weapons will spawn randomly.");
            for (int i = 0; i < weaponPrefabs.Length && availablePoints.Count > 0; i++)
            {
                int randIndex = Random.Range(0, availablePoints.Count);
                Transform point = availablePoints[randIndex];

                Instantiate(weaponPrefabs[i], point.position, Quaternion.identity);
                availablePoints.RemoveAt(randIndex);
            }
        }

        // Fill remaining with random weapons/fuel/ammo
        foreach (Transform point in availablePoints)
        {
            GameObject prefabToSpawn = PickRandomPrefab();
            Instantiate(prefabToSpawn, point.position, Quaternion.identity);
        }
    }

    private GameObject PickRandomPrefab()
    {
        // Example weights: 60% ammo, 40% fuel
        float roll = Random.value;
        if (roll <= 0.6f)
        {
            return ammoPrefab;
        }
        else
        {
            return fuelPrefab;
        }
            
    }
}
