using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class StartGameCanvasControl : MonoBehaviour
{
    [SerializeField]
    private UIFader uiFader;

    public UIFader textFader;


    private bool isVisible = false;

    public GameStateManagerScript GameStateManager { get { return GameStateManagerScript.Get; } }


    // Use this for initialization
    void Start()
    {

    }

    IEnumerable<YieldInstruction> simulate()
    {
        textFader.Fade(1, 1, EasingFunctions.TYPE.In);
        yield return new WaitForSeconds(1);
        while (!Input.GetKey(KeyCode.S)) yield return null;
        uiFader.Fade(0, 3f, EasingFunctions.TYPE.Out);
        GameStateManager.EnableGameSimulation();
        GameStateManager.PlayerScript.SoundScript.PlayDinnerTime();
        isVisible = false;

    }

    public void Show()
    {
        if (isVisible) return;
        uiFader.Fade(1, 0f, EasingFunctions.TYPE.Out);
        textFader.Fade(0, 0f, EasingFunctions.TYPE.Out);

        isVisible = true;
        StartCoroutine(simulate().GetEnumerator());
    }
}
