using UnityEngine;
using System.Collections;

public static class ExtensionMethodsSkinnedMeshRenderer
{
    // -- PUBLIC

    // .. EXENSION_METHODS

    public static Vector3 LocalPointToWorldSpace(this SkinnedMeshRenderer skinned_mesh_renderer, Vector3 point)
    {
        Transform transform;
        transform = skinned_mesh_renderer.GetComponent<Transform>();
        return transform.rotation * point + transform.position;
    }
}
