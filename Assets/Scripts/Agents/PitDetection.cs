using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitDetection : MonoBehaviour
{

    public GameObject parent;
    public SpriteRenderer sprite;
    private GameManager gm;
    private Player player;

    private void Start()
    {
        sprite = parent.GetComponent<SpriteRenderer>();
        gm = FindAnyObjectByType<GameManager>();

        if (parent.tag == "Player")
        {
            player = parent.gameObject.GetComponent<Player>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Pit")
        {
            if (parent.tag == "Enemy")
            {
                parent.GetComponent<CircleCollider2D>().enabled = false;
                StartCoroutine(FadeOut());                     
            }

            if(parent.tag == "Player")
            { 
                if (!player.isDashing)
                {
                    gm.KillPlayer();
                }   
            }
            
        }
       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Pit")
        {
            if (parent.tag == "Enemy")
            {
                StartCoroutine(FadeIn());
                parent.GetComponent<CircleCollider2D>().enabled = true;
            }

        }
    }

    private IEnumerator FadeIn()
    {
        float alphaVal = sprite.color.a;
        Color tmp = sprite.color;

        while (sprite.color.a < 1)
        {
            alphaVal += 0.01f;
            tmp.a = alphaVal;
            sprite.color = tmp;

            yield return new WaitForSeconds(0.001f); // update interval
        }
    }

    private IEnumerator FadeOut()
    {
        float alphaVal = sprite.color.a;
        Color tmp = sprite.color;

        while (sprite.color.a > 0)
        {
            alphaVal -= 0.01f;
            tmp.a = alphaVal;
            sprite.color = tmp;

            yield return new WaitForSeconds(0.001f); // update interval
        }
    }

}
