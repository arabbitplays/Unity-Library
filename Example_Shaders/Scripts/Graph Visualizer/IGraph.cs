using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGraph
{
    Vector3 function(float u, float v, float time);
}
