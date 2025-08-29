using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Spear : MonoBehaviour
{

    private bool swinging = false;

    private int frameCounter;

    private Vector3 idlePos;

    private Vector3 idleRot;

    private Vector3 animPos = new Vector3(1, 0, 0);

    private Vector3 animRot = new Vector3(0, 0, 0);

    private GameObject player;

    public int swingFrames = 45;

    public float knockbackFactor;

    public float knockbackTime;

    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        idlePos = transform.localPosition;
        idleRot = transform.localEulerAngles;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (swinging)
        {
            transform.localPosition = animPos;
            transform.localEulerAngles = animRot;
            frameCounter++;

            if (frameCounter == swingFrames)
            {
                transform.localPosition = idlePos;
                transform.localEulerAngles = idleRot;
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

                transform.localPosition = idlePos;
                transform.localEulerAngles = idleRot;
                //frameCounter = 0;
                //swinging = false;

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
