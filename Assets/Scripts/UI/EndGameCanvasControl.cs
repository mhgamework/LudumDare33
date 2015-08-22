using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class EndGameCanvasControl : MonoBehaviour
{
    [SerializeField]
    private UIFader uiFader;

    public GameStateManagerScript GameStateManager;


    private bool isVisible;
    private bool gameEnded;


    // Use this for initialization
    void Start()
    {
        uiFader = this.GetComponent<UIFader>();
        uiFader.Fade(0, 0, EasingFunctions.TYPE.Out);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isVisible && GameStateManager.Player.GetComponent<PlayerScript>().CanKillPrey())
        {
            showEndGameCanvas();
        }
        if (isVisible)
        {
            if (Input.GetKey(KeyCode.F) && !gameEnded)
            {
                endGame();
            }
        }
    }

    public void endGame()
    {
        GameStateManager.EndGame();
        gameEnded = true; // TODO
    }


    public void showEndGameCanvas()
    {
        uiFader.Fade(1, 0.2f, EasingFunctions.TYPE.In);
        isVisible = true;
    }
}
