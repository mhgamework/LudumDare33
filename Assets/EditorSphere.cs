using UnityEngine;
using System.Collections;

public class EditorSphere : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        
        Gizmos.DrawSphere(transform.position, transform.localScale.magnitude);
    }

}
