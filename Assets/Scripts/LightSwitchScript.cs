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

    [SerializeField] private AudioClip lightOnClip;
    [SerializeField] private AudioClip lightOffClip;
    [SerializeField] private AudioSource clickAudioSource;

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
        if (light.IsLightOn)
        {
            clickAudioSource.clip = lightOffClip;
            clickAudioSource.Play();
        }
        else
        {
            clickAudioSource.clip = lightOnClip;
            clickAudioSource.Play();
        }
        light.IsLightOn = !light.IsLightOn;
        ButtonRenderer.material = light.IsLightOn ? OnMaterial : OffMaterial;

    }
}
