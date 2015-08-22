using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class DetectPlayerScript : MonoBehaviour {

    public FirstPersonController Player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != Player.gameObject) return;

        Debug.Log("Game over!");
    }
}
