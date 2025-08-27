using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float thrust;
    public float knockbackTime;
    public Player player;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = player.GetComponent<Rigidbody2D>();
    }
    

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Rigidbody2D enemy = collision.GetComponent<Rigidbody2D>();
            
            if (enemy && !player.isDead())
            {
                Vector2 difference = (enemy.transform.position - transform.position).normalized * thrust;
                enemy.AddForce(difference, ForceMode2D.Impulse);
                rb.AddForce(difference * -1f, ForceMode2D.Impulse);
                StartCoroutine(KnockbackEnd(enemy));
                
            }

        }
    }
    private IEnumerator KnockbackEnd(Rigidbody2D enemy)
    {
        if (enemy)
        {
            yield return new WaitForSeconds(knockbackTime);
            print("enemyvel");
            enemy.velocity = Vector2.zero;
        }
    }
}
