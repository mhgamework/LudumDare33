using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
public class WaterTrigger : MonoBehaviour
{
    private BoxCollider _boxCollider;

    void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _boxCollider.isTrigger = true;
    }

    public void OnTriggerEnter(Collider other)
    {
        //Debug.Log("(WATERTRIGGER) enter");
        if (GameStateManagerScript.Get != null)
            GameStateManagerScript.Get.Player.GetComponent<PlayerScript>().OnWaterEnter();
    }

    public void OnTriggerExit(Collider other)
    {
        //Debug.Log("(WATERTRIGGER) exit");
        if (GameStateManagerScript.Get != null)
            GameStateManagerScript.Get.Player.GetComponent<PlayerScript>().OnWaterExit();
    }
}
