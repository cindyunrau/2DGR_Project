using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.GlobalIllumination;

public class FireSpirit : MonoBehaviour
{

    public float interactRadius = 2f;
    public float moveSpeed = 0.5f;

    [Header("Health Variables")]
    public int health;
    public int maxHealth = 0;
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
        if (exitDiscovered.value && distance <= interactRadius && Input.GetKeyDown(KeyCode.E))
        {
            phase2Started.value = true;
            GetComponent<NavMeshAgent>().enabled = true;
            GetComponent<CircleCollider2D>().enabled = true;
            safetyWard.SetActive(false);
            PathFind();
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
            maxHealth++;
            health++;
            gm.UseFuel();
        }
        else
        {
            if (health < maxHealth) 
            {
                health++;
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
}
