using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DamageCanvasControl : MonoBehaviour {

    [SerializeField]
    private UIFader uiFader;

    public float fadeDuration;
    public float stayDuration;

    public bool showPermanent = false;

	// Use this for initialization
	void Start () {
        uiFader = this.GetComponent<UIFader>();
        uiFader.Fade(0, 0, EasingFunctions.TYPE.Out);

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private bool fading = false;

    IEnumerable<YieldInstruction> fade()
    {
        uiFader.Fade(1, fadeDuration, EasingFunctions.TYPE.In);
        yield return new WaitForSeconds(fadeDuration + stayDuration);
        uiFader.Fade(0, fadeDuration, EasingFunctions.TYPE.Out);
        yield return new WaitForSeconds(fadeDuration);
        fading = false;
    }

    public void blinkDamage()
    {
        if (showPermanent) return;
        if (fading) return;
        fading = true;
        StartCoroutine(fade().GetEnumerator());
    }


    public void ShowDamagePermanent()
    {
        showPermanent = true;
        uiFader.Fade(1, fadeDuration, EasingFunctions.TYPE.In);
        
    }

    public void ResetDamagePermanent()
    {
        showPermanent = false;
        uiFader.Fade(0, fadeDuration, EasingFunctions.TYPE.In);

    }

}
