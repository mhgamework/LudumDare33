using UnityEngine;
using System.Collections;

/// <summary>
/// Stores(snapshot) the current state of a transform.
/// </summary>
public class TransformValue
{
    public Vector3 Position { get; private set; }
    public Vector3 LocalPosition { get; private set; }
    public Quaternion Rotation { get; private set; }
    public Quaternion LocalRotation { get; private set; }
    public Vector3 LossyScale { get; private set; }
    public Vector3 LocalScale { get; private set; }

    public TransformValue(Transform transform)
    {
        Position = transform.position;
        LocalPosition = transform.localPosition;
        Rotation = transform.rotation;
        LocalRotation = transform.localRotation;
        LossyScale = transform.lossyScale;
        LocalScale = transform.localScale;
    }
}
