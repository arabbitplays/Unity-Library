using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : IGraph
{
    public Vector3 function(float u, float v, float time)
    {
        Vector3 p;
        p.x = u;
        p.z = v;
        p.y = Mathf.Sin(Mathf.PI * (u + v + time));
        return p;
    }
}
