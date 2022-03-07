using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ColorPickerHelper
{
    public static Color GetPixelColor(Sprite sprite)
    {
        Texture2D texture = sprite.texture;
        Rect textureRect = sprite.textureRect;
//        Debug.LogError($"{transform.parent.name}, textureRect.center: {textureRect.center.x}, {textureRect.center.y}");
//        Debug.LogError($"texture: {texture.width} x {texture.height}");
        // Get pixel color
        return texture.GetPixel((int)textureRect.center.x, (int)textureRect.center.y);
    }
}
