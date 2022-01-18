using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Data.DataScriptObjectsAble
{
    [CreateAssetMenu(fileName = "DataGlobal", menuName = "MyGame/DataGlobal")]
    public class DataScriptObjectAble : ScriptableObject
    {
        public ETYPECHARACTOR Etypecharactor = ETYPECHARACTOR.BOY;

        [BoxGroup("Body Skins")] public List<ItemStyle> bodyItemStyles = new List<ItemStyle>();

        // [BoxGroup("Head Skins")] public List<ItemStyle> headItemStyles = new List<ItemStyle>();
        [FormerlySerializedAs("eyeItemStyles")] [BoxGroup("Face Skins")]
        public List<ItemStyle> FacesItemStyles = new List<ItemStyle>();

        [BoxGroup("Hair Skins")] public List<ItemStyle> hairItemStyles = new List<ItemStyle>();
        [BoxGroup("HairLong Skins")] public List<ItemStyle> hairLongItemStyles = new List<ItemStyle>();

        [FormerlySerializedAs("upItemStyles")] [BoxGroup("Top Skins")]
        public List<ItemStyle> topItemStyles = new List<ItemStyle>();

        [FormerlySerializedAs("downItemStyles")] [BoxGroup("Bottom Skins")]
        public List<ItemStyle> bottomItemStyles = new List<ItemStyle>();

        [FormerlySerializedAs("accessoiItemStyles")] [BoxGroup("Accessoires Skins")]
        public List<ItemStyle> AccessoiresItemStyles = new List<ItemStyle>();

        [BoxGroup("Vehicle Skins")] public List<ItemStyle> vehicleItemStyles = new List<ItemStyle>();

        [BoxGroup("Shoes")] public List<ItemStyle> shoesItemStyles = new List<ItemStyle>();

        [BoxGroup("Tails")] public List<ItemStyle> TailsItemStyles = new List<ItemStyle>();

        [BoxGroup("Wings")] public List<ItemStyle> WingsItemStyles = new List<ItemStyle>();

        [BoxGroup("Weapons_L")] public List<ItemStyle> Weapon_l_ItemStyles = new List<ItemStyle>();

        [BoxGroup("Weapons_R")] public List<ItemStyle> Weapon_R_ItemStyles = new List<ItemStyle>();
    }
}

[System.Serializable]
public class DataCharactorGenerate
{
    private List<ItemStyle> _itemStyles = new List<ItemStyle>();
}

[System.Serializable]
public class ItemStyle
{
    [LabelWidth(100)] public string ID;


    [HorizontalGroup("Item datas")] [PreviewField(75)] [HideLabel]
    public Sprite Sprite;

    [FormerlySerializedAs("_itemsItemAnimations")] [FormerlySerializedAs("_bodyAnimations")] [VerticalGroup("Item datas/Animation")]
    public List<ItemChildDetail> _itemsItemChildDetails = new List<ItemChildDetail>();
}

[System.Serializable]
public class ItemChildDetail
{
    [LabelWidth(100)] public string ID;
    [PreviewField(75)] public Sprite Sprite;

    public ItemChildDetail()
    {
    }

    public ItemChildDetail(string _id = "", Sprite _sprite = null)
    {
        this.ID = _id;
        this.Sprite = _sprite;
    }
}