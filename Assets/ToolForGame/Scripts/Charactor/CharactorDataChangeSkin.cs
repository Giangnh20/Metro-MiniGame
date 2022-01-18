using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class CharactorDataChangeSkin : MonoBehaviour
{
    [SerializeField]
    private CharactorDataGeneratorInfomationConvertToSprite _charactorDataGeneratorInfomationConvertToSprite;

    public CharactorDataGeneratorInfomationConvertToSprite CharactorDataGeneratorInfomationConvertToSprite
    {
        get { return this._charactorDataGeneratorInfomationConvertToSprite; }
    }


    [SerializeField] private CharactorDataGeneratorInfomation[] _charactorDataGeneratorInfomation;

    private CharactorSpriteController[] _charactorSpriteControllers;

    public CharactorSpriteController[] CharactorSpriteControllers
    {
        get
        {
            if (this._charactorSpriteControllers == null)
            {
                this._charactorSpriteControllers = this.GetComponentsInChildren<CharactorSpriteController>();
            }

            return this._charactorSpriteControllers;
        }
    }

    private CharactorSkinManager charactorSkinManager;

    public CharactorSkinManager CharactorSkinManager
    {
        get
        {
            if (this.charactorSkinManager == null)
            {
                this.charactorSkinManager = this.GetComponentInParent<CharactorSkinManager>();
            }

            return this.charactorSkinManager;
        }
    }

    private bool inited = false;

    private CharactorVehicleController charactorVehicleController = null;

    private void Start()
    {
        this.charactorVehicleController = this.GetComponentInParent<CharactorVehicleController>();
        this.inited = true;
    }

    public void SetOnStart(CharactorDataGeneratorInfomation charactorDataGeneratorInfomation)
    {
        this.inited = true;
    }

    [Button]
    public void LoadCharactorSpriteData(ESKINCOLOR eskincolor)
    {
        var CharactorConvertSprite = new CharactorDataGeneratorInfomationConvertToSprite();
        CharactorConvertSprite.BodyComponent = new Sprite[6];
        CharactorConvertSprite.TopComponent = new Sprite[2];
        CharactorConvertSprite.BottomComponent = new Sprite[2];
        CharactorConvertSprite.Shoes = new Sprite[2];
        this._charactorDataGeneratorInfomationConvertToSprite = CharactorConvertSprite;
        switch (eskincolor)
        {
            case ESKINCOLOR.WHITE:
                this.ConvertStringDataToSpriteData(0);
                break;
            case ESKINCOLOR.BLACK:
                this.ConvertStringDataToSpriteData(1);
                break;
            case ESKINCOLOR.YELLOW:
                this.ConvertStringDataToSpriteData(2);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(eskincolor), eskincolor, null);
        }

        this.LoadCharactorDataGeneratorSpriteToCharactor(this._charactorDataGeneratorInfomationConvertToSprite);
    }

    public void ConvertStringDataToSpriteData(int idexSkinColor)
    {
        this._charactorDataGeneratorInfomationConvertToSprite.Etypecharactor =
            this._charactorDataGeneratorInfomation[idexSkinColor].Etypecharactor;
        this._charactorDataGeneratorInfomationConvertToSprite.Eskincolor =
            this._charactorDataGeneratorInfomation[idexSkinColor].Eskincolor;
        //body main
        if (!this._charactorDataGeneratorInfomation[idexSkinColor].Body.Equals(String.Empty))
        {
            this._charactorDataGeneratorInfomationConvertToSprite.Body = DataCharactorManagers.Instance
                .LoadItemStyleByID(
                    this._charactorDataGeneratorInfomationConvertToSprite.Etypecharactor,
                    ECHARACTORDETAIL.BODY, this._charactorDataGeneratorInfomation[idexSkinColor].Body).Sprite;
        }

        //Body cpn
        if (!this._charactorDataGeneratorInfomation[idexSkinColor].Body.Equals(String.Empty))
        {
            ItemChildDetail[] itemAnimation = DataCharactorManagers.Instance.LoadSpriteAnimationByID(
                this._charactorDataGeneratorInfomationConvertToSprite.Etypecharactor, ECHARACTORDETAIL.BODY,
                this._charactorDataGeneratorInfomation[idexSkinColor].Body);
            this._charactorDataGeneratorInfomationConvertToSprite.BodyComponent = new Sprite[itemAnimation.Length];
            for (int i = 0; i < itemAnimation.Length; i++)
            {
                this._charactorDataGeneratorInfomationConvertToSprite.BodyComponent[i] = itemAnimation[i].Sprite;
            }
        }

        //Face
        if (!this._charactorDataGeneratorInfomation[idexSkinColor].Face.Equals(String.Empty))
        {
            this._charactorDataGeneratorInfomationConvertToSprite.Face = DataCharactorManagers.Instance
                .LoadItemStyleByID(
                    this._charactorDataGeneratorInfomationConvertToSprite.Etypecharactor, ECHARACTORDETAIL.Face,
                    this._charactorDataGeneratorInfomation[idexSkinColor].Face).Sprite;
        }

        //Hair
        if (!this._charactorDataGeneratorInfomation[idexSkinColor].Hair.Equals(String.Empty))
        {
            this._charactorDataGeneratorInfomationConvertToSprite.Hair = DataCharactorManagers.Instance
                .LoadItemStyleByID(
                    this._charactorDataGeneratorInfomationConvertToSprite.Etypecharactor, ECHARACTORDETAIL.HAIR,
                    this._charactorDataGeneratorInfomation[idexSkinColor].Hair).Sprite;
        }

        //Long Hair
        if (!this._charactorDataGeneratorInfomation[idexSkinColor].HairLong.Equals(String.Empty))
        {
            this._charactorDataGeneratorInfomationConvertToSprite.HairLong = DataCharactorManagers.Instance
                .LoadItemStyleByID(
                    this._charactorDataGeneratorInfomationConvertToSprite.Etypecharactor, ECHARACTORDETAIL.HAIRLONG,
                    this._charactorDataGeneratorInfomation[idexSkinColor].HairLong).Sprite;
        }

        //Top
        if (!this._charactorDataGeneratorInfomation[idexSkinColor].Top.Equals(String.Empty))
        {
            this._charactorDataGeneratorInfomationConvertToSprite.Top = DataCharactorManagers.Instance
                .LoadItemStyleByID(
                    this._charactorDataGeneratorInfomationConvertToSprite.Etypecharactor, ECHARACTORDETAIL.TOP,
                    this._charactorDataGeneratorInfomation[idexSkinColor].Top).Sprite;
            //TopCpn
            ItemChildDetail[] itemAnimation2 = DataCharactorManagers.Instance.LoadSpriteAnimationByID(
                this._charactorDataGeneratorInfomationConvertToSprite.Etypecharactor, ECHARACTORDETAIL.TOP,
                this._charactorDataGeneratorInfomation[idexSkinColor].Top);
            this._charactorDataGeneratorInfomationConvertToSprite.TopComponent = new Sprite[itemAnimation2.Length];
            for (int i = 0; i < itemAnimation2.Length; i++)
            {
                this._charactorDataGeneratorInfomationConvertToSprite.TopComponent[i] = itemAnimation2[i].Sprite;
            }
        }

        //Bottom
        if (!this._charactorDataGeneratorInfomation[idexSkinColor].Bottom.Equals(String.Empty))
        {
            this._charactorDataGeneratorInfomationConvertToSprite.Bottom = DataCharactorManagers.Instance
                .LoadItemStyleByID(
                    this._charactorDataGeneratorInfomationConvertToSprite.Etypecharactor, ECHARACTORDETAIL.BOTTOM,
                    this._charactorDataGeneratorInfomation[idexSkinColor].Bottom).Sprite;
            //BottomCpn
            ItemChildDetail[] itemAnimation4 = DataCharactorManagers.Instance.LoadSpriteAnimationByID(
                this._charactorDataGeneratorInfomationConvertToSprite.Etypecharactor, ECHARACTORDETAIL.BOTTOM,
                this._charactorDataGeneratorInfomation[idexSkinColor].Bottom);
            this._charactorDataGeneratorInfomationConvertToSprite.BottomComponent = new Sprite[itemAnimation4.Length];
            for (int i = 0; i < itemAnimation4.Length; i++)
            {
                this._charactorDataGeneratorInfomationConvertToSprite.BottomComponent[i] = itemAnimation4[i].Sprite;
            }
        }

        //Shoes
        if (!this._charactorDataGeneratorInfomation[idexSkinColor].shoes.Equals(String.Empty))
        {
            ItemChildDetail[] itemAnimation3 = DataCharactorManagers.Instance.LoadSpriteAnimationByID(
                this._charactorDataGeneratorInfomationConvertToSprite.Etypecharactor, ECHARACTORDETAIL.Shoes,
                this._charactorDataGeneratorInfomation[idexSkinColor].shoes);
            this._charactorDataGeneratorInfomationConvertToSprite.Shoes = new Sprite[itemAnimation3.Length];
            for (int i = 0; i < itemAnimation3.Length; i++)
            {
                this._charactorDataGeneratorInfomationConvertToSprite.Shoes[i] = itemAnimation3[i].Sprite;
            }
        }

        //Accessories
        if (!this._charactorDataGeneratorInfomation[idexSkinColor].Accessories.Equals(String.Empty))
        {
            this._charactorDataGeneratorInfomationConvertToSprite.Accessories = DataCharactorManagers.Instance
                .LoadItemStyleByID(
                    this._charactorDataGeneratorInfomationConvertToSprite.Etypecharactor,
                    ECHARACTORDETAIL.ACCESSOIRES_Face,
                    this._charactorDataGeneratorInfomation[idexSkinColor].Accessories).Sprite;
        }

        //Vehicle
        if (!this._charactorDataGeneratorInfomation[idexSkinColor].Vehicle.Equals(String.Empty))
        {
            this._charactorDataGeneratorInfomationConvertToSprite.Vehicle = DataCharactorManagers.Instance
                .LoadItemStyleByID(
                    this._charactorDataGeneratorInfomationConvertToSprite.Etypecharactor, ECHARACTORDETAIL.VEHICLE,
                    this._charactorDataGeneratorInfomation[idexSkinColor].Vehicle).Sprite;
        }

        //Wing
        if (!this._charactorDataGeneratorInfomation[idexSkinColor].wings.Equals(String.Empty))
        {
            this._charactorDataGeneratorInfomationConvertToSprite.Wing = DataCharactorManagers.Instance
                .LoadItemStyleByID(
                    this._charactorDataGeneratorInfomationConvertToSprite.Etypecharactor, ECHARACTORDETAIL.Wings,
                    this._charactorDataGeneratorInfomation[idexSkinColor].wings).Sprite;
        }

        //Tail
        if (!this._charactorDataGeneratorInfomation[idexSkinColor].tail.Equals(String.Empty))
        {
            this._charactorDataGeneratorInfomationConvertToSprite.Tail = DataCharactorManagers.Instance
                .LoadItemStyleByID(
                    this._charactorDataGeneratorInfomationConvertToSprite.Etypecharactor, ECHARACTORDETAIL.Tail,
                    this._charactorDataGeneratorInfomation[idexSkinColor].tail).Sprite;
        }
    }

    public void LoadCharactorDataGeneratorSpriteToCharactor(
        CharactorDataGeneratorInfomationConvertToSprite charactorDataGeneratorInfomationConvertToSprite)
    {
        this.SetSpriteByEcharactorCpn(ESKINCHARACTORCPN.Body_Top_Core,
            charactorDataGeneratorInfomationConvertToSprite.BodyComponent[0]);
        this.SetSpriteByEcharactorCpn(ESKINCHARACTORCPN.HeadCore,
            charactorDataGeneratorInfomationConvertToSprite.BodyComponent[1]);
        this.SetSpriteByEcharactorCpn(ESKINCHARACTORCPN.Food_L_Core,
            charactorDataGeneratorInfomationConvertToSprite.BodyComponent[2]);
        this.SetSpriteByEcharactorCpn(ESKINCHARACTORCPN.Food_R_Core,
            charactorDataGeneratorInfomationConvertToSprite.BodyComponent[2]);
        this.SetSpriteByEcharactorCpn(ESKINCHARACTORCPN.Hand_L_Core,
            charactorDataGeneratorInfomationConvertToSprite.BodyComponent[3]);
        this.SetSpriteByEcharactorCpn(ESKINCHARACTORCPN.Hand_R_Core,
            charactorDataGeneratorInfomationConvertToSprite.BodyComponent[4]);
        this.SetSpriteByEcharactorCpn(ESKINCHARACTORCPN.Leg_L_Core,
            charactorDataGeneratorInfomationConvertToSprite.BodyComponent[5]);
        this.SetSpriteByEcharactorCpn(ESKINCHARACTORCPN.Leg_R_Core,
            charactorDataGeneratorInfomationConvertToSprite.BodyComponent[5]);
        this.SetSpriteByEcharactorCpn(ESKINCHARACTORCPN.Face, charactorDataGeneratorInfomationConvertToSprite.Face);
        this.SetSpriteByEcharactorCpn(ESKINCHARACTORCPN.Hair, charactorDataGeneratorInfomationConvertToSprite.Hair);
        this.SetSpriteByEcharactorCpn(ESKINCHARACTORCPN.HairLong,
            charactorDataGeneratorInfomationConvertToSprite.HairLong);
        this.SetSpriteByEcharactorCpn(ESKINCHARACTORCPN.Skin_Top, charactorDataGeneratorInfomationConvertToSprite.Top);
        this.SetSpriteByEcharactorCpn(ESKINCHARACTORCPN.Skin_Hand_L,
            charactorDataGeneratorInfomationConvertToSprite.TopComponent[0]);
        this.SetSpriteByEcharactorCpn(ESKINCHARACTORCPN.Skin_Hand_R,
            charactorDataGeneratorInfomationConvertToSprite.TopComponent[1]);
        this.SetSpriteByEcharactorCpn(ESKINCHARACTORCPN.Body_Bottom_Core,
            charactorDataGeneratorInfomationConvertToSprite.Bottom);
        this.SetSpriteByEcharactorCpn(ESKINCHARACTORCPN.Skin_Bottom,
            charactorDataGeneratorInfomationConvertToSprite.Bottom);
        this.SetSpriteByEcharactorCpn(ESKINCHARACTORCPN.Skin_Leg_L,
            charactorDataGeneratorInfomationConvertToSprite.BottomComponent[0]);
        this.SetSpriteByEcharactorCpn(ESKINCHARACTORCPN.Skin_Leg_R,
            charactorDataGeneratorInfomationConvertToSprite.BottomComponent[1]);
        this.SetSpriteByEcharactorCpn(ESKINCHARACTORCPN.Skin_Food_L,
            charactorDataGeneratorInfomationConvertToSprite.Shoes[0]);
        this.SetSpriteByEcharactorCpn(ESKINCHARACTORCPN.Skin_Food_R,
            charactorDataGeneratorInfomationConvertToSprite.Shoes[1]);

        if (charactorDataGeneratorInfomationConvertToSprite.Accessories != null)
        {
            this.SetSpriteByEcharactorCpn(ESKINCHARACTORCPN.Accesories_Face,
                charactorDataGeneratorInfomationConvertToSprite.Accessories);
            this.IsOnSprite(ESKINCHARACTORCPN.Accesories_Face, true);
        }
        else
        {
            this.IsOnSprite(ESKINCHARACTORCPN.Accesories_Face, false);
        }

        CharactorVehicleController charactorVehicleController = this.GetComponentInParent<CharactorVehicleController>();
        if (charactorDataGeneratorInfomationConvertToSprite.Vehicle != null)
        {
            ESKINCHARACTORCPN eskincharactorcpn =
                CheckVehicleStringToVehicleDetail(charactorDataGeneratorInfomationConvertToSprite.Vehicle.name);

            switch (eskincharactorcpn)
            {
                case ESKINCHARACTORCPN.Car_Vehicle:
                    charactorVehicleController.SetHaveVehicle(true);
                    charactorVehicleController.SetCurVehicle(EVehicle.Car);
                    break;
                case ESKINCHARACTORCPN.Motor_Vehicle:
                    charactorVehicleController.SetHaveVehicle(true);
                    charactorVehicleController.SetCurVehicle(EVehicle.Motor);
                    break;
                case ESKINCHARACTORCPN.Skate_Vehicle:
                    charactorVehicleController.SetHaveVehicle(true);
                    charactorVehicleController.SetCurVehicle(EVehicle.Skate);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            charactorVehicleController.EquipVehicle();
        }
        else
        {
            charactorVehicleController.SetHaveVehicle(false);
            charactorVehicleController.SetCurVehicle(EVehicle.none);
            charactorVehicleController.EquipVehicle();
        }

        CharactorWingController charactorWingController = this.GetComponentInParent<CharactorWingController>();

        if (charactorDataGeneratorInfomationConvertToSprite.Wing != null)
        {
            charactorWingController.SetHaveWing(true);
            charactorWingController.SetCurWing(
                CONSTANT.ParseEnum<EWing>(charactorDataGeneratorInfomationConvertToSprite.Wing.name));
            charactorWingController.EquipWing();
        }
        else
        {
            charactorWingController.SetHaveWing(false);
            charactorWingController.SetCurWing(EWing.none);
            charactorWingController.EquipWing();
        }

        CharactorTailController charactorTailController = this.GetComponentInParent<CharactorTailController>();
        if (charactorDataGeneratorInfomationConvertToSprite.Tail != null)
        {
            charactorTailController.SetHaveTail(true);
            charactorTailController.SetCurTail(
                CONSTANT.ParseEnum<ETail>(charactorDataGeneratorInfomationConvertToSprite.Tail.name));
            charactorTailController.EquipTail();
        }
        else
        {
            charactorTailController.SetHaveTail(false);
            charactorTailController.SetCurTail(ETail.none);
            charactorTailController.EquipTail();
        }
    }

    public void SetSpriteByEcharactorCpn(ESKINCHARACTORCPN _eskincharactorcpn, Sprite _sprite)
    {
        foreach (var item in CharactorSpriteControllers)
        {
            if (item.Eskincharactorcpn.Equals(_eskincharactorcpn))
            {
                item.ChangeSprite(_sprite);
                return;
            }
        }
    }

    public void SetSkinSkeletonByID(ESKINCHARACTORCPN _eskincharactorcpn, string idSkin)
    {
        foreach (var item in CharactorSpriteControllers)
        {
            if (item.Eskincharactorcpn.Equals(_eskincharactorcpn))
            {
                item.SetSkinSkeletonByID(idSkin);
                return;
            }
        }
    }

    public void IsOnSprite(ESKINCHARACTORCPN eskincharactorcpn, bool isOn)
    {
        foreach (var item in CharactorSpriteControllers)
        {
            if (item.Eskincharactorcpn.Equals(eskincharactorcpn))
            {
                item.IsOnSprite(isOn);
                return;
            }
        }
    }

    public ESKINCHARACTORCPN CheckVehicleStringToVehicleDetail(string idVehicle)
    {
        if (idVehicle.Contains("Car"))
        {
            return ESKINCHARACTORCPN.Car_Vehicle;
        }

        if (idVehicle.Contains("Motor"))
        {
            return ESKINCHARACTORCPN.Motor_Vehicle;
        }

        if (idVehicle.Contains("Skate"))
        {
            return ESKINCHARACTORCPN.Skate_Vehicle;
        }

        return ESKINCHARACTORCPN.None;
    }

    public bool SwapSpriteByECharactorDetail(bool isChoose, ECHARACTORDETAIL echaractordetail, ItemStyle itemStyle)
    {
        bool result = true;
        switch (echaractordetail)
        {
            case ECHARACTORDETAIL.BODY:
                this.SetSpriteByEcharactorCpn(ESKINCHARACTORCPN.Body_Top_Core,
                    itemStyle._itemsItemChildDetails[0].Sprite);
                this.SetSpriteByEcharactorCpn(ESKINCHARACTORCPN.HeadCore, itemStyle._itemsItemChildDetails[1].Sprite);
                this.SetSpriteByEcharactorCpn(ESKINCHARACTORCPN.Food_L_Core,
                    itemStyle._itemsItemChildDetails[2].Sprite);
                this.SetSpriteByEcharactorCpn(ESKINCHARACTORCPN.Food_R_Core,
                    itemStyle._itemsItemChildDetails[2].Sprite);
                this.SetSpriteByEcharactorCpn(ESKINCHARACTORCPN.Hand_L_Core,
                    itemStyle._itemsItemChildDetails[3].Sprite);
                this.SetSpriteByEcharactorCpn(ESKINCHARACTORCPN.Hand_R_Core,
                    itemStyle._itemsItemChildDetails[4].Sprite);
                this.SetSpriteByEcharactorCpn(ESKINCHARACTORCPN.Leg_L_Core, itemStyle._itemsItemChildDetails[5].Sprite);
                this.SetSpriteByEcharactorCpn(ESKINCHARACTORCPN.Leg_R_Core, itemStyle._itemsItemChildDetails[5].Sprite);

                break;
            case ECHARACTORDETAIL.Face:
                this.SetSpriteByEcharactorCpn(ESKINCHARACTORCPN.Face, itemStyle.Sprite);

                break;
            case ECHARACTORDETAIL.HAIR:
                this.SetSpriteByEcharactorCpn(ESKINCHARACTORCPN.Hair, itemStyle.Sprite);

                break;
            case ECHARACTORDETAIL.HAIRLONG:
                this.SetSpriteByEcharactorCpn(ESKINCHARACTORCPN.HairLong, itemStyle.Sprite);
                break;
            case ECHARACTORDETAIL.TOP:
                this.SetSpriteByEcharactorCpn(ESKINCHARACTORCPN.Skin_Top, itemStyle.Sprite);
                this.SetSpriteByEcharactorCpn(ESKINCHARACTORCPN.Skin_Hand_L,
                    itemStyle._itemsItemChildDetails[0].Sprite);
                this.SetSpriteByEcharactorCpn(ESKINCHARACTORCPN.Skin_Hand_R,
                    itemStyle._itemsItemChildDetails[1].Sprite);

                break;
            case ECHARACTORDETAIL.BOTTOM:

                this.SetSpriteByEcharactorCpn(ESKINCHARACTORCPN.Skin_Bottom, itemStyle.Sprite);
                this.SetSpriteByEcharactorCpn(ESKINCHARACTORCPN.Skin_Leg_L, itemStyle._itemsItemChildDetails[0].Sprite);
                this.SetSpriteByEcharactorCpn(ESKINCHARACTORCPN.Skin_Leg_R, itemStyle._itemsItemChildDetails[1].Sprite);
                break;
            case ECHARACTORDETAIL.ACCESSOIRES_Face:
                this.IsOnSprite(ESKINCHARACTORCPN.Accesories_Face, true);
                this.SetSpriteByEcharactorCpn(ESKINCHARACTORCPN.Accesories_Face, itemStyle.Sprite);
                break;
            case ECHARACTORDETAIL.VEHICLE:
                this.SelectVehicle(itemStyle);
                break;
            case ECHARACTORDETAIL.Shoes:
                this.SetSpriteByEcharactorCpn(ESKINCHARACTORCPN.Skin_Food_L, itemStyle.Sprite);
                this.SetSpriteByEcharactorCpn(ESKINCHARACTORCPN.Skin_Food_R, itemStyle.Sprite);
                break;
            case ECHARACTORDETAIL.Tail:
                this.IsOnSprite(ESKINCHARACTORCPN.Tail, true);
                this.SetSkinSkeletonByID(ESKINCHARACTORCPN.Tail, itemStyle.ID);
                break;
            case ECHARACTORDETAIL.Wings:
                this.IsOnSprite(ESKINCHARACTORCPN.Wings, true);
                this.SetSkinSkeletonByID(ESKINCHARACTORCPN.Wings, itemStyle.ID);
                break;
            case ECHARACTORDETAIL.Weapon_L:
                this.IsOnSprite(ESKINCHARACTORCPN.Weapon_L, true);
                this.SetSkinSkeletonByID(ESKINCHARACTORCPN.Weapon_L, itemStyle.ID);
                break;

            case ECHARACTORDETAIL.Weapon_R:
                this.IsOnSprite(ESKINCHARACTORCPN.Weapon_R, true);
                this.SetSpriteByEcharactorCpn(ESKINCHARACTORCPN.Weapon_R, itemStyle.Sprite);
                break;

            default:
                result = false;
                break;
        }

        return result;
    }

    private void SelectVehicle(ItemStyle itemStyle)
    {
        this.IsOnSprite(ESKINCHARACTORCPN.Car_Vehicle, false);
        this.IsOnSprite(ESKINCHARACTORCPN.Motor_Vehicle, false);
        this.IsOnSprite(ESKINCHARACTORCPN.Skate_Vehicle, false);
        Debug.Log("123123");

        switch (this.CheckVehicleStringToVehicleDetail(itemStyle.ID))
        {
            case ESKINCHARACTORCPN.Car_Vehicle:
                this.CharactorSkinManager.CharactorSkeletonAnimationController.PlayDriveCar(true);
                charactorVehicleController.SetHaveVehicle(true);
                charactorVehicleController.SetCurVehicle(EVehicle.Car);
                this.SetSpriteByEcharactorCpn(ESKINCHARACTORCPN.Car_Vehicle, itemStyle.Sprite);
                this.IsOnSprite(ESKINCHARACTORCPN.Car_Vehicle, true);
                break;
            case ESKINCHARACTORCPN.Motor_Vehicle:
                this.CharactorSkinManager.CharactorSkeletonAnimationController.PlayDriveMotor(true);
                charactorVehicleController.SetHaveVehicle(true);
                charactorVehicleController.SetCurVehicle(EVehicle.Motor);
                this.SetSpriteByEcharactorCpn(ESKINCHARACTORCPN.Motor_Vehicle, itemStyle.Sprite);
                this.IsOnSprite(ESKINCHARACTORCPN.Motor_Vehicle, true);
                break;
            case ESKINCHARACTORCPN.Skate_Vehicle:
                this.CharactorSkinManager.CharactorSkeletonAnimationController.PlayDriveSkate(true);
                charactorVehicleController.SetHaveVehicle(true);
                charactorVehicleController.SetCurVehicle(EVehicle.Skate);
                this.SetSpriteByEcharactorCpn(ESKINCHARACTORCPN.Skate_Vehicle, itemStyle.Sprite);
                this.IsOnSprite(ESKINCHARACTORCPN.Skate_Vehicle, true);
                break;
        }
    }

    public void SelectVehicle(EVehicle eVehicle)
    {
        this.IsOnSprite(ESKINCHARACTORCPN.Car_Vehicle, false);
        this.IsOnSprite(ESKINCHARACTORCPN.Motor_Vehicle, false);
        this.IsOnSprite(ESKINCHARACTORCPN.Skate_Vehicle, false);
        if (eVehicle == EVehicle.none)
        {
            this.charactorVehicleController.SetHaveVehicle(false);
            this.charactorVehicleController.SetCurVehicle(EVehicle.none);
            this.CharactorSkinManager.CharactorSkeletonAnimationController.PlayIdle(true);
        }

        switch (eVehicle)
        {
            case EVehicle.Car:
                this.CharactorSkinManager.CharactorSkeletonAnimationController.PlayDriveCar(true);
                this.charactorVehicleController.SetHaveVehicle(true);
                this.charactorVehicleController.SetCurVehicle(EVehicle.Car);
                this.IsOnSprite(ESKINCHARACTORCPN.Car_Vehicle, true);
                break;
            case EVehicle.Motor:
                this.CharactorSkinManager.CharactorSkeletonAnimationController.PlayDriveMotor(true);
                this.charactorVehicleController.SetHaveVehicle(true);
                this.charactorVehicleController.SetCurVehicle(EVehicle.Motor);
                this.IsOnSprite(ESKINCHARACTORCPN.Motor_Vehicle, true);
                break;
            case EVehicle.Skate:
                this.CharactorSkinManager.CharactorSkeletonAnimationController.PlayDriveSkate(true);
                this.charactorVehicleController.SetHaveVehicle(true);
                this.charactorVehicleController.SetCurVehicle(EVehicle.Skate);
                this.IsOnSprite(ESKINCHARACTORCPN.Skate_Vehicle, true);
                break;
        }
    }
}