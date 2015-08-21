using UnityEngine;
using System.Collections;
using System.Linq;

public static class ExtensionMethodsTexture2D
{
    /// <summary>
    /// Floods this Texture2D with given color (EXPENSIVE OPERATION!)
    /// </summary>
    /// <param name="texture"></param>
    /// <param name="color"></param>
    public static void Fill(this Texture2D texture, Color color)
    {
        int width = texture.width;
        int height = texture.height;
        Color[] colors = Enumerable.Repeat(color, width * height).ToArray();
        texture.SetPixels(0, 0, width, height, colors);
        texture.Apply();
    }

}
