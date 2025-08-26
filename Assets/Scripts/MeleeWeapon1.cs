using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon1 : MonoBehaviour
{
    private Animator anim;

    private bool swinging = false;

    public int swingFrames = 45;

    private int frameCounter;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
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
            Debug.Log("This Runs");
            swinging = true;
        }

        
    }
}
