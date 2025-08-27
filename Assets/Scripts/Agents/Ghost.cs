using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Enemy
{
    //Health Variables
    public int maxHealth;
    private int health;

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
