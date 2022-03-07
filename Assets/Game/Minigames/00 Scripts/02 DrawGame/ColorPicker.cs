using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class ColorPicker : MonoBehaviour
{
    public Color Color;

    public Button button;
    [SerializeField] Image _image;
    [SerializeField] TextMeshProUGUI txtNumber;
    [SerializeField] Image progress;
    [SerializeField] private Animator animator;
    public ReactiveProperty<bool> IsCompleted = new ReactiveProperty<bool>(false);
    public ReactiveProperty<bool> IsLocked = new ReactiveProperty<bool>(false);
    private void Start()
    {
//        IsCompleted.Value = false;
    }
    
    public void SetColor(Color color, int index)
    {
        _image.color = color;
        Color = color;
        txtNumber.text = (index + 1).ToString();
    }

    public void UpdateProgress(float percent)
    {
        progress.fillAmount = percent;
    }

    public void SetLockedState(bool isLock)
    {
        animator.SetBool("Locked", isLock);
    }

    public void ShowCompletedState()
    {
        animator.SetTrigger("Completed");
        IsCompleted.Value = true;
    }

    public void ShowSelectedState(bool selected)
    {
        if (IsLocked.Value || IsCompleted.Value)
            return;
        
        animator.SetBool("Selected", selected);
    }
    
}
