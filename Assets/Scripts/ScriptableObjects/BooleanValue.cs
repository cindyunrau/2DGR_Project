using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BooleanValue", menuName = "ScriptableObjects/BooleanValue")]
public class BooleanValue : ScriptableObject
{
    public bool value;
    private void OnEnable() => hideFlags = HideFlags.DontUnloadUnusedAsset;
}
