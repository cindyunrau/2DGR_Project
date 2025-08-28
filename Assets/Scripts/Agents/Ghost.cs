using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Enemy
{
    //Health Variables
    public int maxHealth;
    private int health;
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
        //Debug.Log("Ghost collision: " + collision.gameObject.tag);
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

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Debug.Log("Ghost collision: " + collision.gameObject.tag);
    //    if (collision.gameObject.tag == "Wall")
    //    {

    //        moveSpeed = 0.2f;
    //    }
    //}
    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Wall")
    //    {
    //        moveSpeed = 2f;
    //    }
    //}

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
        setHealth(health - dmg);

        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
