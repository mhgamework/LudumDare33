using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethodsRectTransform
{
    // -- PUBLIC

    // .. EXENSION_METHODS

    public static void MoveAnchoredPosition(this RectTransform rect_transform, Vector2 movement)
    {
        rect_transform.anchoredPosition = rect_transform.anchoredPosition + movement;
    }
    
    public static void AdjustSizeDelta(this RectTransform rect_transform, Vector2 adjustment)
    {
        rect_transform.sizeDelta = rect_transform.sizeDelta + adjustment;
    }

}