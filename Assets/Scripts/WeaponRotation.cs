using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotation : MonoBehaviour
{

    //holds a reference to the main camera
    private Camera mainCamera;

    //a vector holding the current mouse position
    private Vector3 mousePositon;

    //a vector holding the difference between the mouse position and the position of the player
    private Vector3 diff;

    //holds the z rotation angle in degrees
    private float zRotationAngle;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePositon = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        diff = mousePositon - transform.position;

        zRotationAngle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, zRotationAngle);

    }
}
