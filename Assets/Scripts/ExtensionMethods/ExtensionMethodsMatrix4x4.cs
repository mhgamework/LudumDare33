using System;
using UnityEngine;

public static class ExtensionMethodsMatrix4x4
{
    // -- PUBLIC

    // .. EXENSION_METHODS

    public static Quaternion GetRotation(this Matrix4x4 matrix)
    {
        Vector3
            up_vector,
            forward_vector;

        up_vector = matrix.GetColumn(1);
        forward_vector = matrix.GetColumn(2); // THIS WAS 1, changed to 2 but untested

        if (up_vector != Vector3.zero && forward_vector != Vector3.zero && up_vector != forward_vector)
        {
            return Quaternion.LookRotation(forward_vector, up_vector);
        }
        else
        {
            MonoBehaviour.print("invalid rotation");
        }
        return Quaternion.identity;
    }
    
    public static void GetRotation(this Matrix4x4 matrix, ref Quaternion quaternion)
    {
        Vector3
            up_vector,
            forward_vector;

        up_vector = matrix.GetColumn(1);
        forward_vector = matrix.GetColumn(2); // THIS WAS 1, changed to 2 but untested

        if (up_vector != Vector3.zero && forward_vector != Vector3.zero && up_vector != forward_vector)
        {
            quaternion = Quaternion.LookRotation(forward_vector, up_vector);
        }
        else
        {
            MonoBehaviour.print("invalid rotation");
            quaternion = Quaternion.identity;
        }
    }
}