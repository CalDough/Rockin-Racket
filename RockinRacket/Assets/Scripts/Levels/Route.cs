using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Route : MonoBehaviour
{
    public LevelLocation levelLocation1;
    public LevelLocation levelLocation2;
    public List<Vector2> controlPoints;
    public float travelDuration = 1f;

    public Vector2 GetPointAt(float t)
    {
        return Bezier.GetPoint(controlPoints, t);
    }

    public List<Vector2> GetOrderedPoints(LevelLocation start)
    {
        if (start == levelLocation1)
        {
            return controlPoints;
        }
        else if (start == levelLocation2)
        {
            List<Vector2> reversedControlPoints = new List<Vector2>(controlPoints);
            reversedControlPoints.Reverse(); // Reverse the control points
            return reversedControlPoints;
        }
        else
        {
            return null;
        }
    }

    public static Route CreateRoute(LevelLocation level1, LevelLocation level2)
    {
        GameObject routeObject = new GameObject("Route");
        Route route = routeObject.AddComponent<Route>();

        route.levelLocation1 = level1;
        route.levelLocation2 = level2;

        Vector2 midpoint = (level1.mapLocation + level2.mapLocation) / 2;
        routeObject.transform.position = midpoint;

        route.controlPoints = new List<Vector2>
        {
            level1.mapLocation,
            midpoint,
            level2.mapLocation
        };

        level1.routes.Add(route);
        level2.routes.Add(route);

        if (!level1.connectedLevels.Contains(level2))
        {
            level1.connectedLevels.Add(level2);
        }

        if (!level2.connectedLevels.Contains(level1))
        {
            level2.connectedLevels.Add(level1);
        }

        return route;
    }


    private void OnDrawGizmos()
    {
        if (controlPoints == null || controlPoints.Count < 2) return;

        Gizmos.color = Color.yellow;
        for (int i = 0; i < controlPoints.Count - 1; i++)
        {
            Gizmos.DrawLine(controlPoints[i], controlPoints[i + 1]);
        }
    }
}

public static class Bezier
{
    public static Vector2 GetPoint(List<Vector2> points, float t)
    {
        if (points.Count == 1)
        {
            return points[0];
        }

        List<Vector2> newPoints = new List<Vector2>();

        for (int i = 0; i < points.Count - 1; i++)
        {
            Vector2 point = Vector2.Lerp(points[i], points[i + 1], t);
            newPoints.Add(point);
        }

        return GetPoint(newPoints, t);
    }
}