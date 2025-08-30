using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitBarrier : MonoBehaviour
{
    public GameManager gm;
    public float interactRadius = 1.25f;
    public Transform player;

    private Animator animator;
    private Collider2D resrcCollider;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        resrcCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        // Check player distance
        float distance = Vector2.Distance(player.position, transform.position);

        // If inside radius, highlight
        if (distance <= interactRadius)
        {
            animator.SetBool("isPlayerNear", true);

            // Do other things here...
        }
        // Otherwise, no highlight
        else
        {
            animator.SetBool("isPlayerNear", false);
        }
    }
}
