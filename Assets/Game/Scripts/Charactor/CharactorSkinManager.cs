using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class CharactorSkinManager : MonoBehaviour
{
    [SerializeField] private CharactorDataChangeSkin[] _charactorDataChangeSkins;

    private OtherSkeletionAnimationController[] otherSkeletionAnimationController;

    public OtherSkeletionAnimationController[] OtherSkeletionAnimationController
    {
        get
        {
            if (this.otherSkeletionAnimationController == null)
            {
                this.otherSkeletionAnimationController = this._charactorDataChangeSkins[0].gameObject
                    .GetComponentsInChildren<OtherSkeletionAnimationController>();
            }

            return this.otherSkeletionAnimationController;
        }
        set { }
    }

    private CharactorDataChangeSkin _dataChangeSkin;

    public CharactorDataChangeSkin DataChangeSkin
    {
        get
        {
            if (this._dataChangeSkin == null)
            {
                this._dataChangeSkin =
                    this._charactorDataChangeSkins[0].gameObject.GetComponent<CharactorDataChangeSkin>();
            }

            return this._dataChangeSkin;
        }
        private set { this._dataChangeSkin = value; }
    }

    private CharactorSkeletonAnimationController charactorSkeletonAnimationController;

    public CharactorSkeletonAnimationController CharactorSkeletonAnimationController
    {
        get
        {
            if (this.charactorSkeletonAnimationController == null)
            {
                this.charactorSkeletonAnimationController = this._charactorDataChangeSkins[0].gameObject
                    .GetComponentInChildren<CharactorSkeletonAnimationController>();
            }

            return this.charactorSkeletonAnimationController;
        }
        private set { this.charactorSkeletonAnimationController = value; }
    }

    private FishingrodSkeletonAnimationController fishingrodSkeletonAnimationController;

    public FishingrodSkeletonAnimationController FishingrodSkeletonAnimationController
    {
        get
        {
            if (this.fishingrodSkeletonAnimationController == null)
            {
                this.fishingrodSkeletonAnimationController = this._charactorDataChangeSkins[0].gameObject
                    .GetComponentInChildren<FishingrodSkeletonAnimationController>();
            }

            return this.fishingrodSkeletonAnimationController;
        }
        private set { this.fishingrodSkeletonAnimationController = value; }
    }


    private bool inited = false;

    private CharactorSex _charactorSex;

    public CharactorSex CharactorSex
    {
        get
        {
            if (this._charactorSex == null)
            {
                this._charactorSex = this.GetComponent<CharactorSex>();
            }

            return this._charactorSex;
        }
    }

    private void Start()
    {
        this.SetOnStart();
    }

    // [Button]
    public void ConvertToJson()
    {
        // CharactorDataGeneratorInfomation charactorDataGeneratorInfomation = new CharactorDataGeneratorInfomation();
        //     string json = JsonUtility.ToJson(charactorDataGeneratorInfomation);
        //     Debug.Log(json);
    }

    [Button]
    public void SelectCharactor(ETYPECHARACTOR etypecharactor)
    {
        this.CharactorSex.Etypecharactor = etypecharactor;
        this.SetOnStart();
    }

    public void SetOnStart()
    {
        for (int i = 0; i < _charactorDataChangeSkins.Length; i++)
        {
            this._charactorDataChangeSkins[i].gameObject.SetActive(false);
        }

        switch (this.CharactorSex.Etypecharactor)
        {
            case ETYPECHARACTOR.BOY:
                this._charactorDataChangeSkins[0].gameObject.SetActive(true);
                this.DataChangeSkin = this._charactorDataChangeSkins[0].GetComponent<CharactorDataChangeSkin>();
                this.CharactorSkeletonAnimationController = this._charactorDataChangeSkins[0]
                    .GetComponentInChildren<CharactorSkeletonAnimationController>();
                this.OtherSkeletionAnimationController = this._charactorDataChangeSkins[0].gameObject
                    .GetComponentsInChildren<OtherSkeletionAnimationController>();
                this.FishingrodSkeletonAnimationController = this._charactorDataChangeSkins[0].gameObject
                    .GetComponentInChildren<FishingrodSkeletonAnimationController>();
                break;
            case ETYPECHARACTOR.GIRL:
                this._charactorDataChangeSkins[1].gameObject.SetActive(true);
                this.DataChangeSkin = this._charactorDataChangeSkins[1].GetComponent<CharactorDataChangeSkin>();
                this.CharactorSkeletonAnimationController = this._charactorDataChangeSkins[1]
                    .GetComponentInChildren<CharactorSkeletonAnimationController>();
                this.OtherSkeletionAnimationController = this._charactorDataChangeSkins[1].gameObject
                    .GetComponentsInChildren<OtherSkeletionAnimationController>();
                this.FishingrodSkeletonAnimationController = this._charactorDataChangeSkins[1].gameObject
                    .GetComponentInChildren<FishingrodSkeletonAnimationController>();
                break;
            case ETYPECHARACTOR.SPECIAL:
                this._charactorDataChangeSkins[2].gameObject.SetActive(true);
                this.DataChangeSkin = this._charactorDataChangeSkins[2].GetComponent<CharactorDataChangeSkin>();
                this.CharactorSkeletonAnimationController = this._charactorDataChangeSkins[2]
                    .GetComponentInChildren<CharactorSkeletonAnimationController>();
                this.OtherSkeletionAnimationController = this._charactorDataChangeSkins[2].gameObject
                    .GetComponentsInChildren<OtherSkeletionAnimationController>();
                this.FishingrodSkeletonAnimationController = this._charactorDataChangeSkins[2].gameObject
                    .GetComponentInChildren<FishingrodSkeletonAnimationController>();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        this.inited = true;
    }

    public void SetSkin(bool isChoosed, ECHARACTORDETAIL _echaractordetail, ItemStyle _itemStyle)
    {
        this.DataChangeSkin.SwapSpriteByECharactorDetail(isChoosed, _echaractordetail, _itemStyle);
    }
}