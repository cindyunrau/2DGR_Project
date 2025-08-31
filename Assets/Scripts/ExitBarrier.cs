using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ExitBarrier : MonoBehaviour
{
    public GameManager gm;
    public float interactRadius = 1.25f;
    public Transform player;

    private Animator animator;
    private Collider2D resrcCollider;
    public FadeInUI exitText;
    public BooleanValue exitFound;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        resrcCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        // Check player distance
        float distance = Vector2.Distance(player.position, transform.position);

        // If exit has not yet been found
        if (!exitFound.value)
        {
            // If inside radius, highlight
            if (distance <= interactRadius)
            {
                animator.SetBool("isPlayerNear", true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    exitFound.value = true;
                    exitText.gameObject.SetActive(true);
                    StartCoroutine(FadeOut(exitText, 2f));
                    animator.SetBool("isPlayerNear", false);
                }

                // Do other things here...
            }
            // Otherwise, no highlight
            else
            {
                animator.SetBool("isPlayerNear", false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "FireSpirit")
        {
            DestroyBarrier();
        }
    }

    private void DestroyBarrier()
    {
        gameObject.SetActive(false);
    }

    IEnumerator FadeOut(FadeInUI text, float duration)
    {
        yield return new WaitForSeconds(duration);
        StartCoroutine(text.FadeOut());
    }
}
