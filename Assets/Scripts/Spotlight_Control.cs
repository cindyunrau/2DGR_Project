using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using static UnityEngine.GraphicsBuffer;

public class Spotlight_Control : MonoBehaviour
{

    //holds a reference to the attached spotlight
    private Light2D spotlight;

    //holds a reference to the game manager object
    public GameManager gm;

    //the magnitude of the largest possible outer radius minus the smallest possible outer radius
    public float outerRange;

    //the magnitude of the largest possible inner radius minus the smallest possible inner radius
    public float innerRange;

    //Target size for the outer radius
    public float targetOuterRadius;

    //Target size for the outer radius
    public float targetInnerRadius;

    //The time it takes for the spotlight to transition between maximum and minimum radius values
    public float smoothTime = 0.75f;

    //The change in outer radius every fixed update
    private float deltaSizeOuter = 0f;

    //The change in inner radius every fixed update
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
        outerRange = spotlight.pointLightOuterRadius;
        innerRange = spotlight.pointLightInnerRadius;
    }

    // FixedUpdate is called every fixed update frame (default 50 times per second)
    void FixedUpdate()
    {
        //Checking if the spotlight should be shrinking
        if (shrinking)
        {

            //Checking if the spotlight radiuses have reached their minimum radius values
            if (((spotlight.pointLightOuterRadius - targetOuterRadius) > 0.01) && ((spotlight.pointLightInnerRadius - targetInnerRadius) > 0.01))
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
            if (((targetOuterRadius - spotlight.pointLightOuterRadius) > 0.01) && ((targetInnerRadius - spotlight.pointLightInnerRadius) > 0.01))
            {

                //if the spotlights haven't reached their maximum radius values, adding delta to the radiuses
                spotlight.pointLightOuterRadius += deltaSizeOuter;
                spotlight.pointLightInnerRadius += deltaSizeInner;
            }
        }
    }

    //set function for the growing flag
    public void setGrowing(float targetOut, float targetIn)
    {
        targetOuterRadius = targetOut;
        targetInnerRadius = targetIn;

        deltaSizeOuter = ((targetOuterRadius - spotlight.pointLightOuterRadius) / smoothTime) / 50;
        deltaSizeInner = ((targetInnerRadius - spotlight.pointLightInnerRadius) / smoothTime) / 50;

        growing = true;
        shrinking = false;
    }

    //set function for the shrinking flag
    public void setShrinking(float targetOut, float targetIn)
    {
        targetOuterRadius = targetOut;
        targetInnerRadius = targetIn;

        deltaSizeOuter = ((spotlight.pointLightOuterRadius - targetOuterRadius) / smoothTime) / 50;
        deltaSizeInner = ((spotlight.pointLightInnerRadius - targetInnerRadius) / smoothTime) / 50;

        growing = false;
        shrinking = true;

    }

    // Update is called once per frame
    void Update()
    {

    }
}
