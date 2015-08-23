using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DoorScript : MonoBehaviour
{

    public Vector3 OpenOffset;
    public float CloseTime;
    public Transform MovingPart;

    // Use this for initialization
    void Start()
    {
        MovingPart.position += OpenOffset;
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

        StartCoroutine(animate(MovingPart.position, MovingPart.position - OpenOffset, CloseTime).GetEnumerator());

    }

    IEnumerable<YieldInstruction> animate(Vector3 start, Vector3 end, float time)
    {
        var timeLeft = time;
        while (timeLeft > 0)
        {
            MovingPart.position = Vector3.Lerp(end, start, timeLeft / time);
            yield return null;
            timeLeft -= Time.deltaTime;
        }
    }


}
