using UnityEngine;
using System.Collections;

public class LightSwitchScript : MonoBehaviour {

    private bool lightOn = false;
    [SerializeField]
    private GameObject lightCone;

	// Use this for initialization
	void Start () {
        lightOn = true;
        lightCone.SetActive(lightOn);
    }
	
	// Update is called once per frame
	void Update () {
	}

    internal void Interact()
    {
        lightOn = !lightOn;
        lightCone.SetActive(lightOn);
    }
}
