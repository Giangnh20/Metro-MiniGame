using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Spine;
using Spine.Unity;
using Event = Spine.Event;

public class FishingrodSkeletonAnimationController : MonoBehaviour
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

    [SpineAnimation] private string Fishing_Open_Bag = "Fishing_Open_Bag";
    [SpineAnimation] private string Fishing_Catch_1 = "Fishing_Catch_1";
    [SpineAnimation] private string Fishing_Catch_2 = "Fishing_Catch_2";
    [SpineAnimation] private string Fishing_Idle = "Fishing_Ilde";
    [SpineAnimation] private string Fishing_Start = "Fishing_Start";
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
        throw new System.NotImplementedException();
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
    public void Play_Fishing_Open_Bag(bool isLoop = false)
    {
        this.PlayAnim(this.Fishing_Open_Bag, isLoop);
    }

    [Button]
    public void Play_Fishing_Catch_1(bool isLoop = false)
    {
        this.PlayAnim(this.Fishing_Catch_1, isLoop);
    }

    [Button]
    public void Play_Fishing_Catch_2(bool isLoop = false)
    {
        this.PlayAnim(this.Fishing_Catch_2, isLoop);
    }

    [Button]
    public void Play_Fishing_Idle(bool isLoop = false)
    {
        this.PlayAnim(this.Fishing_Idle, isLoop);
    }

    public void SetIsOnMeshRenreder(bool isOn)
    {
        this.skeletonAnimation.GetComponent<MeshRenderer>().enabled = isOn;
    }
}