using UnityEngine;
using System.Collections;

public class CheckpointScript : MonoBehaviour
{
    public GameObject WayPointToRestore = null;

    public GameStateManagerScript GameStateManager { get { return GameStateManagerScript.Get; } }
    private bool visited = false;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != GameStateManager.Player.gameObject) return;
        if (visited) return;
        visited = true;
        GameStateManager.TakeCheckpoint(WayPointToRestore);
        GameStateManager.CheckpointCanvas.HitCheckpoint();
        GetComponent<AudioSource>().Play();
    }
}
