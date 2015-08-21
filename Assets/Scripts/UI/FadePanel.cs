using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UIFader))]
public class FadePanel : AGuiElement
{
    // -- PUBLIC

    // .. OPERATIONS

    public override void Show(float animation_time = 0)
    {
        TryInitialize();

        if (IsVisible)
            return;

        IsVisible = true;
        gameObject.SetActive(true);
        StopCoroutine("UpdateShowHide");
        object[] args = new object[] { true, animation_time };
        StartCoroutine("UpdateShowHide", args);
    }

    public override void Hide(float animation_time = 0)
    {
        TryInitialize();

        if (!IsVisible)
            return;

        IsVisible = false;
        gameObject.SetActive(true);
        StopCoroutine("UpdateShowHide");
        object[] args = new object[] { false, animation_time };
        StartCoroutine("UpdateShowHide", args);
    }

    public override bool IsHidden()
    {
        TryInitialize();
        return !IsVisible;
    }

    // -- PRIVATE

    // .. OPERATIONS

    void Start()
    {
        TryInitialize();
    }

    private void TryInitialize()
    {
        if (IsInitialized)
            return;

        IsInitialized = true;

        Fader = GetComponent<UIFader>();

        IsVisible = !VisibleOnStart;
        if(VisibleOnStart)
            Show();
        else
            Hide();
    }

    // .. COROUTINES

    IEnumerator UpdateShowHide(object[] args)
    {
        bool show = (bool)args[0];
        float animation_time = (float)args[1];

        if (show)
            Fader.Fade(1, animation_time, FadeInEasingType);
        else
            Fader.Fade(0, animation_time, FadeOutEasingType);

        yield return new WaitForSeconds(animation_time);

        gameObject.SetActive(show);
    }

    // .. ATTRIBUTES

    [SerializeField]
    private bool VisibleOnStart = true;
    [SerializeField]
    private EasingFunctions.TYPE FadeInEasingType = EasingFunctions.TYPE.Regular;
    [SerializeField]
    private EasingFunctions.TYPE FadeOutEasingType = EasingFunctions.TYPE.Regular;

    private bool IsVisible;
    private UIFader Fader;
    private bool IsInitialized;

}
