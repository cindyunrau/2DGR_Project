using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidewaysDoor : MonoBehaviour
{
    public float interactRadius = 2f;
    public Transform player;

    public Animator farDoorAnimator;
    public Animator nearDoorAnimator;

    private Collider2D doorCollider;
    private bool isOpen = false;

    void Awake()
    {
        doorCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        float distance = Vector2.Distance(player.position, transform.position);

        if (!isOpen && distance <= interactRadius && Input.GetKeyDown(KeyCode.E))
        {
            OpenDoors();
        }
    }

    void OpenDoors()
    {
        isOpen = true;

        if (farDoorAnimator != null)
            farDoorAnimator.SetBool("isOpen", true);

        if (nearDoorAnimator != null)
            nearDoorAnimator.SetBool("isOpen", true);

        doorCollider.enabled = false;
    }
}
