using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    public GameObject player;
    public GameObject fireSpirit;
    public Transform target;
    public float distance;
    public float moveSpeed;
    public BooleanValue phase2Started;

    public AudioClip hitSound;

    [Header("Health Variables")]
    public int maxHealth;
    public int health;
    public bool wardDamageable = true;

    private void Awake()
    {
        setHealth(maxHealth);
        player = GameObject.FindWithTag("Player");
        fireSpirit = GameObject.FindWithTag("FireSpirit");

        if (!phase2Started.value)
        {
            target = player.transform;
        }
        else
        {
            target = fireSpirit.transform;
        }
        
    }

    protected virtual void Pathfind()
    {
        Debug.Log("Pathfinding to " + target.name);
    }

    public int getHealth()
    {
        return health;
    }

    public void setHealth(int hp)
    {
        health = hp;
    }

    public void takeDamage(int dmg)
    {
        float pitch = Random.Range(1.9f, 2.0f);
        SoundManager.instance.playSoundClip(hitSound, this.transform, 1f, pitch);
        setHealth(health - dmg);

       // if (health <= 0)
       // {
       //     Destroy(this.gameObject);
       // }
    }

}
