using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FireSpirit : MonoBehaviour
{

    public float interactRadius = 2f;
    public float moveSpeed = 0.5f;

    [Header("References")]
    public Transform player;
    public BooleanValue exitDiscovered; 
    public GameObject exitBarrier;

    private NavMeshAgent agent;
    private Transform target;


    public void Start()
    {
        target = exitBarrier.transform;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = moveSpeed;
    }

    void Update()
    {
        // Check player distance
        float distance = Vector2.Distance(player.position, transform.position);

        if (exitDiscovered.value && distance <= interactRadius && Input.GetKeyDown(KeyCode.E))
        {
            GetComponent<NavMeshAgent>().enabled = true;
            PathFind();
        }
    }

    private void PathFind()
    {
        GetComponent<NavMeshAgent>().SetDestination(target.position);
    }
}
