using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class EndGameCanvasControl : MonoBehaviour
{
    [SerializeField]
    private UIFader uiFader;

    private bool isVisible;
    private bool gameEnded;


	// Use this for initialization
	void Start ()
	{
	    uiFader = this.GetComponent<UIFader>();
        uiFader.Fade(0,0,EasingFunctions.TYPE.Out);
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKey(KeyCode.A))
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
        gameEnded = true;
        Debug.Log("The game has ended.");
    }


    public void showEndGameCanvas()
    {
        uiFader.Fade(1,0.2f,EasingFunctions.TYPE.In);
        isVisible = true;
    }
}
