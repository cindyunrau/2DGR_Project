using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAmmoFuel : MonoBehaviour
{
    public GameManager gm;
    public float interactRadius = 1f;
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

            // Check for trying to pick up resource.
            if (Input.GetKeyDown(KeyCode.E) && gameObject.CompareTag("Ammo"))
            {
                gm.AddAmmo(Random.Range(12, 24));
                Destroy(gameObject);
            } 
            else if (Input.GetKeyDown(KeyCode.E) && gameObject.CompareTag("Fuel"))
            {
                gm.AddFuel(Random.Range(2, 6));
                Destroy(gameObject);
            }
        } 
        // Otherwise, no highlight
        else
        {
            animator.SetBool("isPlayerNear", false);
        }
    }
}
