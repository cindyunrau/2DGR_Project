using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Enemy
{
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
}
