using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Mathf;

public class Sphere : IGraph
{
	public Vector3 function(float u, float v, float time)
	{
		float r = 0.9f + 0.1f * Sin(PI * (6f*u + 4f*v + time)); // stripes
		//float r = 0.5f + 0.5f * Sin(0.5f * PI * time);
		float s = r * Cos(0.5f * PI * v);
		Vector3 p;
		p.x = s * Sin(PI * u);
		p.y = r * Sin(0.5f * PI * v);
		p.z = s * Cos(PI * u);
		return p;
	}
}
