using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Enemy
{
    public float normalSpeed = 2f;
    public float wallSpeed = 1f;

    private void FixedUpdate()
    {
        Pathfind();
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

}
