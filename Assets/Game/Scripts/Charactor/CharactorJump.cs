using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class CharactorJump : MonoBehaviour, IJump
{
    [SerializeField] private float timeJumpValue;

    [SerializeField] private float yLocalValue = 2f;

    [SerializeField] private Transform skeleton;

    private CharactorSkinManager charactorSkinManager;

    private float ySaveValue = 0;


    public float jumpValue
    {
        get => this.timeJumpValue;
        set => timeJumpValue = value;
    }

    public bool IsJump { get; set; }
    public bool Jumping { get; set; }
    public bool Jumped { get; set; }

    private void Start()
    {
        this.charactorSkinManager = this.GetComponent<CharactorSkinManager>();
        this.ySaveValue = this.skeleton.transform.localPosition.y;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            OnJump();
        }
    }

    [Button]
    public void OnJump()
    {
        if (Jumping)
        {
            return;
        }

        Jumping = true;
        if (!this.IsJump)
        {
            this.IsJump = true;
            this.charactorSkinManager.CharactorSkeletonAnimationController.PlayJump(false);
            this.StartCoroutine(this.IEJump());
        }
    }

    private IEnumerator IEJump()
    {
        Debug.Log("jump");
        this.GetComponent<IMove>().blockMove = true;
        yield return new WaitForSeconds(0.25f);
        this.GetComponent<IMove>().blockMove = false;
        this.skeleton.transform.DOLocalMoveY(yLocalValue, timeJumpValue).SetLoops(2, LoopType.Yoyo).OnComplete(
            delegate
            {
                this.Jumping = false;
                this.IsJump = false;
                Vector3 pos = new Vector3(this.skeleton.transform.localPosition.x,
                    this.ySaveValue, this.skeleton.transform.localPosition.z);
                this.skeleton.transform.localPosition = pos;
                //this.charactorSkinManager.CharactorSkeletonAnimationController.PlayWalk(false);
            });
    }
}