using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    //[SerializeField] private Vector2 target;
    [SerializeField] private GameObject player;
    //[SerializeField] private float moveSpeed = 2f;
    private float distance;

    [SerializeField] private Transform target;
    NavMeshAgent agent;
    

    // Start is called before the first frame update
    void Start()
    {

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        player = GameObject.FindWithTag("Player");
    }

    private void Awake()
    {
        target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //target = player.transform.position;
        distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance > 1)
        {
            agent.SetDestination(target.position);
            //transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
        }
        
    }
}
