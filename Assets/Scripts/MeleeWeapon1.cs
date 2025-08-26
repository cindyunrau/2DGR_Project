using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class MeleeWeapon1 : MonoBehaviour
{
    private Animator anim;

    private bool swinging = false;

    public int swingFrames = 45;

    public float knockbackFactor = 20f;

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

    /* Work In Progress
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (swinging)
            {
                Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();

                Vector2 pushbackDirection = (collision.transform.position - player.transform.position).normalized;

                rb.AddForce((pushbackDirection * knockbackFactor),ForceMode2D.Impulse);

                anim.SetBool("swinging", false);
                frameCounter = 0;
                swinging = false;
            }
            
        }

    }*/
}
