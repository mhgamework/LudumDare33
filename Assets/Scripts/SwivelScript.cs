using UnityEngine;
using System.Collections;

public class SwivelScript : MonoBehaviour
{
    [SerializeField] private GameObject thingToSwivel = null;
    [SerializeField] private float swivelStartPosition = -30f;
    [SerializeField] private float swivelEndPosition = 30f;
    [SerializeField] private float swivelSpeed = 1f;

    private float currentAngle;


	// Use this for initialization
	void Start ()
	{
	    thingToSwivel.transform.localRotation = Quaternion.AngleAxis(swivelStartPosition,Vector3.forward);
	}
	
	// Update is called once per frame
	void Update ()
	{
	    var range = swivelEndPosition - swivelStartPosition;
        var rotationAngle = sawtooth(Time.timeSinceLevelLoad/range*swivelSpeed);
        //Debug.Log(rotationAngle);
	    rotationAngle = rotationAngle*range + swivelStartPosition;

        thingToSwivel.transform.localRotation = Quaternion.AngleAxis(rotationAngle, Vector3.forward);

	}

    float sawtooth(float x)
    {
        return Mathf.Abs((x%1)*2 - 1);
    }
}
