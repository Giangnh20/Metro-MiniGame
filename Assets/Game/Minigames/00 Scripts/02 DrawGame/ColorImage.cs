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
    [SerializeField] private TextMeshProUGUI txtNumber;
    [SerializeField] private Button button;
    public Button Button => button;

    private Material grayscaleMaterial;
    private static readonly int GrayscaleAmount = Shader.PropertyToID("_GrayscaleAmount");

    private void Awake()
    {
        //Color = ColorPickerHelper.GetPixelColor(image.sprite);
        grayscaleMaterial = Instantiate(image.material);
        image.material = grayscaleMaterial;
        
        if (image != null)
        {
            image.alphaHitTestMinimumThreshold = 0.001f;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
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
        
        ToWhiteColor();
    }

    public void Populate(int number, Color color, bool isLock)
    {
        this.Color = color;
        SetColorNumber(number);
    }
    
    public void ToWhiteColor()
    {
        Material mat = grayscaleMaterial;
        mat.SetFloat(GrayscaleAmount, 1f);
    }

    public void ToNormalColor()
    {
        Material mat = grayscaleMaterial;
        mat.SetFloat(GrayscaleAmount, 0f);
    }

    public void SetColorNumber(int index)
    {
        txtNumber.text = index.ToString();
    }
    
}
