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
        rb = GetComponent<Rigidbody2D>();
        //Debug.Log(player.isImmune);
    }
    

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Shambler" || collision.gameObject.tag == "Ghost")
        {
            Rigidbody2D enemy = collision.GetComponent<Rigidbody2D>();
            if(this.tag == "Player")
            {
                if (enemy && !player.isDead() && !player.isDashing)
                {
                    Vector2 difference = (enemy.transform.position - transform.position).normalized * thrust;
                    enemy.AddForce(difference, ForceMode2D.Impulse);
                    rb.AddForce(difference * -1f, ForceMode2D.Impulse);
                    StartCoroutine(KnockbackEnd(enemy));
                }
            }
            else if(this.tag == "FireSpirit")
            {
                FireSpirit fireSpirit = GetComponent<FireSpirit>();
                if (enemy && !fireSpirit.dead)
                {
                    Vector2 difference = (enemy.transform.position - transform.position).normalized * thrust;
                    enemy.AddForce(difference, ForceMode2D.Impulse);
                    rb.AddForce(difference * -1f, ForceMode2D.Impulse);
                    StartCoroutine(KnockbackEnd(enemy));
                }
            }
            

        }
    }
    private IEnumerator KnockbackEnd(Rigidbody2D enemy)
    {
        yield return new WaitForSeconds(knockbackTime);
        print("enemyvel");
        if (enemy)
        {
            enemy.velocity = Vector2.zero;
        }
        if(this.tag == "FireSpirit")
        {
            this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }
}
