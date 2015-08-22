using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FlickeringLightScript : MonoBehaviour {

    [SerializeField]
    private GameObject lightCone;
    [SerializeField]
    private List<float> flickersequence = new List<float>(new float[]{0.1f});
    private float lightOffTime = 0;
    private float lightBurningTime = 0;
    private int currentFlickerIndex = 0;
    private bool lightBurning;

	// Use this for initialization
	void Start () {
	   
	}
	
	// Update is called once per frame
	void Update () {
        lightCone.SetActive(lightBurning);
        if (flickersequence.Count == 0)
        {
            return;
        }
        else
        {
            if (lightBurning)
            {
                lightBurningTime += Time.deltaTime;
                if (lightBurningTime >= flickersequence[currentFlickerIndex])
                {
                    lightBurningTime = 0;
                    lightBurning = false;
                    incrementIndexWrapping();
                }
            }
            else
            {
                lightOffTime += Time.deltaTime;
                if (lightOffTime >= flickersequence[currentFlickerIndex])
                {
                    lightOffTime = 0;
                    lightBurning = true;
                    incrementIndexWrapping();
                }
            }
        }
	}

    private void incrementIndexWrapping()
    {
        if (currentFlickerIndex == flickersequence.Count - 1)
        {
            currentFlickerIndex = 0;
        }
        else
        {
            currentFlickerIndex++;
        }
    }
}
