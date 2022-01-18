using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class CharactorWingController : MonoBehaviour, IWing
{
    [SerializeField] private bool haveWing = false;

    public bool HaveWing
    {
        get { return this.haveWing; }
        set { this.haveWing = value; }
    }

    [FormerlySerializedAs("rEWing")] [SerializeField]
    private EWing EWing = EWing.none;

    private CharactorMovement charactorMovement;

    private CharactorSkinManager charactorSkinManager;

    private void Start()
    {
        this.charactorSkinManager = this.GetComponent<CharactorSkinManager>();
        this.charactorMovement = this.GetComponent<CharactorMovement>();
    }

    public void SetHaveWing(bool have)
    {
        this.HaveWing = have;
    }

    public void SetCurWing(EWing _eVehicle)
    {
        this.EWing = _eVehicle;
    }

    [Button]
    public void EquipWing()
    {
        switch (this.EWing)
        {
            case EWing.none:
                this.SetHaveWing(false);
                this.SetCurWing(EWing.none);
                this.charactorSkinManager.OtherSkeletionAnimationController[0].SetIsOnMeshRenreder(false);
                break;
            case EWing.Wings_1:
                this.SetHaveWing(true);
                this.SetCurWing(EWing.Wings_1);
                this.charactorSkinManager.OtherSkeletionAnimationController[0].SetSkinsByName(this.EWing.ToString());
                this.charactorSkinManager.OtherSkeletionAnimationController[0].SetIsOnMeshRenreder(true);
                break;
            case EWing.Wings_2:
                this.SetHaveWing(true);
                this.SetCurWing(EWing.Wings_2);
                this.charactorSkinManager.OtherSkeletionAnimationController[0].SetSkinsByName(this.EWing.ToString());
                this.charactorSkinManager.OtherSkeletionAnimationController[0].SetIsOnMeshRenreder(true);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        this.charactorMovement.IdleExtension();
    }
}