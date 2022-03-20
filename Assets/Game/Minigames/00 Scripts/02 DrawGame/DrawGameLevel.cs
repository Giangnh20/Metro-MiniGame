using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawGameLevel : MonoBehaviour
{
    
    [SerializeField] private List<ColorPicker> pickers;
    [SerializeField] private Image imgFullColor;
    [SerializeField] private Transform tranObjective;

    public List<ColorPicker> Pickers => pickers;
    public Transform Objective => tranObjective;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public async void ShowFullColorObject()
    {
        imgFullColor.gameObject.SetActive(true);
    }
}
