using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BulletType : ScriptableObject
{
    [Tooltip("has to match the key from the object pool dictionary")]
    public string key;
}
