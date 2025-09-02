using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ghost : Enemy
{
    private Animator animator;
    public float normalSpeed = 2f;
    public float wallSpeed = 1f;

    private Vector2 lastPosition; // store previous frameÅfs position
    private Vector2 movement;     // store current movement vector

    public AudioClip deathSound;

    private void Start()
    {
        //sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        lastPosition = transform.position;
    }

    private void Update()
    {
        if (health <= 0)
        {
            float pitch = Random.Range(1.05f, 1.3f);
            SoundManager.instance.playSoundClip(deathSound, this.transform, .8f, pitch);
            enemyCount.value--;
            Destroy(this.gameObject);
        }
    }

    private void FixedUpdate()
    {
        Pathfind();
        HandleAnimations();

        // Update lastPosition at end of frame
        lastPosition = transform.position;
    }

    protected override void Pathfind()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance > 0.5)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            moveSpeed = wallSpeed;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            moveSpeed = normalSpeed;
        }
    }

    private void HandleAnimations()
    {
        // Capture velocity of enemy.
        movement = ((Vector2)transform.position - lastPosition).normalized;

        // If moving (always, basically)
        if (movement.sqrMagnitude > 0.01f)
        {
            // If more horizontal than vertical movement, use horizontal animation.
            if (Mathf.Abs(movement.x) > Mathf.Abs(movement.y))
            {
                if (movement.x > 0)
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
