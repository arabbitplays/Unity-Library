using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphVisualizer : MonoBehaviour
{
    [SerializeField] private GameObject pointPrefab;
    [SerializeField, Range(10, 100)] private int resolution;
    [SerializeField] private float animationSpeed;
    private IGraph graph;
    private Vector3[,] values;
    private Transform[,] points;
    private float time;
    private int currResolution;
    [SerializeField] private float stretchFactor;

    private void Start()
    {
        graph = new Torus();

        setResolution(resolution);
    }

    private void setResolution(int resolution)
    {
        currResolution = resolution;
        if (points != null)
        {
            foreach(Transform point in points)
            {
                Destroy(point.gameObject);
            }
        }

        points = new Transform[currResolution, currResolution];
        values = new Vector3[currResolution, currResolution];
        float delta = 1f / currResolution * 2;
        for (int i = 0; i < currResolution; i++)
        {
            for (int j = 0; j < currResolution; j++)
            {
                points[i, j] = Instantiate(pointPrefab, Vector3.zero, Quaternion.identity).transform;
                points[i, j].localScale = Vector3.one / currResolution * 2;
                points[i, j].SetParent(transform);
            }
        }
    }

    private void FixedUpdate()
    {
        if (resolution != currResolution)
        {
            setResolution(resolution);
        }
        updateValues();
        visualizePoints();
        time += Time.fixedDeltaTime * animationSpeed;
    }

    private void updateValues()
    {
        float step = 2f / currResolution;
        for (int x = 0; x < currResolution; x++)
        {
            for (int z = 0; z < currResolution; z++)
            {
                float u = (x + 0.5f) * step - 1f;
                float v = (z + 0.5f) * step - 1f;
                values[x, z] = graph.function(u, v, time);
            }
        }
    }

    private void visualizePoints()
    {
        for (int i = 0; i < currResolution; i++)
        {
            for (int j = 0; j < currResolution; j++)
            {
                points[i, j].position = stretchFactor * values[i, j];
            }
        }
    }
}
