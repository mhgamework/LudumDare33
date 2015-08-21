using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(GameObjectAnimationScale))]
public class BounceButton : AGuiElement, IPointerClickHandler
{
    // -- PUBLIC

    // .. TYPES

    [Serializable]
    public class ButtonClickedEvent : UnityEvent { }

    // ..OPERATIONS

    public void OnPointerClick(PointerEventData eventData)
    {
        TryInitialize();
        StopCoroutine("UpdateClicked");
        StartCoroutine("UpdateClicked", 0.25f);
    }

    public override void Show(float animation_time)
    {
        TryInitialize();
        Hidden = false;
        gameObject.SetActive(true);
        StopCoroutine("UpdateShow");
        StopCoroutine("UpdateHide");
        StartCoroutine("UpdateShow", animation_time);
    }

    public override void Hide(float animation_time)
    {
        TryInitialize();
        Hidden = true;
        gameObject.SetActive(true);
        StopCoroutine("UpdateShow");
        StopCoroutine("UpdateHide");
        StartCoroutine("UpdateHide", animation_time);
    }

    public override bool IsHidden()
    {
        return Hidden;
    }

    // .. ATTRIBUTES

    public ButtonClickedEvent onClick;


    // -- PRIVATE

    // .. OPERATIONS

    void Start()
    {
        TryInitialize();
    }

    private void TryInitialize()
    {
        if (ScaleAnimator != null)
            return;

        ScaleAnimator = GetComponent<GameObjectAnimationScale>();
    }

    // .. COROUTINES

    private IEnumerator UpdateClicked(float animation_time)
    {
        onClick.Invoke();

        float grow_time = animation_time * 0.5f;
        float shrink_time = animation_time * 0.5f;
        Transform this_tranform = GetComponent<Transform>();

        ScaleAnimator.StartAnimation(EasingFunctions.TYPE.BackInCubic, this_tranform.localScale, Vector3.one * BounceFactor, grow_time);
        yield return new WaitForSeconds(grow_time);
        ScaleAnimator.StartAnimation(EasingFunctions.TYPE.Out, this_tranform.localScale, Vector3.one, shrink_time);
        yield return new WaitForSeconds(shrink_time);
    }

    private IEnumerator UpdateShow(float animation_time)
    {
        Transform this_tranform = GetComponent<Transform>();
        ScaleAnimator.StartAnimation(ShowEasingType, this_tranform.localScale, Vector3.one, animation_time);
        yield return new WaitForSeconds(animation_time);
    }

    private IEnumerator UpdateHide(float animation_time)
    {
        Transform this_tranform = GetComponent<Transform>();
        //ScaleAnimator.StartAnimation(EasingFunctions.TYPE.BackInCubic, this_tranform.localScale, Vector3.zero, animation_time);
        ScaleAnimator.StartAnimation(HideEasingType, this_tranform.localScale, Vector3.zero, animation_time);
        yield return new WaitForSeconds(animation_time);
        gameObject.SetActive(false);
    }

    // .. ATTRIBUTES

    [SerializeField]
    private float BounceFactor = 1.25f;

    [SerializeField]
    private EasingFunctions.TYPE ShowEasingType = EasingFunctions.TYPE.OutElastic;
    [SerializeField]
    private EasingFunctions.TYPE HideEasingType = EasingFunctions.TYPE.In;

    private GameObjectAnimationScale ScaleAnimator;
    private bool Hidden;
}
