using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Object = System.Object;

public class UIEditorItemPrefab : MonoBehaviour
{
    [SerializeField] public ItemStyle _itemStyle;
    [SerializeField] private Image ImgChoosed;
    [SerializeField] private Image _imageIcon;

    [SerializeField] private bool isChoosed;

    private GameObject mainCharactor;

    private GameObject MainCharactor
    {
        get
        {
            if (this.mainCharactor == null)
            {
                this.mainCharactor = GameObject.FindWithTag("Player");
            }

            return this.mainCharactor;
        }
    }

    public bool IsChoosed
    {
        get
        {
            bool result =
                PlayerPrefs.GetInt(
                    "KEY_" + this.MainCharactor.GetComponent<CharactorSex>().Etypecharactor.ToString() + "_" +
                    _itemStyle.ID,
                    0) == 0
                    ? false
                    : true;
            this.isChoosed = result;
            return this.isChoosed;
        }
        set
        {
            this.MainCharactor.GetComponent<CharactorSex>().Etypecharactor.ToString();
            Debug.Log(
                "KEY_" + this.MainCharactor.GetComponent<CharactorSex>().Etypecharactor.ToString() + "_" +
                _itemStyle.ID);
            PlayerPrefs.SetInt(
                "KEY_" + this.MainCharactor.GetComponent<CharactorSex>().Etypecharactor.ToString() + "_" +
                _itemStyle.ID,
                value == false ? 0 : 1);
            PlayerPrefs.Save();
            this.isChoosed = value;
        }
    }

    private Button _button;
    private System.Action<object> callbackCLick = null;
    [SerializeField] public ECHARACTORDETAIL _echaractordetail;
    private bool inited = false;

    private void Start()
    {
        this._button = this.GetComponent<Button>();
        this._button.onClick.AddListener(delegate { this.btn_Click(); });
    }

    private void btn_Click()
    {
        if (this.callbackCLick != null)
        {
            this.callbackCLick(this);
        }
    }

    public void SetOnStart(ECHARACTORDETAIL echaractordetail, ItemStyle itemStyle,
        System.Action<object> callback = null)
    {
        this._echaractordetail = echaractordetail;
        this.callbackCLick = callback;
        this._itemStyle = itemStyle;
        this._imageIcon.sprite = this._itemStyle.Sprite;
        this._imageIcon.SetNativeSize();
        Debug.Log(this.IsChoosed);
        this.ImgChoosed.gameObject.SetActive(this.IsChoosed);
        this.inited = true;
    }

    public void IsOnChoose(bool isChoosed)
    {
        this.IsChoosed = isChoosed;
        this.ImgChoosed.gameObject.SetActive(isChoosed);
    }
}