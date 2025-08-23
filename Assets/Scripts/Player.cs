using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    public Transform jumpCheck;
    public GameObject sparkle;

    bool dead = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (!dead)
        {


        }
    }

    void Update()
    {

    }



    IEnumerator Reload(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Kill()
    {
        StartCoroutine(Reload(3));

        dead = true;
    }

    public bool isDead()
    {
        return dead;
    }
}