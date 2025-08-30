using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public GameManager gm;
    public float interactRadius = 1f;
    public Transform player;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check player distance
        float distance = Vector2.Distance(player.position, transform.position);

        // If inside radius, highlight
        if (distance <= interactRadius)
        {
            animator.SetBool("isPlayerNear", true);

            // Check for trying to pick up a weapon
            if (Input.GetKeyDown(KeyCode.E) && gameObject.CompareTag("Sword"))
            {
                gm.AddSword();
                gameObject.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.E) && gameObject.CompareTag("Spear"))
            {
                gm.AddSpear();
                gameObject.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.E) && gameObject.CompareTag("Pistol"))
            {
                gm.AddPistol();
                gameObject.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.E) && gameObject.CompareTag("Shotgun"))
            {
                gm.AddShotgun();
                gameObject.SetActive(false);
            }
        }
        else
        {
            animator.SetBool("isPlayerNear", false);
        }

    }
}
