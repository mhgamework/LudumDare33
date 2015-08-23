using UnityEngine;
using System.Collections;

public class CreditsCanvasControl : MonoBehaviour {

    [SerializeField]
    private UIFader uiFader;

    public GameStateManagerScript GameStateManager;

	// Use this for initialization
	void Start () {
        uiFader = this.GetComponent<UIFader>();
        uiFader.Fade(0, 0, EasingFunctions.TYPE.Out);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void triggerCredits()
    {
        uiFader.Fade(1, 0.1f, EasingFunctions.TYPE.In);
    }
}
