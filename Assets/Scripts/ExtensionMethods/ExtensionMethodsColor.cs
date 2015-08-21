using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethodsColor
{
    // -- PUBLIC

    // .. EXENSION_METHODS

    /// <summary>
    /// Adjust the alpha of this color.
    /// </summary>
    /// <param name="base_color"></param>
    /// <param name="alpha"></param>
    /// <returns></returns>
    public static Color CloneAdjustedAlpha(this Color base_color, float alpha)
    {
        Color color;

        color = base_color;
        color.a = alpha;

        return color;
    }

    // -- PRIVATE

    // .. FUNCTIONS
}