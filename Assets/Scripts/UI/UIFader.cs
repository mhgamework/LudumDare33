using UnityEngine;
using System.Collections;

/// <summary>
/// Provides functionality for fading in/out a ui element (including all of its children).
/// </summary>
[RequireComponent(typeof(CanvasGroup))]
public class UIFader : MonoBehaviour
{
    // -- PUBLIC

    // .. OPERATIONS

    public void Fade(float target_alpha, float time, EasingFunctions.TYPE easing_type = EasingFunctions.TYPE.Regular)
    {
        StopCoroutine("UpdateFade");

        object[] args = new object[] { target_alpha, time, easing_type };
        StartCoroutine("UpdateFade", args);
    }

    // -- PRIVATE

    // .. OPERATIONS

    void Start()
    {
        CanvasGroup = GetComponent<CanvasGroup>();
    }

    // .. COROUTINES

    IEnumerator UpdateFade(object[] args)
    {
        float target_alpha = Mathf.Clamp01((float)args[0]);
        float animation_time = (float)args[1];
        EasingFunctions.TYPE easing_type = (EasingFunctions.TYPE)args[2];

        if (CanvasGroup == null)
            CanvasGroup = GetComponent<CanvasGroup>();

        float start_alpha = CanvasGroup.alpha;
        float elapsed = 0f;

        while (elapsed < animation_time)
        {
            CanvasGroup.alpha = EasingFunctions.Ease(easing_type, elapsed / animation_time, start_alpha, target_alpha);
            elapsed += Time.deltaTime;
            yield return null;
        }

        CanvasGroup.alpha = target_alpha;
    }

    // .. ATTRIBUTES

    private CanvasGroup CanvasGroup;
}
