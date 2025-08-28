using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    public GameObject player;
    public Transform target;
    public float distance;
    public float moveSpeed;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        target = player.transform;
        
    }

    protected virtual void Pathfind()
    {
        Debug.Log("Pathfinding to " + target.name);
    }


}
