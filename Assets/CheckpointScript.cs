using UnityEngine;
using System.Collections;

public class CheckpointScript : MonoBehaviour
{

    public GameStateManagerScript GameStateManager;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != GameStateManager.Player.gameObject) return;

        GameStateManager.TakeCheckpoint();
        GameStateManager.CheckpointCanvas.HitCheckpoint();
    }
}
