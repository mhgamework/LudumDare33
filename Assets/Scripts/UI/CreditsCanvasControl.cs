using UnityEngine;
using System.Collections;

public class CreditsCanvasControl : MonoBehaviour
{

    [SerializeField]
    private UIFader uiFader;

    public GameStateManagerScript GameStateManager { get { return GameStateManagerScript.Get;} }

    // Use this for initialization
    void Start()
    {
        uiFader = this.GetComponent<UIFader>();
        uiFader.Fade(0, 0, EasingFunctions.TYPE.Out);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Show()
    {
        GameStateManager.PlayerScript.SoundScript.PlayMonsterChewing();

        uiFader.Fade(2, 0.5f, EasingFunctions.TYPE.In);
    }

    public void Hide()
    {
        uiFader.Fade(1, 4f, EasingFunctions.TYPE.In);
    }
}
