using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Spine;
using Spine.Unity;
using UnityEngine;
using Event = Spine.Event;

public class CharactorSkeletonAnimationController : MonoBehaviour
{
    private SkeletonAnimation _skeletonAnimation;

    public SkeletonAnimation skeletonAnimation
    {
        get
        {
            if (this._skeletonAnimation == null)
            {
                this._skeletonAnimation = this.GetComponent<SkeletonAnimation>();
            }

            return this._skeletonAnimation;
        }
    }

    [SpineAnimation] private string _animationHi = "Hi";
    [SpineAnimation] private string _animationIdle = "Idle";
    [SpineAnimation] private string _animationJump = "Jump";
    [SpineAnimation] private string _animationWalk = "Walk";
    [SpineAnimation] private string _animationDriveCar = "Drive_Car";
    [SpineAnimation] private string _animationDriveMotor = "Drive_Motor";
    [SpineAnimation] private string _animationDriveSkate = "Drive_Skate";
    [SpineAnimation] private string _animationWalk_Hand_Equip = "Walk_Hand_Equip";
    [SpineAnimation] private string _animationFly = "Fly";
    [SpineAnimation] private string _animationFishing_Catch_1 = "Fishing_Catch_1";
    [SpineAnimation] private string _animationFishing_Catch_2 = "Fishing_Catch_2";
    [SpineAnimation] private string _animationFishing_Idle = "Fishing_Ilde";
    [SpineAnimation] private string _animationFishing_Open_Bag = "Fishing_Open_Bag";
    [SpineAnimation] private string _animationFishing_Start = "Fishing_Start";
    [SpineAnimation] private string _animationPlay_FootBall_1 = "Play_FootBall_1";
    [SpineAnimation] private string _animationPlay_FootBall_2 = "Play_FootBall_2";
    [SpineAnimation] private string _animationSit_Idle = "Sit_Idle";
    [SpineAnimation] private string _animationSit_Talk = "Sit_Talk";
    [SpineAnimation] private string _animationVictory_1 = "Victory_1";
    [SpineAnimation] private string _animationVictory_2 = "Victory_2";
    private bool inited = false;
    private Spine.AnimationState _animationState;

    private int trackIndex = 0;

    void Start()
    {
        this._animationState = this.skeletonAnimation.AnimationState;
        // registering for events raised by any animation
        this._animationState.Start += OnSpineAnimationStart;
        this._animationState.Interrupt += OnSpineAnimationInterrupt;
        this._animationState.End += OnSpineAnimationEnd;
        this._animationState.Dispose += OnSpineAnimationDispose;
        this._animationState.Complete += OnSpineAnimationComplete;
        this._animationState.Event += OnUserDefinedEvent;
    }

    private void OnUserDefinedEvent(TrackEntry trackentry, Event e)
    {
    }

    private void OnSpineAnimationComplete(TrackEntry trackentry)
    {
    }

    private void OnSpineAnimationDispose(TrackEntry trackentry)
    {
    }

    private void OnSpineAnimationEnd(TrackEntry trackentry)
    {
    }

    private void OnSpineAnimationInterrupt(TrackEntry trackentry)
    {
    }

    private void OnSpineAnimationStart(TrackEntry trackentry)
    {
    }

    public void PlayAnim(string anim, bool isLoop)
    {
        // registering for events raised by a single animation track entry
        this._animationState = this.skeletonAnimation.AnimationState;
        Spine.TrackEntry trackEntry = this._animationState.SetAnimation(trackIndex, anim, isLoop);
        trackEntry.Start += OnSpineAnimationStart;
        trackEntry.Interrupt += OnSpineAnimationInterrupt;
        trackEntry.End += OnSpineAnimationEnd;
        trackEntry.Dispose += OnSpineAnimationDispose;
        trackEntry.Complete += OnSpineAnimationComplete;
        trackEntry.Event += OnUserDefinedEvent;
    }

    [Button]
    public void PlayHi(bool isLoop = false)
    {
        this.PlayAnim(_animationHi, isLoop);
    }

    [Button]
    public void PlayIdle(bool isLoop = true)
    {
        this.PlayAnim(_animationIdle, isLoop);
    }

    [Button]
    public void PlayWalk(bool isLoop = true)
    {
        this.PlayAnim(_animationWalk, isLoop);
    }

    [Button]
    public void PlayJump(bool isLoop = false)
    {
        this.PlayAnim(_animationJump, isLoop);
    }

    [Button]
    public void PlayDriveCar(bool isLoop = true)
    {
        this.PlayAnim(_animationDriveCar, isLoop);
    }

    [Button]
    public void PlayDriveMotor(bool isLoop = true)
    {
        this.PlayAnim(_animationDriveMotor, isLoop);
    }

    [Button]
    public void PlayDriveSkate(bool isLoop = true)
    {
        this.PlayAnim(_animationDriveSkate, isLoop);
    }

    [Button]
    public void PlayFly(bool isLoop = true)
    {
        this.PlayAnim(_animationFly, isLoop);
    }

    [Button]
    public void Play_Fishing_Open_Bag(bool isLoop = false)
    {
        this.PlayAnim(this._animationFishing_Open_Bag, isLoop);
    }

    [Button]
    public void Play_Fishing_Catch_1(bool isLoop = false)
    {
        this.PlayAnim(this._animationFishing_Catch_1, isLoop);
    }

    [Button]
    public void Play_Fishing_Catch_2(bool isLoop = false)
    {
        this.PlayAnim(this._animationFishing_Catch_2, isLoop);
    }

    [Button]
    public void Play_Fishing_Idle(bool isLoop = false)
    {
        this.PlayAnim(this._animationFishing_Idle, isLoop);
    }

    [Button]
    public void Play_Sit_Idle(bool isLoop)
    {
        this.PlayAnim(this._animationSit_Idle, isLoop);
    }

    [Button]
    public void Play_Sit_Talk(bool isLoop)
    {
        this.PlayAnim(this._animationSit_Talk, isLoop);
    }

    public void SetIsOnMeshRenreder(bool isOn)
    {
        this.skeletonAnimation.GetComponent<MeshRenderer>().enabled = isOn;
    }
}