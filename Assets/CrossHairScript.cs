using System;
using UnityEngine;
using System.Collections;

public class CrossHairScript : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Show()
    {
        GetComponent<Canvas>().enabled = true;
    }

    public void Hide()
    {
        GetComponent<Canvas>().enabled = false;
        
    }
}
