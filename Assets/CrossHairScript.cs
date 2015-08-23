using UnityEngine;
using System.Collections;

public class CrossHairScript : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        GameStateManagerScript.Get.PauseEvent.AddListener(() => GetComponent<Canvas>().enabled = false);
        GameStateManagerScript.Get.UnPauseEvent.AddListener(() => GetComponent<Canvas>().enabled = true);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
