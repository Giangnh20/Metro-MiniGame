using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class CharactorFishingRobController : MonoBehaviour, IFishing
{
    public bool IsDoingFish { get; set; }
    public bool IsFishing { get; set; }
    public bool Fished { get; set; }

    public void DoFishing(bool doing)
    {
        this.IsDoingFish = doing;
    }

    private CharactorSkinManager charactorSkinManager;

    private void Start()
    {
        this.charactorSkinManager = this.GetComponent<CharactorSkinManager>();
    }

    [Button]
    public void Play_Fishing_Open_Bag(bool isloop)
    {
        this.charactorSkinManager.CharactorSkeletonAnimationController.Play_Fishing_Open_Bag(isloop);
        this.charactorSkinManager.FishingrodSkeletonAnimationController.Play_Fishing_Open_Bag(isloop);
    }

    [Button]
    public void Play_Fishing_Catch_1(bool isLoop)
    {
        this.charactorSkinManager.CharactorSkeletonAnimationController.Play_Fishing_Catch_1(isLoop);
        this.charactorSkinManager.FishingrodSkeletonAnimationController.Play_Fishing_Catch_1(isLoop);
        this.charactorSkinManager.FishingrodSkeletonAnimationController.SetIsOnMeshRenreder(true);
    }

    [Button]
    public void Play_Fishing_Catch_2(bool isLoop)
    {
        this.charactorSkinManager.CharactorSkeletonAnimationController.Play_Fishing_Catch_2(isLoop);
        this.charactorSkinManager.FishingrodSkeletonAnimationController.Play_Fishing_Catch_2(isLoop);
        this.charactorSkinManager.FishingrodSkeletonAnimationController.SetIsOnMeshRenreder(true);
    }

    [Button]
    public void Play_Fishing_Idle(bool isLoop)
    {
        this.charactorSkinManager.CharactorSkeletonAnimationController.Play_Fishing_Idle(isLoop);
        this.charactorSkinManager.FishingrodSkeletonAnimationController.Play_Fishing_Idle(isLoop);
        this.charactorSkinManager.FishingrodSkeletonAnimationController.SetIsOnMeshRenreder(true);
    }

    [Button]
    public void IsOnMeshRenreder(bool isOnMeshRenreder)
    {
        this.charactorSkinManager.FishingrodSkeletonAnimationController.SetIsOnMeshRenreder(isOnMeshRenreder);
    }
}