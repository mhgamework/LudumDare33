using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TitleScreenManagerScript : MonoBehaviour
{

    public Canvas BlackOverlay;


	// Use this for initialization
	void Start () {
	
	}

    private bool starting = false;

	// Update is called once per frame
	void Update () {
        if (starting) return;
	    if (Input.anyKey)
	    {
	        starting = true;
	        StartCoroutine(StartLoadLevel().GetEnumerator());
	    }
	}

    IEnumerable<YieldInstruction> StartLoadLevel()
    {
        BlackOverlay.GetComponent<UIFader>().Fade(1, 3, EasingFunctions.TYPE.Out);
        yield return new WaitForSeconds(3);
        Application.LoadLevel("main");

    }
}
