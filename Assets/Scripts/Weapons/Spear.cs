using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Spear : MonoBehaviour
{

    private Animator anim;

    private bool stabbing = false;

    private int frameCounter;

    private GameObject player;

    public GameManager gm;

    public int stabFrames = 45;

    public float knockbackFactor;

    public float knockbackTime;

    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (stabbing)
        {
            gm.blockWeaponSwap = true;
            anim.SetBool("isAttacking", true);
            frameCounter++;

            if (frameCounter == stabFrames)
            {
                gm.weaponSwapCooldown = 300;
                anim.SetBool("isAttacking", false);
                frameCounter = 0;
                stabbing = false;
            }

        }

        if (Input.GetMouseButtonDown(0))
        {
            stabbing = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Shambler" || collision.gameObject.tag == "Ghost")
        {
            Rigidbody2D enemy = collision.GetComponent<Rigidbody2D>();

            if (stabbing)
            {
                if (collision.TryGetComponent<Shambler>(out Shambler shambler))
                {
                    shambler.takeDamage(damage);
                }
                else if (collision.TryGetComponent<Ghost>(out Ghost ghost))
                {
                    ghost.takeDamage(damage);
                }


                Vector2 difference = (enemy.transform.position - player.transform.position).normalized * knockbackFactor;
                enemy.AddForce(difference, ForceMode2D.Impulse);
                StartCoroutine(KnockbackEnd(enemy));

                gm.weaponSwapCooldown = 300;
                anim.SetBool("isAttacking", false);
                frameCounter = 0;
                stabbing = false;

            }

        }
    }

    private IEnumerator KnockbackEnd(Rigidbody2D enemy)
    {
        //if (enemy)
        //{
        yield return new WaitForSeconds(knockbackTime);
        print("enemyvel");
        // Second check
        if (enemy)
        {
            enemy.velocity = Vector2.zero;
        }
        //}
    }
}
