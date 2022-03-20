using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawGameLevel : MonoBehaviour
{
    
    [SerializeField] private List<ColorPicker> pickers;
    [SerializeField] private Image imgFullColor;

    public List<ColorPicker> Pickers => pickers;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public async void ShowFullColorObject()
    {
        imgFullColor.gameObject.SetActive(true);
    }
}
