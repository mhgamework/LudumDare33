using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class EndGameCanvasControl : MonoBehaviour
{
    [SerializeField]
    private UIFader uiFader;

    public GameStateManagerScript GameStateManager { get { return GameStateManagerScript.Get; } }


    private bool isVisible;
    public float fadeDuration;
    public float stayDuration;


    // Use this for initialization
    void Start()
    {
        uiFader = this.GetComponent<UIFader>();
        uiFader.Fade(0, 0, EasingFunctions.TYPE.Out);

        GameStateManager.PauseEvent.AddListener(() => { uiFader.Fade(0, fadeDuration, EasingFunctions.TYPE.In); });
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameStateManager.SimulationEnabled) return;

        if (!isVisible && GameStateManager.Player.GetComponent<PlayerScript>().CanKillPrey())
        {
            showEndGameCanvas();
        }
        if (isVisible && !GameStateManager.Player.GetComponent<PlayerScript>().CanKillPrey() || GameStateManager.isEnded)
        {
            hideEndGameCanvas();
        }
        if (isVisible)
        {
            if (Input.GetKey(KeyCode.F))// && !gameEnded)
            {
                GameStateManager.EndGame();
                //gameEnded = true; // TODO
            }
        }
    }

    IEnumerable<YieldInstruction> fade()
    {
        uiFader.Fade(1, fadeDuration, EasingFunctions.TYPE.In);
        yield return new WaitForSeconds(fadeDuration + stayDuration);
        uiFader.Fade(0, fadeDuration, EasingFunctions.TYPE.Out);
        yield return new WaitForSeconds(fadeDuration);
        isVisible = false;
    }

    private void hideEndGameCanvas()
    {
        uiFader.Fade(0, 0.2f, EasingFunctions.TYPE.In);
        isVisible = false;
    }


    public void showEndGameCanvas()
    {
        if (isVisible)
        {
            return;
        }
        isVisible = true;
        StartCoroutine(fade().GetEnumerator());
    }

}
