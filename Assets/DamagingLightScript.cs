﻿using UnityEngine;
using System.Collections;

public class DamagingLightScript : MonoBehaviour {
    private bool isLightOn;
    public GameObject LightObject;
    public DetectPlayerScript DetectPlayerScript;
    [SerializeField] private AudioSource humm = null;

	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public bool IsLightOn
    {
        get { return isLightOn; }
        set
        {
            if (isLightOn == value) return;
            isLightOn = value;
            LightObject.SetActive(isLightOn);

            if (!isLightOn)
            {
                DetectPlayerScript.DisableLight();
                humm.Stop();
            }
            else
            {
                DetectPlayerScript.EnableLight();
                humm.Play();
            }
        }
    }
}
