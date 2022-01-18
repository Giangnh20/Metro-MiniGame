using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class UIEditorCpnController : MonoBehaviour
{
    [SerializeField] private GameObject ItemPrefab;
    [SerializeField] private ECHARACTORDETAIL eStartCreateItems = ECHARACTORDETAIL.TOP;

    [SerializeField] private ItemsControllers[] _itemsControllersArray;
    private bool inited = false;

    private GameObject MainCharactor = null;


    private void Start()
    {
        this.MainCharactor = GameObject.FindWithTag("Player");
        SetOnStart();
    }

    public void SetOnStart()
    {
        this.CreateID();
        this.ResetAllParentItems();
        this.CreateItems(this.MainCharactor.GetComponent<CharactorSex>().Etypecharactor, eStartCreateItems);
        for (int i = 0; i < this._itemsControllersArray.Length; i++)
        {
            if (this._itemsControllersArray[i].echaractordetail.Equals(eStartCreateItems))
            {
                this._itemsControllersArray[i].ParentPanel.gameObject.SetActive(true);
            }
        }

        //this.ResetAllItemChoosed();
        this.inited = true;
    }

    public void CreatingItems(int ID)
    {
        this.ResetAllParentItems();
        foreach (var item in this._itemsControllersArray)
        {
            if (item.ID == ID)
            {
                CreateItems(this.MainCharactor.GetComponent<CharactorSex>().Etypecharactor, item.echaractordetail);
            }
        }
        //this.ResetAllItemChoosed();
    }

    public void ResetAllParentItems()
    {
        foreach (var item in _itemsControllersArray)
        {
            foreach (var item2 in item.ListItemsPrefab)
            {
                Destroy(item2);
            }

            item.ListItemsPrefab.Clear();
            item.ParentPanel.gameObject.SetActive(false);
        }
    }

    public void ResetAllItemChoosed()
    {
        foreach (var item in _itemsControllersArray)
        {
            this.ResetItemChoosed(item.ListItemsPrefab);
        }
    }

    public void ResetItemChoosed(List<GameObject> lisItemPrefab)
    {
        if (lisItemPrefab == null)
        {
            return;
        }

        foreach (var item in lisItemPrefab)
        {
            UIEditorItemPrefab uiEditorItemPrefab = item.GetComponent<UIEditorItemPrefab>();
            uiEditorItemPrefab.IsOnChoose(false);
        }
    }

    private void CreateID()
    {
        for (int i = 0; i < _itemsControllersArray.Length; i++)
        {
            this._itemsControllersArray[i].ID = i;
        }
    }

    public void CreateItems(ETYPECHARACTOR etypecharactor, ECHARACTORDETAIL echaractordetail)
    {
       

        for (int i = 0; i < this._itemsControllersArray.Length; i++)
        {
            if (this._itemsControllersArray[i].echaractordetail.Equals(echaractordetail))
            {
                this._itemsControllersArray[i].ParentPanel.gameObject.SetActive(true);
                
                ItemStyle[] itemStyles = DataCharactorManagers.Instance.LoadItemsStyle(etypecharactor, echaractordetail);
                if (itemStyles == null)
                {
                    Debug.Log("item not found !");
                    return;
                }
                
                for (int j = 0; j < itemStyles.Length; j++)
                {
                    GameObject Go = GameObject.Instantiate(ItemPrefab, Vector3.zero, Quaternion.identity) as GameObject;
                    Go.transform.SetParent(this._itemsControllersArray[i].ParentPanel);
                    Go.SetActive(true);
                    Go.transform.localPosition = Vector3.zero;
                    Go.transform.localScale = Vector3.one;
                    UIEditorItemPrefab uiEditorItemPrefab = Go.GetComponent<UIEditorItemPrefab>();
                    uiEditorItemPrefab.SetOnStart(this._itemsControllersArray[i].echaractordetail, itemStyles[j],
                        this.btn_ClickCallback);
                    this._itemsControllersArray[i].ListItemsPrefab.Add(Go);
                }
            }
        }
    }

    private void btn_ClickCallback(object obj)
    {
        UIEditorItemPrefab uiEditorItemPrefab = (UIEditorItemPrefab) obj;
        this.ResetItemChoosed(this.CheckItemInList(uiEditorItemPrefab.gameObject));
        uiEditorItemPrefab.IsOnChoose(!uiEditorItemPrefab.IsChoosed);
        this.MainCharactor.GetComponent<CharactorSkinManager>().SetSkin(uiEditorItemPrefab.IsChoosed, uiEditorItemPrefab._echaractordetail, uiEditorItemPrefab._itemStyle);
    }

    private List<GameObject> CheckItemInList(GameObject item)
    {
        for (int i = 0; i < this._itemsControllersArray.Length; i++)
        {
            if (this._itemsControllersArray[i].ListItemsPrefab.Contains(item))
            {
                return this._itemsControllersArray[i].ListItemsPrefab;
            }
        }

        Debug.Log("list not found !");
        return null;
    }
}

[System.Serializable]
public class ItemsControllers
{
    public int ID = 0;
    public ECHARACTORDETAIL echaractordetail;
    public Button btnButton;
    public Transform ParentPanel;
    public List<GameObject> ListItemsPrefab = new List<GameObject>();
}