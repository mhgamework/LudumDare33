using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityStandardAssets.Characters.FirstPerson;

public class GameStateManagerScript : MonoBehaviour
{
    public static GameStateManagerScript Get { get; private set; }

    public StartGameCanvasControl StartGameCanvas;
    public EndGameCanvasControl EndGameCanvas;
    public CreditsCanvasControl CreditsCanvasControl;
    public CheckpointCanvasControl CheckpointCanvas;
    public DeathCanvasControl DeathCanvasControl;
    public DamageCanvasControl DamageCanvasControl;
    public LostCanvasControl LostCanvasControl;
    public FirstPersonController Player;
    public PlayerScript PlayerScript { get { return Player.GetComponent<PlayerScript>(); } }
    public bool SimulationEnabled { get; private set; }

    public PreyWalkScript Prey;

    public bool isEnded;
    public bool isLost;

    public class PauseEventHandler : UnityEvent
    {

    }
    public class UnPauseEventHandler : UnityEvent
    {

    }

    public UnPauseEventHandler UnPauseEvent = new UnPauseEventHandler();
    public PauseEventHandler PauseEvent = new PauseEventHandler();

    public GameStateManagerScript()
    {


    }

    public Camera PlayerCamera;

    // Use this for initialization
    void Start()
    {
        if (Get != null && Get != this) throw new InvalidOperationException("Singleton instance of GameStateManager is being overriden");

        Get = this;

        if (PlayerCamera != null)
            PlayerCamera.clearStencilAfterLightingPass = true;
        DisableGameSimulation();
        StartGameCanvas.Show();


        /*StartGameCanvas.gameObject.SetActive(true);
        EndGameCanvas.gameObject.SetActive(true);
        CheckpointCanvas.gameObject.SetActive(true);*/
    }

    private bool firstFrame = true;

    // Update is called once per frame
    void Update()
    {
        if (firstFrame)
        {
            DisableGameSimulation();
            StartGameCanvas.Show();
            firstFrame = false;
        }
    }

    public void DisableGameSimulation()
    {
        Player.enabled = false;
        //Prey.enabled = false;
        SimulationEnabled = false;
        PauseEvent.Invoke();

    }

    public void EnableGameSimulation()
    {
        Player.enabled = true;
        //Prey.enabled = true;
        EndGameCanvas.gameObject.SetActive(true);
        TakeCheckpoint(null);

        SimulationEnabled = true;
        UnPauseEvent.Invoke();
    }

    public void StartGame()
    {
        Application.LoadLevel("TitleScene");
    }

    IEnumerable<YieldInstruction> end()
    {
        CreditsCanvasControl.FadeToBlack();
        CreditsCanvasControl.PlayKillSound();
        yield return new WaitForSeconds(18);
        if (CreditsCanvasControl != null)
            CreditsCanvasControl.ShowCredits();
        yield return new WaitForSeconds(creditsDuration);
        CreditsCanvasControl.Hide();
        yield return new WaitForSeconds(4);
        StartGame();
    }

    public void EndGame()
    {
        DisableGameSimulation();
        isEnded = true;

        StartCoroutine(end().GetEnumerator());
    }

    private CheckpointData lastCheckpoint;
    public float creditsDuration;

    public void TakeCheckpoint(GameObject waypoint_to_restore)
    {
        int waypointIndex = GetWayPointIndex(waypoint_to_restore);
        int preyprogress = waypointIndex != -1 ? waypointIndex : Mathf.FloorToInt(Prey.PathProgression);

        lastCheckpoint = new CheckpointData()
        {
            PlayerPosition = Player.transform.position,
            PreyProgress = preyprogress,
            playerHealth = Player.GetComponent<PlayerScript>().health
        };
    }

    private int GetWayPointIndex(GameObject waypoint)
    {
        if (waypoint == null)
            return -1;

        var waypoint_transform = waypoint.GetComponent<Transform>();
        var parent = waypoint_transform.parent;

        for (int i = 0; i < parent.childCount; i++)
        {
            if (parent.GetChild(i) == waypoint_transform)
                return i;
        }
        return -1;
    }

    IEnumerable<YieldInstruction> die()
    {
        PlayerScript.SoundScript.PlayMonsterDeath();
        DisableGameSimulation();

        //Hack
        //EndGameCanvas.gameObject.SetActive(false);
        DamageCanvasControl.ShowDamagePermanent();

        DeathCanvasControl.Show();

        yield return new WaitForSeconds(DeathCanvasControl.fadeDuration + DeathCanvasControl.stayDuration);

        RestoreCheckpoint();

        EnableGameSimulation();

        DeathCanvasControl.Hide();
        DamageCanvasControl.ResetDamagePermanent();
    }

    public void PlayerDeath()
    {
        if (!SimulationEnabled) return;
        StartCoroutine(die().GetEnumerator());
    }

    private void RestoreCheckpoint()
    {
        isLost = false;
        isEnded = false;
        Player.transform.position = lastCheckpoint.PlayerPosition;
        Prey.PathProgression = lastCheckpoint.PreyProgress;
        Prey.updateOrientationUsingPathProgression();
        Player.transform.rotation = Quaternion.LookRotation(Prey.transform.position - Player.transform.position);
        Player.GetComponent<PlayerScript>().health = lastCheckpoint.playerHealth;
    }

    public struct CheckpointData
    {
        public Vector3 PlayerPosition;
        public float PreyProgress;
        public float playerHealth;
    }

    IEnumerable<YieldInstruction> lose()
    {
        if (LostCanvasControl == null)
            yield break;

        PlayerScript.SoundScript.PlayMonsterDeath();
        DisableGameSimulation();

        //Hack
        //EndGameCanvas.gameObject.SetActive(false);
        DamageCanvasControl.ShowDamagePermanent();

        LostCanvasControl.triggerLose();
        yield return new WaitForSeconds(LostCanvasControl.fadeDuration + LostCanvasControl.stayDuration);
        RestoreCheckpoint();

        EnableGameSimulation();

        LostCanvasControl.Hide();
        DamageCanvasControl.ResetDamagePermanent();
    }

    public void loseGame()
    {
        StartCoroutine(lose().GetEnumerator());
        isLost = true;
        Debug.Log("Lost game, prey got away.");
    }
}
