using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    public Player target;

    public float offsetx;
    public float offsety;
    public float offsetz;

    void Start()
    {
        transform.position = new Vector3(target.transform.position.x + offsetx, target.transform.position.y + offsety, transform.position.z + offsetz);
    }

    void Update()
    {
        if (!target.isDead())
        {
            transform.position = new Vector3(target.transform.position.x + offsetx, target.transform.position.y + offsety, transform.position.z + offsetz);
        }
    }
}