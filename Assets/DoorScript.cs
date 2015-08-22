using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DoorScript : MonoBehaviour
{

    public float ClosedAngle;
    public float TurnTime;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerExit(Collider other)
    {
        var componentInParent = other.GetComponentInParent<PreyWalkScript>();
        if (componentInParent.gameObject != GameStateManagerScript.Get.Prey.gameObject) return;
        if (other.gameObject.name != "Body") return;

        StartCoroutine(animateRotation(transform.rotation, Quaternion.AngleAxis(ClosedAngle, Vector3.up), TurnTime).GetEnumerator());

    }

    IEnumerable<YieldInstruction> animateRotation(Quaternion start, Quaternion end, float time)
    {
        var timeLeft = time;
        while (timeLeft > 0)
        {
            transform.rotation = Quaternion.Lerp(end, start, timeLeft / time);
            yield return null;
            timeLeft -= Time.deltaTime;
        }
    }


}
