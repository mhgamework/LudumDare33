using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethodsTransform
{
    // -- PUBLIC

    // .. EXENSION_METHODS

    /// <summary>
    /// Adjusts the local scale of this Transform without affecting the world position of its children.
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="scale"></param>
    public static void SetSelfLocalScale(this Transform transform, Vector3 scale)
    {
        Transform[] child_table;

        child_table = DisconnectChildren(transform);

        transform.localScale = scale;

        ConnectChildern(transform, child_table);
    }

    /// <summary>
    /// Initializes this Transform to its default values.
    /// </summary>
    /// <param name="transform"></param>
    public static void Reset(this Transform transform)
    {
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.localScale = Vector3.one;
    }

    public static void Lerp(this Transform transform, Transform start, Transform end, float t)
    {
        transform.localPosition = Vector3.Lerp(start.localPosition, end.localPosition, t);
        transform.localRotation = Quaternion.Lerp(start.localRotation, end.localRotation, t);
        transform.localScale = Vector3.Lerp(start.localScale, end.localScale, t);
    }

    public static void Lerp(this Transform transform, TransformValue start, TransformValue end, float t)
    {
        transform.localPosition = Vector3.Lerp(start.LocalPosition, end.LocalPosition, t);
        transform.localRotation = Quaternion.Lerp(start.LocalRotation, end.LocalRotation, t);
        transform.localScale = Vector3.Lerp(start.LocalScale, end.LocalScale, t);
    }

    public static Matrix4x4 LocalToWorldMatrixWithoutScale(this Transform transform)
    {
        Matrix4x4 matrix;
        matrix = new Matrix4x4();
        matrix.SetTRS(transform.position, transform.rotation, Vector3.one);
        return matrix;
    }

    public static Matrix4x4 WorldToLocalMatrixWithoutScale(this Transform transform)
    {
        return LocalToWorldMatrixWithoutScale(transform).inverse;
    }

    // -- PRIVATE

    // .. FUNCTIONS

    static Transform[] DisconnectChildren(Transform parent)
    {
        Transform[] child_table;

        child_table = new Transform[parent.childCount];

        for (int index = child_table.Length; index >= 0; index--)
        {
            Transform
                child;

            child = parent.GetChild(index);
            child_table[index] = child;
            child.parent = null;
        }

        return child_table;
    }

    static void ConnectChildern(Transform parent, Transform[] child_table)
    {
        for (int index = 0; index < child_table.Length; index++)
        {
            child_table[index].parent = parent;
        }
    }



}