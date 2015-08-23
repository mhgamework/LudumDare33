using UnityEngine;
using System.Collections;

public class WaypointPathScript : MonoBehaviour
{
    

    public Vector3 PathDrawOffset;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void OnDrawGizmos()
    {


        Transform prev = null;
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            if (prev != null)
            {
                Gizmos.DrawLine(prev.transform.position + PathDrawOffset, child.transform.position + PathDrawOffset);
            }

            prev = child;
        }
    }
}
