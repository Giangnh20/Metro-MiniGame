using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class ObjectsController : MonoBehaviour
{
    [SerializeField] [PreviewField] private Sprite Icon;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private EObject EObject;

    public SpriteRenderer SpriteRenderer
    {
        get
        {
            if (this.spriteRenderer == null)
            {
                this.spriteRenderer = this.GetComponentInChildren<SpriteRenderer>();
            }

            return this.spriteRenderer;
        }
        set => this.spriteRenderer = value;
    }

    public void SetOrderinLayer(int IDLayer)
    {
        this.SpriteRenderer.sortingLayerID = IDLayer;
    }

    public void SetSprite(Sprite sprite)
    {
        this.SpriteRenderer.sprite = sprite;
    }

    [Button]
    public void UpdateSprite_Click()
    {
        this.SetSprite(this.Icon);
    }
}