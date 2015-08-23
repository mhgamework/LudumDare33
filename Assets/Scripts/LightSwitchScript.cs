using UnityEngine;
using System.Collections;

public class LightSwitchScript : MonoBehaviour
{

    [SerializeField]
    private DamagingLightScript light;
    [SerializeField]
    private Material OnMaterial;
    [SerializeField]
    private Material OffMaterial;
    [SerializeField]
    private MeshRenderer ButtonRenderer;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    internal void Interact()
    {
        light.IsLightOn = !light.IsLightOn;

        ButtonRenderer.material = light.IsLightOn ? OnMaterial : OffMaterial;

    }
}
