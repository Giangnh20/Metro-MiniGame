using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIEditorsSkinCanvas : MonoBehaviour
{
    private UIEditorCpnController[] uiEditorCpnControllers;

    [SerializeField] private TMP_Dropdown dropdownSex;
    [SerializeField] private TMP_Dropdown dropdownSkin;
    private GameObject MainCharactor;
    private bool inited = false;

    private void Start()
    {
        this.MainCharactor = GameObject.FindWithTag("Player");
        this.uiEditorCpnControllers = this.GetComponentsInChildren<UIEditorCpnController>();
    }

    public void LoadRefreshSkin(int value)
    {
        this.dropdownSkin.value = value;
    }

    //Ouput the new value of the Dropdown into Text
    public void DropdownSexValueChanged(int value)
    {
        CharactorSkinManager charactorSkinManager = this.MainCharactor.GetComponent<CharactorSkinManager>();
        Debug.Log(value);
        switch (value)
        {
            case 0:
                charactorSkinManager.SelectCharactor(ETYPECHARACTOR.BOY);

                break;
            case 1:
                charactorSkinManager.SelectCharactor(ETYPECHARACTOR.GIRL);
                break;
            case 2:
                break;
        }

        ESKINCOLOR eskincolor = charactorSkinManager.DataChangeSkin.CharactorDataGeneratorInfomationConvertToSprite
            .Eskincolor;
        switch (eskincolor)
        {
            case ESKINCOLOR.WHITE:
                LoadRefreshSkin(0);
                break;
            case ESKINCOLOR.YELLOW:
                LoadRefreshSkin(2);
                break;
            case ESKINCOLOR.BLACK:
                LoadRefreshSkin(1);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        for (int i = 0; i < this.uiEditorCpnControllers.Length; i++)
        {
            this.uiEditorCpnControllers[i].SetOnStart();
        }
    }

    public void DropdownSkinValueChanged(int value)
    {
        Debug.Log(value);
        switch (value)
        {
            case 0:
                this.MainCharactor.GetComponent<CharactorSkinManager>().DataChangeSkin
                    .LoadCharactorSpriteData(ESKINCOLOR.WHITE);
                break;
            case 1:
                this.MainCharactor.GetComponent<CharactorSkinManager>().DataChangeSkin
                    .LoadCharactorSpriteData(ESKINCOLOR.BLACK);
                break;
            case 2:
                this.MainCharactor.GetComponent<CharactorSkinManager>().DataChangeSkin
                    .LoadCharactorSpriteData(ESKINCOLOR.YELLOW);
                break;
        }
    }


    private void SetOnStart()
    {
        this.inited = true;
    }
}