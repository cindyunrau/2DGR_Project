using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Room : MonoBehaviour
{
    public bool roomEntered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!roomEntered && collision.gameObject.tag == "Player")
        {
            foreach(Transform spawnPoint in gameObject.transform)
            {
                spawnPoint.gameObject.SetActive(true);
            }
        }
    }
}
