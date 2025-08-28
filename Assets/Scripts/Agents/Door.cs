using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    public float interactRadius = 2f;
    public Transform player;

    private Animator animator;
    private Collider2D doorCollider;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        doorCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
	    // Check player distance
        float distance = Vector2.Distance(player.position, transform.position);

        if (!animator.GetBool("isOpen") && distance <= interactRadius && Input.GetKeyDown(KeyCode.E))
        {
            animator.SetBool("isOpen", true);
	        doorCollider.enabled = false;
        }
    }
}
