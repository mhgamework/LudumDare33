using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class DetectPlayerScript : MonoBehaviour {

    public FirstPersonController Player;
    public GameStateManagerScript GameState;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != Player.gameObject) return;

        GameState.RestoreCheckpoint();
    }
}
