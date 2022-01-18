using System;
using System.Collections;
using System.Collections.Generic;
using Game.Data.DataScriptObjectsAble;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class DataEditLoader : MonoBehaviour
{
    [FormerlySerializedAs("_dataBoy")] [SerializeField]
    private DataScriptObjectAble _dataCharactor;

    #region BodyGroup

    [InfoBox("This is Generate Body Data from body  and body animation folder to Charactor Data.")]
    [InlineButton("OnLoadMainBodiesData")]
    [BoxGroup("Body Group")]
    [SerializeField]
    private string url = @"DemosImages/Colored";

    [BoxGroup("Body Group")]
    public void OnLoadMainBodiesData()
    {
        LoadDataToItems(this._dataCharactor.bodyItemStyles, url);
    }

    [BoxGroup("Body Group")] [SerializeField]
    private string UrlBodyAnimation = @"DemosImages/Colored";

    [BoxGroup("Body Group")]
    [Button]
    public void OnLoadBodyAnimation(int index)
    {
        LoadDataAnimationsToItems(this._dataCharactor.bodyItemStyles, UrlBodyAnimation, index);
        Debug.Log("Load body Animation Successfully !");
    }

    #endregion

    // #region HeadGroup
    //
    // [InfoBox("This is Generate Head Data from Head folder to Charactor Data.")]
    // [BoxGroup("Head Group")]
    // [SerializeField]
    // [InlineButton("OnLoadHeadData")]
    // private string UrlHead = @"";
    //
    // public void OnLoadHeadData()
    // {
    //     LoadDataToItems(this._dataCharactor.headItemStyles, UrlHead);
    // }
    //
    // #endregion
    //
    #region FaceGroup
    
    [InfoBox("This is Generate Face Data from Face folder to Charactor Data.")]
    [BoxGroup("Face Group")]
    [SerializeField]
    [InlineButton("OnLoadFaceData")]
    
    private string UrlFace = @"";
    
    public void OnLoadFaceData()
    {
        LoadDataToItems(this._dataCharactor.FacesItemStyles, UrlFace);
    }

     #endregion

    #region HairGroup

    [InfoBox("This is Generate Hair Data from Hair folder to Charactor Data.")]
    [BoxGroup("Hair Group")]
    [SerializeField]
    [InlineButton("OnLoadHairData")]
    private string UrlHair = @"";

    public void OnLoadHairData()
    {
        LoadDataToItems(this._dataCharactor.hairItemStyles, UrlHair);
    }

    #endregion

    #region HairLongGroup

    [InfoBox("This is Generate HairLong Data from HairLong folder to Charactor Data.")]
    [BoxGroup("HairLong Group")]
    [SerializeField]
    [InlineButton("OnLoadHairLongData")]
    private string UrlHairLong = @"";

    public void OnLoadHairLongData()
    {
        LoadDataToItems(this._dataCharactor.hairLongItemStyles, UrlHairLong);
    }

    #endregion

    #region UpGroup

    [InfoBox("This is Generate Up Data from Up folder to Charactor Data.")]
    [BoxGroup("Up Group")]
    [SerializeField]
    [InlineButton("OnLoadUpData")]
    private string UrlUp = @"";

    public void OnLoadUpData()
    {
        LoadDataToItems(this._dataCharactor.topItemStyles, UrlUp);
    }

    [BoxGroup("Up Group")] [SerializeField]
    private string UrlUpAnimation = @"DemosImages/Colored";

    [BoxGroup("Up Group")]
    [Button]
    public void OnLoadUpAnimation(int index)
    {
        LoadDataAnimationsToItems(this._dataCharactor.topItemStyles, UrlUpAnimation, index);
        Debug.Log("Load Up Animation Successfully !");
    }

    #endregion

    #region DownGroup

    [InfoBox("This is Generate Down Data from Down folder to Charactor Data.")]
    [BoxGroup("Down Group")]
    [SerializeField]
    [InlineButton("OnLoadDownData")]
    private string UrlDown = @"";

    public void OnLoadDownData()
    {
        LoadDataToItems(this._dataCharactor.bottomItemStyles, UrlDown);
    }

    [BoxGroup("Down Group")] [SerializeField]
    private string UrlDownAnimation = @"DemosImages/Colored";

    [BoxGroup("Down Group")]
    [Button]
    public void OnLoadDownAnimation(int index)
    {
        LoadDataAnimationsToItems(this._dataCharactor.bottomItemStyles, UrlDownAnimation, index);
        Debug.Log("Load Up Animation Successfully !");
    }

    #endregion

    #region Accessoires Face Group

    [InfoBox("This is Generate Accessoires Data from Accessoires folder to Charactor Data.")]
    [BoxGroup("Accessoires Group")]
    [SerializeField]
    [InlineButton("OnLoadAccessoiresData")]
    private string UrlAccessoires = @"";

    public void OnLoadAccessoiresData()
    {
        LoadDataToItems(this._dataCharactor.AccessoiresItemStyles, UrlAccessoires);
    }

    #endregion

    #region Vehicle Group

    [InfoBox("This is Generate Vehicle Data from Vehicle folder to Charactor Data.")]
    [BoxGroup("Vehicle Group")]
    [SerializeField]
    [InlineButton("OnLoadVehicleData")]
    private string UrlVehicle = @"";

    public void OnLoadVehicleData()
    {
        LoadDataToItems(this._dataCharactor.vehicleItemStyles, UrlVehicle);
    }

    #endregion

    #region Shoes Group

    [InfoBox("This is Generate Shoes Data from Shoes folder to Charactor Data.")]
    [BoxGroup("Shoes Group")]
    [SerializeField]
    [InlineButton("OnLoadShoesData")]
    private string UrlShoes = @"";

    public void OnLoadShoesData()
    {
        LoadDataToItems(this._dataCharactor.shoesItemStyles, UrlShoes);
    }
    
    [BoxGroup("Shoes Group")] [SerializeField]
    private string UrlShoesAnimation = @"DemosImages/Colored";

    [BoxGroup("Shoes Group")]
    [Button]
    public void OnLoadShoesAnimation(int index)
    {
        LoadDataAnimationsToItems(this._dataCharactor.shoesItemStyles, UrlShoesAnimation, index);
        Debug.Log("Load Up Animation Successfully !");
    }

    #endregion

    #region Wings Group

    [InfoBox("This is Generate Wings Data from Wings folder to Charactor Data.")]
    [BoxGroup("Wings Group")]
    [SerializeField]
    [InlineButton("OnLoadWingsData")]
    private string UrlWings = @"";

    public void OnLoadWingsData()
    {
        LoadDataToItems(this._dataCharactor.WingsItemStyles, UrlWings);
    }

    #endregion

    #region Tails Group

    [InfoBox("This is Generate Tails Data from Tails folder to Charactor Data.")]
    [BoxGroup("Tails Group")]
    [SerializeField]
    [InlineButton("OnLoadTailsData")]
    private string UrlTails = @"";

    public void OnLoadTailsData()
    {
        LoadDataToItems(this._dataCharactor.TailsItemStyles, UrlTails);
    }

    #endregion

    #region Weapon_L

    [InfoBox("This is Generate Weapon_L Data from Weapon_L folder to Charactor Data.")]
    [BoxGroup("Weapon_L Group")]
    [SerializeField]
    [InlineButton("OnLoadWeapons_L_Data")]
    private string UrlWeapon_L = @"";

    public void OnLoadWeapons_L_Data()
    {
        LoadDataToItems(this._dataCharactor.Weapon_l_ItemStyles, UrlWeapon_L);
    }

    #endregion

    #region Weapon_R

    [InfoBox("This is Generate Weapon_R Data from Weapon_R folder to Charactor Data.")]
    [BoxGroup("Weapon_R Group")]
    [SerializeField]
    [InlineButton("OnLoadWeapons_R_Data")]
    private string UrlWeapon_R = @"";

    public void OnLoadWeapons_R_Data()
    {
        LoadDataToItems(this._dataCharactor.Weapon_R_ItemStyles, UrlWeapon_R);
    }

    #endregion

    #region staticFunc

    static void LoadDataToItems(List<ItemStyle> itemStyles, string url)
    {
        if (url.Equals(String.Empty))
        {
            Debug.Log("url is empty !");
            return;
        }

        //itemStyles.Clear();
        Sprite[] sprites = Resources.LoadAll<Sprite>(url);
        for (int i = 0; i < sprites.Length; i++)
        {
            ItemStyle itemStyle = new ItemStyle();
            itemStyle.ID = sprites[i].name;
            itemStyle.Sprite = sprites[i];
            itemStyles.Add(itemStyle);
        }

        Debug.Log("Load  Items successfully");
    }


    static void LoadDataAnimationsToItems(List<ItemStyle> itemStyles, string url, int index)
    {
        if (url.Equals(String.Empty))
        {
            Debug.Log("url animation is empty !");
            return;
        }


        string tempUrl = url;
        // tempUrl += "/" + itemStyles[i].ID;
        //  Debug.Log(tempUrl);
        Sprite[] _texture2Ds = Resources.LoadAll<Sprite>(tempUrl);
        //itemStyles[i]._itemsItemAnimations.Clear();
        for (int j = 0; j < _texture2Ds.Length; j++)
        {
            ItemChildDetail itemChildDetail = new ItemChildDetail(_texture2Ds[j].name, _texture2Ds[j]);
            itemStyles[index]._itemsItemChildDetails.Add(itemChildDetail);
        }


        Debug.Log("Load Animation  Items successfully");
    }

    #endregion
}