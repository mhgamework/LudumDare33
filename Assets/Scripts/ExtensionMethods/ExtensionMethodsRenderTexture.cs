using UnityEngine;
using System.Collections;

public static class ExtensionMethodsRenderTexture {

    /// <summary>
    /// Bakes this RenderTexture to a Texture2D (EXPENSIVE OPERATION!)
    /// </summary>
    /// <param name="render_texture"></param>
    /// <returns></returns>
    public static Texture2D ToTexture2D(this RenderTexture render_texture)
    {
        int width = render_texture.width;
        int height = render_texture.height;
        var texture_2d = new Texture2D(width, height);

        var current_active = RenderTexture.active;
        RenderTexture.active = render_texture;

        texture_2d.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        texture_2d.Apply();

        RenderTexture.active = current_active;

        return texture_2d;
    }
}
