using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomUtil : MonoBehaviour
{
    private RandomUtil() { }

    public static Vector2 GetRandomPositionInBox(Vector2 center, Vector2 size)
    {
        float x = Random.Range(-size.x / 2, size.x / 2);
        float y = Random.Range(-size.y / 2, size.y / 2);
        return center + new Vector2(x, y);
    }

    public static Vector2 GetRandomPositionInLevel(Vector2 boundingBoxCenter, Vector2 boundingBoxSize, LayerMask obstacleMask, float minDistanceToObstacle)
    {
        Vector2 position = Vector2.zero;
        do
        {
            position = GetRandomPositionInBox(boundingBoxCenter, boundingBoxSize);
        } while (Physics2D.OverlapCircle(position, minDistanceToObstacle, obstacleMask) != null);

        return position;
    }
}
