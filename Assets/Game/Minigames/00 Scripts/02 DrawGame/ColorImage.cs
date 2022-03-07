using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class ColorImage : MonoBehaviour
{
    public Color Color;
    public ReactiveProperty<bool> IsOpen = new ReactiveProperty<bool>();

    [SerializeField] private Image image;
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI txtNumber;

    

    private Material tmpMaterial;

    private void Awake()
    {
        Color = ColorPickerHelper.GetPixelColor(image.sprite);
        tmpMaterial = Instantiate(image.material);
        image.material = tmpMaterial;
        
        if (image != null)
        {
            image.alphaHitTestMinimumThreshold = 0.001f;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
//        IsOpen = new ReactiveProperty<bool>();
        button?.onClick.AddListener(() =>
        {
//            Debug.LogError($", colorSprite: {ColorUtility.ToHtmlStringRGBA(Color)}, CurrentColor: {ColorUtility.ToHtmlStringRGBA(DrawGameManager.Instance.CurrentColor)}, ");
            if (Color == DrawGameManager.Instance.CurrentColor)
            {
                ToNormalColor();
                txtNumber.gameObject.SetActive(false);
                IsOpen.Value = true;
            }
        });
        
        
//        yield return new WaitUntil(()=> DrawGameManager.Instance.InitDone);
        ToWhiteColor();
    }
    
    public void ToWhiteColor()
    {
        Material mat = tmpMaterial;
        mat.shader = DrawGameManager.Instance.ShaderGUItext;
        image.color = Color.white;
    }

    public void ToNormalColor()
    {
        Material mat = tmpMaterial;
        mat.shader = DrawGameManager.Instance.ShaderSpritesDefault;;
        image.color = Color.white;
    }

    public void SetColorNumber(int index)
    {
        txtNumber.text = (index + 1).ToString();
    }
    
}
