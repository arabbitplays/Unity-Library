using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Mathf;

public class Torus : IGraph
{
	public Vector3 function(float u, float v, float time)
	{
		float r1 = 0.7f + 0.1f * Sin(PI * (6f * u + 0.5f * time)); //star shape
		float r2 = 0.15f + 0.05f * Sin(PI * (8f * u + 4f * v + 2f * time)); //twisting
		//float r1 = 0.75f; // radius of torus itself
		//float r2 = 0.25f; // radius of the body
		float s = r1 + r2 * Cos(PI * v);
		Vector3 p;
		p.x = s * Sin(PI * u);
		p.y = r2 * Sin( PI * v);
		p.z = s * Cos(PI * u);
		return p;
	}
}
