using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class ColorPicker : MonoBehaviour
{
    public Color Color;
    public int Number;

    public Button button;
    [SerializeField] Image _image;
    [SerializeField] TextMeshProUGUI txtNumber;
    [SerializeField] Image progress;
    [SerializeField] private Animator animator;
    public ReactiveProperty<bool> IsCompleted = new ReactiveProperty<bool>(false);
    public ReactiveProperty<bool> IsLocked = new ReactiveProperty<bool>(false);

    [SerializeField] private Button btnUnlock;
    [SerializeField] private List<ColorImage> listImages;
    


    private void Start()
    {
        btnUnlock?.onClick.AddListener(BtnUnlockClicked);
    }

    public void Populate(bool isLocked)
    {
        IsLocked.Value = isLocked;
        txtNumber.text = Number.ToString();
        SetLockedState(isLocked);
        
        int totalImages = listImages.Count;
        for (int i = 0; i < totalImages; i++)
        {
            var imgElement = listImages[i];
            imgElement.Populate(Number, Color, isLocked);
        }

        listImages.Select(x => x.IsOpen).CombineLatest().Subscribe(opened =>
        {
            int successCount = opened.Count(x => x); 
            UpdateProgress((float) successCount / totalImages);
            if (successCount == totalImages)
                ShowCompletedState();
        });
    }
    
//    public void SetColor(Color color, int index)
//    {
//        _image.color = color;
//        Color = color;
//        txtNumber.text = (index + 1).ToString();
//    }

    private void UpdateProgress(float percent)
    {
        progress.DOFillAmount(percent, 0.1f);
    }

    private void SetLockedState(bool isLock)
    {
        animator.SetBool("Locked", isLock);
        IsLocked.Value = isLock;
    }

    private void ShowCompletedState()
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

    public void ShowBuyUnlock(bool isShow)
    {
        btnUnlock.transform.DOScale(isShow ? Vector3.one : Vector3.zero, 0.05f);
    }

    private void BtnUnlockClicked()
    {
        SetLockedState(false);
        ShowBuyUnlock(false);
    }
    
}
