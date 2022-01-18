using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherSpriteController : CharactorSpriteController
{
    private OtherSkeletionAnimationController _otherSkeletionAnimationController;

    public OtherSkeletionAnimationController OtherSkeletionAnimationController
    {
        get
        {
            if (this._otherSkeletionAnimationController == null)
            {
                this._otherSkeletionAnimationController =
                    this.GetComponentInChildren<OtherSkeletionAnimationController>();
            }

            return this._otherSkeletionAnimationController;
        }
    }

    public override void IsOnSprite(bool isOn)
    {
        OtherSkeletionAnimationController.GetComponent<MeshRenderer>().enabled = isOn;
    }

    public override void SetOrderID(int value)
    {
        OtherSkeletionAnimationController.skeletonAnimation.GetComponent<MeshRenderer>().sortingOrder = value;
    }

    public override void SetSkinSkeletonByID(string _skinname)
    {
        OtherSkeletionAnimationController.SetSkinsByName(_skinname);
    }
}