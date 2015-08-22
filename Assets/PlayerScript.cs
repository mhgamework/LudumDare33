using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour
{

    public float PreyKillDistance = 5;
    public PreyWalkScript Prey;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public bool CanKillPrey()
    {
        return (Prey.transform.position - transform.position).magnitude < PreyKillDistance;
    }
}
