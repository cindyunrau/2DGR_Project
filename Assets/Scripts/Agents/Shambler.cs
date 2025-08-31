using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class Shambler : Enemy
{
    private NavMeshAgent agent;
    private Animator animator;
    public AudioClip deathSound;

    private void Start()
    {
        setHealth(maxHealth);
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = moveSpeed;
    }
    private void Update()
    {
        if (health <= 0)
        {
            float pitch = Random.Range(1.1f, 1.3f);
            SoundManager.instance.playSoundClip(deathSound, this.transform, 1f, pitch);
            Destroy(this.gameObject);
        }
    }

    private void FixedUpdate()
    {
        Pathfind();
        HandleAnimations();
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

    private void HandleAnimations()
    {
        // Capture velocity of enemy.
        Vector2 velocity = agent.velocity;

        // If moving (so always, basically)
        if (velocity.sqrMagnitude > 0.01f)
        {
            // Extract direction.
            Vector2 dir = velocity.normalized;

            // If more horizontal than vertical movement, use horizontal animation.
            if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
            {
                if (dir.x > 0)
                {
                    animator.SetInteger("direction", 2);
                } 
                else
                {
                    animator.SetInteger("direction", 0);
                }
            } 
            else
            {
                animator.SetInteger("direction", 1);
            }
        }
    }
}
