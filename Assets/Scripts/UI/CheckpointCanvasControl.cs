using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CheckpointCanvasControl : MonoBehaviour {

    [SerializeField]
    private UIFader uiFader;

    [SerializeField]
    private float duration;

    private bool checkPointTriggered;

	// Use this for initialization
	void Start () {
        uiFader = this.GetComponent<UIFader>();
        uiFader.Fade(0,0, EasingFunctions.TYPE.Out);
	}

    IEnumerable<YieldInstruction> fade()
    {
        uiFader.Fade(1, duration, EasingFunctions.TYPE.In);
        yield return new WaitForSeconds(duration + 0.3f);
        uiFader.Fade(0, duration, EasingFunctions.TYPE.Out);
        checkPointTriggered = false;
    }

	// Update is called once per frame
	void Update () {
	}

    public void HitCheckpoint()
    {
        if (checkPointTriggered) return;
        checkPointTriggered = true;
        StartCoroutine(fade().GetEnumerator());
    }
}
