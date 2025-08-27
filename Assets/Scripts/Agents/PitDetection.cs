using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitDetection : MonoBehaviour
{

    public GameObject parent;
    public SpriteRenderer sprite;

    private void Start()
    {
        sprite = parent.GetComponent<SpriteRenderer>();
        //Color tmp = sprite.color;
        //tmp.a = 1.0f;
        //sprite.color = tmp;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Pit")
        {
            if (parent.tag == "Enemy")
            {
                StartCoroutine(FadeOut());          
                parent.GetComponent<CircleCollider2D>().enabled = false;
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
        Debug.Log("Hi");
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
