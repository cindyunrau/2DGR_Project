using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class YSort : MonoBehaviour
{
    private SpriteRenderer sr;

    [Tooltip("Optional offset if the spriteÅfs pivot isnÅft at its base.")]
    public float offset = 0f;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void LateUpdate()
    {
        // Multiply by -100 to preserve decimal precision
        sr.sortingOrder = Mathf.RoundToInt((transform.position.y + offset) * -100);
    }
}
