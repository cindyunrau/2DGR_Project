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
    public GameManager gameManager;
    public Spotlight_Control spotlight;
    public AudioClip takeDamage;
    //public CapsuleCollider2D collider;

    [Header("Health Variables")]
    public float damageCooldown = 0.5f;
    public bool isImmune = false;
    public bool wardHealable = true;


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
    [SerializeField] private TrailRenderer trail;

    private bool dead = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        //collider = GetComponent<CapsuleCollider2D>();
    }

    void FixedUpdate()
    {
        if (!dead)
        {
            if (isDashing)
            {
                return;
            }

            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            movement = Vector2.ClampMagnitude(movement, 1);
            
            //rb.velocity = new Vector2(movement.x * moveSpeed, movement.y * moveSpeed);
            if(movement != Vector2.zero)
            {
                animator.SetBool("isMoving", true);
                rb.AddForce(movement * moveSpeed * Time.fixedDeltaTime);
            } 
            else
            {
                animator.SetBool("isMoving", false);
            }

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
        // Trigger temporary
        if (Input.GetKeyDown(KeyCode.Z))
        {
            gameManager.UseFuel();
            // Add Fuel Functionality
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Shambler" || collision.gameObject.tag == "Ghost")
        {
            if (!isImmune)
            {
                float pitch = UnityEngine.Random.Range(0.95f, 1.05f);
                SoundManager.instance.playSoundClip(takeDamage, this.transform, 1f,pitch);
                gameManager.DamagePlayer(1);

                //float percentHealth = (float)gameManager.getHealth() / (float)gameManager.getMaxHealth();

                //spotlight.setShrinking((spotlight.outerRange * percentHealth), (spotlight.innerRange * percentHealth));


                if (!dead)
                {
                    StartCoroutine(IFrames(damageCooldown));
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ammo")
        {
            gameManager.AddAmmo(other.gameObject.GetComponent<Pickup>().getNum());
            other.gameObject.SetActive(false);
        }
        else if (other.gameObject.tag == "Fuel")
        {
            print("Fuel");
            gameManager.AddFuel(1);
            other.gameObject.SetActive(false);
        }

    }

    public void stopAllMovement()
    {
        rb.velocity = Vector2.zero;
    }

    public void setDead()
    {
        isImmune = true;
        dead = true;
    }
    public bool isDead()
    {
        return dead;
    }

    private IEnumerator Dash()
    {
        // Dash
        canDash = false;
        isDashing = true;
        Vector2 dashForce = new Vector2(movement.x * dashStrength, movement.y * dashStrength);
        rb.AddForce(dashForce, ForceMode2D.Impulse);
        StartCoroutine(IFrames(dashTime));
        trail.emitting = true;
        yield return new WaitForSeconds(dashTime);
        trail.emitting = false;
        isDashing = false;

        // Dash cooldown
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    private IEnumerator IFrames(float length)
    {
        isImmune = true;
        yield return new WaitForSeconds(length);
        isImmune = false;
    }
}