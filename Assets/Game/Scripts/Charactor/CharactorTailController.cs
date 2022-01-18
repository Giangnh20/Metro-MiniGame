using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactorTailController : MonoBehaviour, ITail
{
    [SerializeField] private bool haveTail = false;

    public bool HaveTail
    {
        get { return this.haveTail; }
        set { this.haveTail = value; }
    }

    [SerializeField] private ETail eTail = ETail.none;
    private CharactorSkinManager charactorSkinManager;
    private CharactorMovement charactorMovement;

    private void Start()
    {
        this.charactorSkinManager = this.GetComponent<CharactorSkinManager>();
        this.charactorMovement = this.GetComponent<CharactorMovement>();
    }

    public void SetHaveTail(bool have)
    {
        this.HaveTail = have;
    }

    public void SetCurTail(ETail eTail)
    {
        this.eTail = eTail;
    }

    public void EquipTail()
    {
        switch (this.eTail)
        {
            case ETail.none:
                this.SetCurTail(ETail.none);
                this.SetHaveTail(false);
                break;
            case ETail.Tail_1:
                this.SetCurTail(ETail.Tail_1);
                this.SetHaveTail(true);
                this.charactorSkinManager.OtherSkeletionAnimationController[1].SetSkinsByName(this.eTail.ToString());
                this.charactorSkinManager.OtherSkeletionAnimationController[1].SetIsOnMeshRenreder(true);
                break;
            case ETail.Tail_2:
                this.SetCurTail(ETail.Tail_2);
                this.SetHaveTail(true);
                this.charactorSkinManager.OtherSkeletionAnimationController[1].SetSkinsByName(this.eTail.ToString());
                this.charactorSkinManager.OtherSkeletionAnimationController[1].SetIsOnMeshRenreder(true);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}