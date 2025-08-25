using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Spotlight_Control : MonoBehaviour
{
    private Light2D spotlight;

    public float maxOuterRadius = 5f;

    public float minOuterRadius = 0.5f;

    public float maxInnerRadius = 0.5f;

    public float minInnerRadius = 0f;

    public float smoothTime = 10f;

    private float deltaSizeOuter = 0f;

    private float deltaSizeInner = 0f;

    private bool growing = false;

    private bool shrinking = false;

    // Start is called before the first frame update
    void Start()
    {
        spotlight = GetComponent<Light2D>();
        spotlight.pointLightOuterRadius = maxOuterRadius;
        spotlight.pointLightInnerRadius = maxInnerRadius;
        deltaSizeOuter = ((maxOuterRadius - minOuterRadius) / smoothTime) / 50f;
        deltaSizeInner = ((maxInnerRadius - minInnerRadius) / smoothTime) / 50f;
    }

    // FixedUpdate is called every fixed update frame (default 50 times per second)
    void FixedUpdate()
    {
        if (shrinking)
        {
            if (((spotlight.pointLightOuterRadius - minOuterRadius) > 0.01) && ((spotlight.pointLightInnerRadius - minInnerRadius) > 0.01))
            {
                spotlight.pointLightOuterRadius -= deltaSizeOuter;
                spotlight.pointLightInnerRadius -= deltaSizeInner;
            }
        }
        if (growing)
        {
            if (((maxOuterRadius - spotlight.pointLightOuterRadius) > 0.01) && ((maxInnerRadius - spotlight.pointLightInnerRadius) > 0.01))
            {
                spotlight.pointLightOuterRadius += deltaSizeOuter;
                spotlight.pointLightInnerRadius += deltaSizeInner;
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            growing = true;
            shrinking = false;
        }
        if (Input.GetButtonDown("Fire2"))
        {
            shrinking = true;
            growing = false;
        }
    }
}
