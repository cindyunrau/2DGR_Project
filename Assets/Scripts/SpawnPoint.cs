using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class SpawnPoint : MonoBehaviour
{
    // List of values dictating the -space- between spawning elements, repeats
    public float[] distancePattern = new float[] { 1f, 1f, 1f };
    // List of Entities to spawn, in order, repeats
    public Enemy[] entityPattern;

    // Init value is offset
    public float nextTimeToSpawn = 0f;

    private int distanceIndex = 0;
    private int entityIndex = 0;


    public Enemy GetNextEntity()
    {
        if (distanceIndex >= distancePattern.Length)
        {
            distanceIndex = 0;
        }
        if (entityIndex >= entityPattern.Length)
        {
            entityIndex = 0;
        }

        Enemy nextEntity = entityPattern[entityIndex];

        nextTimeToSpawn = Time.time + distancePattern[distanceIndex];

        distanceIndex++;
        entityIndex++;

        return nextEntity;
    }

}