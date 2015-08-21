using UnityEngine;
using System.Collections;

public static class ExtensionMethodsBounds
{
    // -- PUBLIC

    // .. EXTENSION_METHODS

    /// <summary>
    /// Adjust this Bounds to be the smallest Bounds containing given points.
    /// </summary>
    /// <param name="bounds"></param>
    /// <param name="point_table"></param>
    /// <returns></returns>
    public static Bounds CalculateBoundingBounds(this Bounds bounds, params Vector3[] point_table)
    {
        Vector3
            minimum,
            maximum;

        minimum = point_table[0];
        maximum = point_table[0];

        foreach (Vector3 point in point_table)
        {
            minimum.x = Mathf.Min(point.x, minimum.x);
            minimum.y = Mathf.Min(point.y, minimum.y);
            minimum.z = Mathf.Min(point.z, minimum.z);

            maximum.x = Mathf.Max(point.x, maximum.x);
            maximum.y = Mathf.Max(point.y, maximum.y);
            maximum.z = Mathf.Max(point.z, maximum.z);
        }

        bounds.min = minimum;
        bounds.max = maximum;

        return bounds;
    }
    
    /// <summary>
    /// Returns whether this Bounds (fully) contains given Bounds.
    /// </summary>
    /// <param name="bounds"></param>
    /// <param name="smaller_bounds"></param>
    /// <returns></returns>
    public static bool Contains(this Bounds bounds, Bounds smaller_bounds)
    {
        if (smaller_bounds.min.x < bounds.min.x)
        {
            return false;
        }

        if (smaller_bounds.min.y < bounds.min.y)
        {
            return false;
        }

        if (smaller_bounds.min.z < bounds.min.z)
        {
            return false;
        }

        if (smaller_bounds.max.x > bounds.max.x)
        {
            return false;
        }

        if (smaller_bounds.max.y > bounds.max.y)
        {
            return false;
        }

        if (smaller_bounds.max.z > bounds.max.z)
        {
            return false;
        }

        return true;
    }
    
    /// <summary>
    /// Transform this bounds by given matrix.
    /// </summary>
    /// <param name="bounds"></param>
    /// <param name="matrix"></param>
    /// <returns></returns>
    public static Bounds MultiplyWithMatrix(this Bounds bounds, Matrix4x4 matrix)
    {
        bounds.SetMinMax(matrix.MultiplyVector(bounds.min), matrix.MultiplyVector(bounds.max));
        return bounds;
    }
}
