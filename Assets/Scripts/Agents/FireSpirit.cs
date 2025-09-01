using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Rendering.Universal;

public class FireSpirit : MonoBehaviour
{
    private Animator animator;
    public float interactRadius = 2f;
    public float moveSpeed = 0.5f; 
    public AudioClip takeDamage;
    public AudioClip spiritScream;

    [Header("Health Variables")]
    public int health;
    public int maxHealth = 0;
    public int healthCap = 10;
    public float damageCooldown = 0.5f;
    public bool isImmune = false;
    public bool dead = false;

    [Header("Fuel Variables")]
    public float fuelingCooldown = 0.2f;
    public bool canRecieveFuel = true;

    [Header("References")]
    public Transform player;
    public GameManager gm;
    public BooleanValue exitDiscovered;
    public BooleanValue phase2Started;
    public GameObject exitBarrier;
    private NavMeshAgent agent;
    private Transform target;
    public Spotlight_Control spotlight;
    public GameObject safetyWard;


    public void Start()
    {
        target = exitBarrier.transform;
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = moveSpeed;
    }

    void Update()
    {
        // Check player distance
        float distance = Vector2.Distance(player.position, transform.position);
        // Initiate Phase 2
        if (exitDiscovered.value && distance <= interactRadius && maxHealth > 0 && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Starting phase 2!");
            SoundManager.instance.startPhase2Music();
            SoundManager.instance.playSoundClip(spiritScream, this.transform, 0.85f, 1.35f);
            phase2Started.value = true;
            GetComponent<NavMeshAgent>().enabled = true;
            GetComponent<CircleCollider2D>().enabled = true;
            safetyWard.SetActive(false);
            PathFind();
        }

        if (!phase2Started.value)
        {
            HandlePhase1Animations();
        }
        else
        {
            HandlePhase2Animations();
        }
    }

    public void FixedUpdate()
    {
        float distance = Vector2.Distance(player.position, transform.position);
        // Give fuel to sprite
        if(distance <= interactRadius && gm.GetFuel() > 0 && canRecieveFuel)
        {
            canRecieveFuel = false;
            StartCoroutine(FuelSpirit());
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Shambler" || collision.gameObject.tag == "Ghost")
        {
            if (!isImmune)
            {
                DamageFireSpirit(1);        
            }

            if (!dead)
            {
                StartCoroutine(IFrames(damageCooldown));
            }
        }
    }

    private void DamageFireSpirit(int damage)
    {
        float pitch = UnityEngine.Random.Range(0.95f, 1.05f);
        SoundManager.instance.playSoundClip(takeDamage, this.transform, 1f, pitch);

        health -= damage;

        float percentHealth = (float)health / (float)maxHealth;
        spotlight.setShrinking((spotlight.outerRange * percentHealth), (spotlight.innerRange * percentHealth));

        if (health <= 0)
        {
            KillFireSpirit();
        }
    }

    private void KillFireSpirit()
    {
        dead = true;
        this.gameObject.SetActive(false);
        gm.DamagePlayer(gm.getHealth());
    }

    private void PathFind()
    {
        GetComponent<NavMeshAgent>().SetDestination(target.position);
    }

    private IEnumerator FuelSpirit()
    {
        if (!phase2Started.value)
        {
            if (maxHealth < healthCap)
            {
                maxHealth++;
                spotlight.outerRange += 0.25f;
                spotlight.innerRange += 0.25f;
                spotlight.setGrowing(spotlight.outerRange, spotlight.innerRange);
                health++;
                gm.UseFuel();
            }
        }
        else
        {
            if (health < maxHealth) 
            {
                health++;
                float percentHealth = (float)health / (float)maxHealth;
                spotlight.setGrowing((spotlight.outerRange * percentHealth), (spotlight.innerRange * percentHealth));
                gm.UseFuel();
            }
        }

        yield return new WaitForSeconds(fuelingCooldown);

        canRecieveFuel = true;

    }

    private IEnumerator IFrames(float length)
    {
        isImmune = true;
        yield return new WaitForSeconds(length);
        isImmune = false;
    }

    private void HandlePhase1Animations()
    {
        if (player == null) return;

        Vector2 dir = (player.position - transform.position).normalized;
        
        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
        {
            if (dir.x > 0)
            {
                animator.SetInteger("direction", 2);
            }
            else
            {
                animator.SetInteger("direction", 0);
            }  
        }
        else
        {
            if (dir.y > 0)
            {
                animator.SetInteger("direction", 1);
            }
            else
            {
                animator.SetInteger("direction", 3);
            }
        }
    }

    private void HandlePhase2Animations()
    {
        // Capture velocity of fire spirit.
        Vector2 velocity = agent.velocity;

        // If moving (so always, basically)
        if (velocity.sqrMagnitude > 0.01f)
        {
            // Extract direction.
            Vector2 dir = velocity.normalized;

            // If more horizontal than vertical movement, use horizontal animation.
            if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
            {
                if (dir.x > 0)
                {
                    animator.SetInteger("direction", 2);
                }
                else
                {
                    animator.SetInteger("direction", 0);
                }
            }
            else
            {
                if (dir.y > 0)
                {
                    animator.SetInteger("direction", 1);
                }
                else
                {
                    animator.SetInteger("direction", 3);
                }
                
            }
        }
    }
}
