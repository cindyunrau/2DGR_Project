using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectionWard : MonoBehaviour
{
    private GameManager gm;

    public float healCooldown = 1f;
    public int healAmount = 1;
    public float damageCooldown = 1f;
    public int damageAmount = 2;

    private void Start()
    {
        gm = FindAnyObjectByType<GameManager>();

        //if (parent.tag == "Player")
        //{
        //    player = parent.gameObject.GetComponent<Player>();
        //}

        //if (parent.tag == "Shambler")
        //{
        //    enemy = parent.gameObject.GetComponent<Shambler>();
        //}

        //if (parent.tag == "Ghost")
        //{
        //    enemy = parent.gameObject.GetComponent<Ghost>();
        //}
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Heal player
            Player player = collision.gameObject.GetComponent<Player>();
            if (player.wardHealable) 
            {
                gm.HealPlayer(healAmount);
                player.wardHealable = false;
                StartCoroutine(healTimer(player, healCooldown));
            }
        }

        if (collision.gameObject.tag == "Shambler" || collision.gameObject.tag == "Ghost")
        {
            // Damage enemies
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy.wardDamageable)
            {
                enemy.takeDamage(damageAmount);
                enemy.wardDamageable = false;
                StartCoroutine(damageTimer(enemy, damageCooldown));
            }
        }
    }

    private IEnumerator damageTimer(Enemy enemy, float length)
    {
        yield return new WaitForSeconds(length);
        enemy.wardDamageable = true;
    }

    private IEnumerator healTimer(Player player, float length)
    {
        yield return new WaitForSeconds(length);
        player.wardHealable = true;
    }
}
