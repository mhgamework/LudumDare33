using UnityEngine;
using System.Collections;

public static class ExtensionMethodsRect
{
    // -- PUBLIC

    // .. EXTENSION_METHODS

    /// <summary>
    /// Adjust this Rect to be the smallest Rect containing given points.
    /// </summary>
    /// <param name="rect"></param>
    /// <param name="point_table"></param>
    /// <returns></returns>
    public static Rect CalculateBoundingRect(this Rect rect, params Vector2[] point_table)
    {
        rect.xMin = point_table[0].x;
        rect.xMax = point_table[0].x;
        rect.yMin = point_table[0].y;
        rect.yMax = point_table[0].y;

        foreach (Vector2 point in point_table)
        {
            rect.xMin = Mathf.Min(point.x, rect.xMin);
            rect.xMax = Mathf.Max(point.x, rect.xMax);
            rect.yMin = Mathf.Min(point.y, rect.yMin);
            rect.yMax = Mathf.Max(point.y, rect.yMax);
        }

        return rect;
    }
    
    /// <summary>
    /// Returns whether given Rect is (fully) contained by this Rect.
    /// </summary>
    /// <param name="original"></param>
    /// <param name="smaller_rect"></param>
    /// <returns></returns>
    public static bool Contains(this Rect original, Rect smaller_rect)
    {
        if (smaller_rect.xMin < original.xMin)
        {
            return false;
        }

        if (smaller_rect.xMax > original.xMax)
        {
            return false;
        }

        if (smaller_rect.yMin < original.yMin)
        {
            return false;
        }

        if (smaller_rect.yMax > original.yMax)
        {
            return false;
        }

        return true;
    }
    
    /// <summary>
    /// Returns whether given point is contained by the Rect.
    /// </summary>
    /// <param name="original"></param>
    /// <param name="point"></param>
    /// <returns></returns>
    public static bool ContainsExtended(this Rect original, Vector2 point)
    {
        if (point.x < original.xMin)
        {
            return false;
        }

        if (point.x > original.xMax)
        {
            return false;
        }

        if (point.y < original.yMin)
        {
            return false;
        }

        if (point.y > original.yMax)
        {
            return false;
        }

        return true;
    }
}
