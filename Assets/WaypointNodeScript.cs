using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class WaypointNodeScript : MonoBehaviour
{
    public bool LookBack = false;
    public float WalkModifier = 3;
    public Material NormalMaterial;
    public Material LookBackmaterial;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Renderer>().material = LookBack ? LookBackmaterial : NormalMaterial;

    }
}
