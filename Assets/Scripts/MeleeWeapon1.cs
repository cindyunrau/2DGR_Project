using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class MeleeWeapon1 : MonoBehaviour
{
    private Animator anim;

    private bool swinging = false;

    public int swingFrames = 45;

    public float knockbackFactor;
    public float knockbackTime;

    public int damage;

    private int frameCounter;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (swinging)
        {
            anim.SetBool("swinging",true);
            frameCounter++;

            if(frameCounter == swingFrames)
            {
                anim.SetBool("swinging", false);
                frameCounter = 0;
                swinging = false;
            }

        }

        if (Input.GetMouseButtonDown(0))
        {
            swinging = true;
        }

        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Rigidbody2D enemy = collision.GetComponent<Rigidbody2D>();

            if (swinging)
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


                anim.SetBool("swinging", false);
                frameCounter = 0;
                swinging = false;

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
