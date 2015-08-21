using System;
using UnityEngine;

public static class ExtensionMethodsQuaternion
{
    // -- PUBLIC

    // .. EXENSION_METHODS

    /// <summary>
    /// Check if the quaternion contains NaNs.
    /// </summary>
    /// <param name="quaternion"></param>
    /// <returns></returns>
    public static bool IsValid(this Quaternion quaternion)
    {
        return !(float.IsNaN(quaternion.x) || float.IsNaN(quaternion.y) || float.IsNaN(quaternion.z) || float.IsNaN(quaternion.w));
    }
}
