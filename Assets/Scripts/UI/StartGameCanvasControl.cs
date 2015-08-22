using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class StartGameCanvasControl : MonoBehaviour
{
    [SerializeField]
    private UIFader uiFader;

    private bool isVisible = true;

    private bool gameStarted;


	// Use this for initialization
	void Start ()
	{
	    uiFader = this.GetComponent<UIFader>();
        uiFader.Fade(1,0,EasingFunctions.TYPE.In);
	}
	
	// Update is called once per frame
	void Update () {
	    if (isVisible)
	    {
	        if (Input.GetKey(KeyCode.S) && !gameStarted)
	        {
                hideStartGameCanvas();
	            startGame();
	        }
	    }
	}

    public void startGame()
    {
        gameStarted = true;
        Debug.Log("The game has started.");
    }


    public void hideStartGameCanvas()
    {
        uiFader.Fade(0,0.2f,EasingFunctions.TYPE.Out);
        isVisible = false;
    }
}
