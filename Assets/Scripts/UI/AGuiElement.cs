using UnityEngine;
using System.Collections;

[RequireComponent(typeof(RectTransform))]
public abstract class AGuiElement : MonoBehaviour
{
    public abstract void Show(float animation_time = 0f);
    public abstract void Hide(float animation_time = 0f);
    public abstract bool IsHidden();
}
