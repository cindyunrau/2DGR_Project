using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemSpawning : MonoBehaviour
{
    [Header("Prefabs")]
    [Tooltip("Weapons MUST be ordered sword, spear, pistol, shotgun.")]
    public GameObject[] weaponPrefabs;
    public GameObject ammoPrefab;
    public GameObject fuelPrefab;

    private Dictionary<WeaponType, GameObject> weaponMap;
    private List<Transform> spawnPoints = new List<Transform>();
    private Transform startRoomSpawn;

    private void Start()
    {
        // Build a lookup table for weapon types -> prefab
        weaponMap = weaponPrefabs
            .Select(prefab => prefab.GetComponent<WeaponIdentifier>())
            .Where(id => id != null)
            .ToDictionary(id => id.weaponType, id => id.gameObject);

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
            WeaponType startType = PickStartingWeaponType();
            Instantiate(weaponMap[startType], startRoomSpawn.position, Quaternion.identity);
            availablePoints.Remove(startRoomSpawn);

            // Spawn the rest of the weapons randomly
            foreach (var kv in weaponMap.Where(kv => kv.Key != startType))
            {
                if (availablePoints.Count == 0) { break; }

                int randIndex = Random.Range(0, availablePoints.Count);
                Transform point = availablePoints[randIndex];

                Instantiate(kv.Value, point.position, Quaternion.identity);
                availablePoints.RemoveAt(randIndex);
            }
        }
        else
        {
            Debug.LogWarning("StartItemLoc not found! All weapons will spawn randomly.");
            foreach (var weapon in weaponMap.Values)
            {
                if (availablePoints.Count == 0) { break; }

                int randIndex = Random.Range(0, availablePoints.Count);
                Transform point = availablePoints[randIndex];

                Instantiate(weapon, point.position, Quaternion.identity);
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
        return Random.value <= 0.7f ? ammoPrefab : fuelPrefab;
    }

    private WeaponType PickStartingWeaponType()
    {
        float roll = Random.value;
        if (roll <= 0.1f)
        {
            return WeaponType.Pistol;  // 10%
        }
        else if (roll <= 0.5f)
        {
            return WeaponType.Spear;   // 40%
        }  
        else
        {
            return WeaponType.Sword;   // 50%
        }
            
    }
}
