using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Mathf;

public class MultiWave : IGraph
{
    public Vector3 function(float u, float v, float time)
    {
        Vector3 p;
        p.x = u;
        p.z = v;
        float y = Sin(PI * (u + 0.5f * time));
        y += 0.5f * Sin(2f * PI * (v + time));
        y += Sin(PI * (u + v + 0.25f * time));
        p.y = y * (1f / 2.5f);
        return p;
    }
}
