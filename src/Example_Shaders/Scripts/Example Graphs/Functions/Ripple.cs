using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Mathf;

public class Ripple : IGraph
{
    public Vector3 function(float u, float v, float time)
    {
        Vector3 p;
        p.x = u;
        p.z = v;
        float d = Sqrt(u * u + v * v);
        float y = Sin(PI * (4f * d - time));
        p.y = y / (1f + 10f * d);
        return p;
    }
}
