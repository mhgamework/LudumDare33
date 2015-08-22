﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DeathCanvasControl : MonoBehaviour {

    [SerializeField]
    private UIFader uiFader;

    public float fadeDuration;
    public float stayDuration;


	// Use this for initialization
	void Start () {
        uiFader = this.GetComponent<UIFader>();
        uiFader.Fade(0, 0, EasingFunctions.TYPE.Out);
	}

    IEnumerable<YieldInstruction> fade()
    {
        uiFader.Fade(1, fadeDuration, EasingFunctions.TYPE.In);
        yield return new WaitForSeconds(fadeDuration + stayDuration);
        uiFader.Fade(0, stayDuration, EasingFunctions.TYPE.Out);
    }

	// Update is called once per frame
	void Update () {
	
	}

    public void triggerDeath()
    {
        StartCoroutine(fade().GetEnumerator());
    }
}
