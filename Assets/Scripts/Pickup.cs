using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public enum Type
    {
        fuel, ammo
    }

    public Type pickupType;
    public int num = 1;

    public int getNum()
    {
        return num;
    }

}
