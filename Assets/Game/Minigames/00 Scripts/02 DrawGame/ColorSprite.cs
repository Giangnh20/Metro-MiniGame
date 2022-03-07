using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class ColorSprite : MonoBehaviour
{
    public Color Color;
    private SpriteRenderer _spriteRenderer;
    private Shader shaderGUItext;
    private Shader shaderSpritesDefault;
 
    void Awake () {
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        shaderGUItext = Shader.Find("GUI/Text Shader");
        shaderSpritesDefault = Shader.Find("Sprites/Default"); // or whatever sprite shader is being used
    }

    private void Start()
    {
        Color = ColorPickerHelper.GetPixelColor(_spriteRenderer.sprite);
        ToWhiteColor();
    }

    
    // https://answers.unity.com/questions/609629/how-to-get-pixel-color-for-sprite-u43.html
    public void GetPixelColor()
    {
        var sprite = _spriteRenderer.sprite;
        Texture2D texture = sprite.texture;
        Rect textureRect = sprite.textureRect;
//        Debug.LogError($"{transform.parent.name}, textureRect.center: {textureRect.center.x}, {textureRect.center.y}");
//        Debug.LogError($"texture: {texture.width} x {texture.height}");
        // Get pixel color
        Color = texture.GetPixel((int)textureRect.center.x, (int)textureRect.center.y);
    }
    
    
    [Button]
    public void ToWhiteColor()
    {
        _spriteRenderer.material.shader = shaderGUItext;
        _spriteRenderer.color = Color.white;
    }

    [Button]
    public void ToNormalColor()
    {
        _spriteRenderer.material.shader = shaderSpritesDefault;
        _spriteRenderer.color = Color.white;
    }
    
}
