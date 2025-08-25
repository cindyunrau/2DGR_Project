using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Spotlight_Control : MonoBehaviour
{

    //holds a reference to the attached spotlight
    private Light2D spotlight;

    //The maximum value for the outer radius of the attached spotlight
    public float maxOuterRadius = 5f;

    //The minimum value for the outer radius of the attached spotlight
    public float minOuterRadius = 0.5f;

    //The maximum value for the inner radius of the attached spotlight
    public float maxInnerRadius = 0.5f;
    
    //The minumum value for the inner radius of the attached spotlight
    public float minInnerRadius = 0f;

    //The time it takes for the spotlight to transition between maximum and minimum radius values
    public float smoothTime = 10f;

    //The difference between the maximum and minimum outer radius values
    private float deltaSizeOuter = 0f;

    //The difference between the maximum and minimum inner radius values
    private float deltaSizeInner = 0f;

    //boolean flag for if the light should be growing
    private bool growing = false;

    //boolean flag for if the light should be shrinking
    private bool shrinking = false;

    // Start is called before the first frame update
    void Start()
    {

        //getting references and setting initial values
        spotlight = GetComponent<Light2D>();
        spotlight.pointLightOuterRadius = maxOuterRadius;
        spotlight.pointLightInnerRadius = maxInnerRadius;
        deltaSizeOuter = ((maxOuterRadius - minOuterRadius) / smoothTime) / 50f;
        deltaSizeInner = ((maxInnerRadius - minInnerRadius) / smoothTime) / 50f;
    }

    // FixedUpdate is called every fixed update frame (default 50 times per second)
    void FixedUpdate()
    {
        //Checking if the spotlight should be shrinking
        if (shrinking)
        {

            //Checking if the spotlight radiuses have reached their minimum radius values
            if (((spotlight.pointLightOuterRadius - minOuterRadius) > 0.01) && ((spotlight.pointLightInnerRadius - minInnerRadius) > 0.01))
            {

                //if the spotlights haven't reached their minimum radius values, subtract delta from the radiuses
                spotlight.pointLightOuterRadius -= deltaSizeOuter;
                spotlight.pointLightInnerRadius -= deltaSizeInner;
            }
        }

        //Checking if the spotlight should be growing
        if (growing)
        {

            //Checking if the spotlight radiuses have reached their maximum radius values
            if (((maxOuterRadius - spotlight.pointLightOuterRadius) > 0.01) && ((maxInnerRadius - spotlight.pointLightInnerRadius) > 0.01))
            {

                //if the spotlights haven't reached their maximum radius values, adding delta to the radiuses
                spotlight.pointLightOuterRadius += deltaSizeOuter;
                spotlight.pointLightInnerRadius += deltaSizeInner;
            }
        }
    }

    //set function for the growing flag
    void setGrowing(bool b)
    {
        if (b)
        {
            growing = true;
            shrinking = false;
        }
    }

    //set function for the shrinking flag
    void setShrinking(bool b)
    {
        if (b)
        {
            growing = false;
            shrinking = true;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
