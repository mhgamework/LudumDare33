﻿using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.FirstPerson;

public class GameStateManagerScript : MonoBehaviour
{
    public static GameStateManagerScript Get { get; private set; }

    public StartGameCanvasControl StartGameCanvas;
    public EndGameCanvasControl EndGameCanvas;
    public CheckpointCanvasControl CheckpointCanvas;
    public DeathCanvasControl DeathCanvasControl;
    public DamageCanvasControl DamageCanvasControl;
    public FirstPersonController Player;
    public PreyWalkScript Prey;

    // Use this for initialization
    void Start()
    {
        if (Get != null && Get != this) throw new InvalidOperationException("Singleton instance of GameStateManager is being overriden");

        Get = this;

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
        EndGameCanvas.gameObject.SetActive(true);
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

    IEnumerable<YieldInstruction> die()
    {
        EndGameCanvas.gameObject.SetActive(false);
        DeathCanvasControl.triggerDeath();
        yield return new WaitForSeconds(DeathCanvasControl.fadeDuration + DeathCanvasControl.stayDuration);
        RestoreCheckpoint();
    }

    public void PlayerDeath()
    {
        StartCoroutine(die().GetEnumerator());
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
