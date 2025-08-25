using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private Vector2 target;
    [SerializeField] private GameObject player;
    [SerializeField] private float moveSpeed = 3f;
    private float distance;
    

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        target = player.transform.position;
        distance = Vector2.Distance(transform.position, target);
        if (distance > 1)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
        }
    }
}
