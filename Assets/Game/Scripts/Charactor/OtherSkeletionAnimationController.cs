using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Spine.Unity;
using UnityEngine;

public class OtherSkeletionAnimationController : CharactorSkeletonAnimationController
{
    [SpineSkin] [SerializeField] private string[] skinsName;

    [Button]
    public void SetSkinsByName(string _skinName)
    {
        for (int i = 0; i < skinsName.Length; i++)
        {
            if (skinsName[i].Equals(_skinName))
            {
                base.skeletonAnimation.skeleton.SetSkin(_skinName);
                base.skeletonAnimation.skeleton.SetToSetupPose();
                base.skeletonAnimation.LateUpdate();
            }
        }
    }

    [Button]
    public void SetSkinsByID(int _IDskinName)
    {
        for (int i = 0; i < skinsName.Length; i++)
        {
            if (i == _IDskinName)
            {
                base.skeletonAnimation.skeleton.SetSkin(skinsName[i]);
                base.skeletonAnimation.skeleton.SetToSetupPose();
                base.skeletonAnimation.LateUpdate();
            }
        }
    }
}