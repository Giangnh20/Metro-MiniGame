using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Game.Data.DataScriptObjectsAble;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class DataCharactorManagers : MonoBehaviour
{
    #region Singleton

    private static DataCharactorManagers instance;

    public static DataCharactorManagers Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<DataCharactorManagers>();
                DontDestroyOnLoad(instance);
            }

            return instance;
        }
    }

    #endregion

    /// <summary>
    /// All Charactor Genertor Data
    /// </summary>
    [FormerlySerializedAs("CharactorDatas")]
    public DataScriptObjectAble[] charactorDatas;

    /// <summary>
    /// this is Func Load Sprite by ID
    /// </summary>
    /// <param name="etypecharactor"> Type Charator. Ex:BOY ,GIRL , Special</param>
    /// <param name="echaractordetail"> Type Charator details. Ex : body ,head, hair ....</param>
    /// <param name="id"> ID Sprite Needed</param>
    /// <returns></returns>
    public ItemStyle LoadItemStyleByID(ETYPECHARACTOR etypecharactor, ECHARACTORDETAIL echaractordetail, string id)
    {
        if (id.Equals(string.Empty))
        {
            return null;
        }

        ItemStyle _itemStyle = null;

        foreach (var item in charactorDatas)
        {
            if (item.Etypecharactor.Equals(etypecharactor))
            {
                var itemStyles = GetItemStyles(echaractordetail, item);
                foreach (var itemStyle in itemStyles.Where(itemStyle => itemStyle.ID.Equals(id)))
                {
                    _itemStyle = itemStyle;
                    break;
                }

                return _itemStyle;
            }
        }

        Debug.Log("Body Skin not found !");
        return null;
    }

    public ItemStyle[] LoadItemsStyle(ETYPECHARACTOR etypecharactor, ECHARACTORDETAIL echaractordetail)
    {
        ItemStyle[] _itemStyle = null;

        foreach (var item in charactorDatas)
        {
            if (item.Etypecharactor.Equals(etypecharactor))
            {
                var itemStyles = GetItemStyles(echaractordetail, item);
                if (itemStyles == null)
                {
                    return null;
                }
                else
                {
                    _itemStyle = itemStyles.ToArray();
                }

                return _itemStyle;
            }
        }

        return null;
    }


    public ItemChildDetail[] LoadSpriteAnimationByID(ETYPECHARACTOR etypecharactor, ECHARACTORDETAIL echaractordetail,
        string id)
    {
        if (id.Equals(String.Empty))
        {
            return null;
        }

        ItemChildDetail[] itemAnimations = null;
        foreach (var item in this.charactorDatas)
        {
            if (item.Etypecharactor.Equals(etypecharactor))
            {
                var itemStyle = GetItemStyles(echaractordetail, item);
                foreach (var style in itemStyle)
                {
                    if (style.ID.Equals(id))
                    {
                        itemAnimations = style._itemsItemChildDetails.ToArray();
                        return itemAnimations;
                    }
                }
            }
        }

        Debug.Log("Sprite Animation Not Found ! " + id);
        return null;
    }

    #region staticFunc

    private static List<ItemStyle> GetItemStyles(ECHARACTORDETAIL echaractordetail, DataScriptObjectAble item)
    {
        List<ItemStyle> itemStyles = null;
        switch (echaractordetail)
        {
            case ECHARACTORDETAIL.BODY:
                itemStyles = item.bodyItemStyles;
                break;
            case ECHARACTORDETAIL.Face:
                itemStyles = item.FacesItemStyles;
                break;
            case ECHARACTORDETAIL.HAIR:
                itemStyles = item.hairItemStyles;
                break;
            case ECHARACTORDETAIL.HAIRLONG:
                itemStyles = item.hairLongItemStyles;
                break;
            case ECHARACTORDETAIL.TOP:
                itemStyles = item.topItemStyles;
                break;
            case ECHARACTORDETAIL.BOTTOM:
                itemStyles = item.bottomItemStyles;
                break;
            case ECHARACTORDETAIL.ACCESSOIRES_Face:
                itemStyles = item.AccessoiresItemStyles;
                break;
            case ECHARACTORDETAIL.VEHICLE:
                itemStyles = item.vehicleItemStyles;
                break;
            case ECHARACTORDETAIL.Shoes:
                itemStyles = item.shoesItemStyles;
                break;
            case ECHARACTORDETAIL.Wings:
                itemStyles = item.WingsItemStyles;
                break;
            case ECHARACTORDETAIL.Tail:
                itemStyles = item.TailsItemStyles;
                break;
            case ECHARACTORDETAIL.Weapon_L:
                itemStyles = item.Weapon_l_ItemStyles;
                break;
            case ECHARACTORDETAIL.Weapon_R:
                itemStyles = item.Weapon_R_ItemStyles;
                break;
            default:
                itemStyles = null;
                break;
        }

        return itemStyles;
    }

    #endregion
}