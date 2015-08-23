using UnityEngine;
using System.Collections;

public class CreditsCanvasControl : MonoBehaviour
{

    [SerializeField]
    private UIFader uiFader;
    [SerializeField]
    private UIFader textFader;

    [SerializeField] private AudioSource endSound;

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

    public void ShowCredits()
    {
        GameStateManager.PlayerScript.SoundScript.PlayMonsterChewing();

        textFader.Fade(1, 2, EasingFunctions.TYPE.In);
    }

    public void Hide()
    {
        uiFader.Fade(0, 4f, EasingFunctions.TYPE.In);
    }

    public void PlayKillSound()
    {
        endSound.Play();
    }

    public void FadeToBlack()
    {
        textFader.Fade(0, 0, EasingFunctions.TYPE.Out);
        uiFader.Fade(1, 0.3f, EasingFunctions.TYPE.In);
    }
}
