using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class GameStateManagerScript : MonoBehaviour
{
    public StartGameCanvasControl StartGameCanvas;
    public EndGameCanvasControl EndGameCanvas;
    public CheckpointCanvasControl CheckpointCanvas;
    public DeathCanvasControl DeathCanvasControl;
    public FirstPersonController Player;
    public PreyWalkScript Prey;

    // Use this for initialization
    void Start()
    {
        DisableGame();
        StartGameCanvas.gameObject.SetActive(true);
        EndGameCanvas.gameObject.SetActive(true);
        CheckpointCanvas.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DisableGame()
    {
        Player.enabled = false;
        Prey.enabled = false;
    }

    public void StartGame()
    {
        Player.enabled = true;
        Prey.enabled = true;
        TakeCheckpoint();
    }

    public void EndGame()
    {
        Debug.Log("The game has ended.");
    }

    private CheckpointData lastCheckpoint;

    public void TakeCheckpoint()
    {
        lastCheckpoint = new CheckpointData()
        {
            PlayerPosition = Player.transform.position,
            PreyProgress = Prey.PathProgression,
            playerHealth = Player.GetComponent<PlayerScript>().health
        };
    }

    public void PlayerDeath()
    {
        DeathCanvasControl.triggerDeath();
        RestoreCheckpoint();
    }

    private void RestoreCheckpoint()
    {
        Player.transform.position = lastCheckpoint.PlayerPosition;
        Prey.PathProgression = lastCheckpoint.PreyProgress;
        Player.GetComponent<PlayerScript>().health = lastCheckpoint.playerHealth;
    }

    public struct CheckpointData
    {
        public Vector3 PlayerPosition;
        public float PreyProgress;
        public float playerHealth;
    }
}
