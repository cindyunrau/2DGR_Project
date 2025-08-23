using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    public Transform jumpCheck;
    public GameObject sparkle;
    //public CapsuleCollider2D collider;

    [Header("Health Variables")]
    public int health = 5;
    public float damageCooldown = 0.5f;
    public bool isImmune = false;
    public TMP_Text healthText;

    [Header("Movement Variables")]
    public float moveSpeed;
    private Vector2 movement;

    [Header("Dash Variables")]
    public bool dashPressed = false;
    public bool canDash = true;
    public bool isDashing = false;
    public float dashStrength;
    public float dashTime;
    public float dashCooldown;

    bool dead = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        //collider = GetComponent<CapsuleCollider2D>();
    }

    void FixedUpdate()
    {
        if(health <= 0)
        {
            Kill();
        }

        if (!dead)
        {
            if (isDashing)
            {
                return;
            }

            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            movement = Vector2.ClampMagnitude(movement, 1);
            rb.velocity = new Vector2(movement.x * moveSpeed, movement.y * moveSpeed);

            if (dashPressed)
            {
                StartCoroutine(Dash());
                //StartCoroutine(IFrames(dashTime));
                dashPressed = false;
            }
        }
    }

    void Update()
    {
        if (isDashing)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            dashPressed = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            if (!isImmune)
            {
                health--;
                healthText.text = "Health : " + health;
                StartCoroutine(IFrames(damageCooldown));
            }
        }
    }



    IEnumerator Reload(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Kill()
    {
        StartCoroutine(Reload(3));
        isImmune = true;
        dead = true;
    }

    public bool isDead()
    {
        return dead;
    }

    private IEnumerator Dash()
    {
        GetComponent<CapsuleCollider2D>().enabled = false;
        canDash = false;
        isDashing = true;
        rb.velocity = new Vector2(rb.velocity.x * dashStrength, rb.velocity.y * dashStrength);
        yield return new WaitForSeconds(dashTime);
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        GetComponent<CapsuleCollider2D>().enabled = true;
        canDash = true;
    }

    private IEnumerator IFrames(float length)
    {
        isImmune = true;
        yield return new WaitForSeconds(length);
        isImmune = false;
    }
}