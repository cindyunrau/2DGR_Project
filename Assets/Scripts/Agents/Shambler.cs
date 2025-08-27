using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class Shambler : Enemy
{
    private NavMeshAgent agent;

    //Health Variables
    public int maxHealth;
    private int health;

    private void Start()
    {
        setHealth(maxHealth);
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = moveSpeed;
    }

    private void FixedUpdate()
    {
        Pathfind();
    }

    protected override void Pathfind()
    {
        //target = player.transform.position;
        distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance > 1)
        {
            agent.SetDestination(target.position);
            //transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
        }
    }

    public int getHealth()
    {
        return health;
    }

    public void setHealth(int hp)
    {
        health = hp;
    }

    public void takeDamage(int dmg)
    {
        setHealth(health-dmg);

        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
