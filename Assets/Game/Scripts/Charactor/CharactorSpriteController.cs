using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactorSpriteController : MonoBehaviour
{
    public ESKINCHARACTORCPN Eskincharactorcpn;

    private SpriteRenderer spriteRenderer;

    private SpriteRenderer SpriteRenderer
    {
        get
        {
            if (this.spriteRenderer == null)
            {
                this.spriteRenderer = this.GetComponent<SpriteRenderer>();
            }

            return this.spriteRenderer;
        }
    }

    public void ChangeSprite(Sprite sprite = null)
    {
        this.SpriteRenderer.sprite = sprite;
    }

    public virtual void SetOrderID(int value)
    {
        this.SpriteRenderer.sortingOrder = value;
    }

    public virtual void IsOnSprite(bool isOn)
    {
        this.SpriteRenderer.enabled = isOn;
    }

    public virtual void SetSkinSkeletonByID(string _skinname)
    {
    }
}