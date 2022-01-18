using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactorMovement : MonoBehaviour, IMove
{
    Rigidbody2D body;
    private Vector2 move = Vector2.zero;
    public float runSpeed = 10.0f;
    private bool m_FacingRight = true;
    private IJump ijump;
    public bool IsMove { get; set; }
    public bool Moving { get; set; }
    public bool Moved { get; set; }
    public bool blockMove { get; set; }

    private CharactorSkinManager charactorSkinManager;

    private CharactorVehicleController charactorVehicleController;

    private CharactorWingController charactorWingController;

    private CharactorTailController charactorTailController;

    private void Start()
    {
        this.charactorVehicleController = this.GetComponent<CharactorVehicleController>();
        this.charactorWingController = this.GetComponent<CharactorWingController>();
        this.charactorTailController = this.GetComponent<CharactorTailController>();
        this.charactorSkinManager = this.GetComponent<CharactorSkinManager>();
        ijump = GetComponent<IJump>();
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        move.x = Input.GetAxisRaw("Horizontal");
        move.y = Input.GetAxisRaw("Vertical");
        CheckFlip();
        CheckPlayAnim();
    }

    private void FixedUpdate()
    {
        if (this.blockMove)
        {
            return;
        }

        this.body.position += move * (runSpeed * Time.deltaTime);
    }

    private void CheckPlayAnim()
    {
        if (!ijump.Jumping)
        {
            if (move.sqrMagnitude > 0f)
            {
                if (!this.IsMove)
                {
                    this.IsMove = true;
                    MovingExtension();
                }

                this.IsMove = true;
                this.Moving = true;
            }
            else
            {
                if (this.IsMove)
                {
                    this.IsMove = false;
                    IdleExtension();
                }

                this.Moving = false;
            }
        }
        else
        {
            this.IsMove = false;
            this.Moving = false;
        }
    }

    public void IdleExtension()
    {
        if (!this.charactorVehicleController.HaveVehicle)
        {
            if (!this.charactorWingController.HaveWing)
            {
                this.charactorSkinManager.CharactorSkeletonAnimationController.PlayIdle(true);
                this.charactorSkinManager.OtherSkeletionAnimationController[0].PlayFly();
                if (this.charactorTailController.HaveTail)
                {
                    this.charactorSkinManager.OtherSkeletionAnimationController[1].PlayIdle(true);
                }
            }
            else
            {
                this.charactorSkinManager.CharactorSkeletonAnimationController.PlayFly(true);
                if (this.charactorTailController.HaveTail)
                {
                    this.charactorSkinManager.OtherSkeletionAnimationController[1].PlayFly(true);
                }
            }
        }
        else
        {
            switch (this.charactorVehicleController.EVehicle)
            {
                case EVehicle.none:
                    break;
                case EVehicle.Car:
                    this.charactorSkinManager.CharactorSkeletonAnimationController.PlayDriveCar(true);
                    if (this.charactorTailController.HaveTail)
                    {
                        this.charactorSkinManager.OtherSkeletionAnimationController[1].PlayDriveCar(true);
                    }

                    break;
                case EVehicle.Motor:
                    this.charactorSkinManager.CharactorSkeletonAnimationController.PlayDriveMotor(true);
                    if (this.charactorTailController.HaveTail)
                    {
                        this.charactorSkinManager.OtherSkeletionAnimationController[1].PlayDriveMotor(true);
                    }

                    break;
                case EVehicle.Skate:
                    this.charactorSkinManager.CharactorSkeletonAnimationController.PlayDriveSkate(true);
                    if (this.charactorTailController.HaveTail)
                    {
                        this.charactorSkinManager.OtherSkeletionAnimationController[1].PlayDriveSkate(true);
                    }

                    break;
                default:
                    break;
            }
        }
    }

    public void MovingExtension()
    {
        if (!this.charactorVehicleController.HaveVehicle)
        {
            if (!this.charactorWingController.HaveWing)
            {
                this.charactorSkinManager.CharactorSkeletonAnimationController.PlayWalk(true);
            }
            else
            {
                this.charactorSkinManager.CharactorSkeletonAnimationController.PlayFly(true);
                this.charactorSkinManager.OtherSkeletionAnimationController[0].PlayFly();
            }
        }
        else
        {
            switch (this.charactorVehicleController.EVehicle)
            {
                case EVehicle.none:
                    break;
                case EVehicle.Car:
                    this.charactorSkinManager.CharactorSkeletonAnimationController.PlayDriveCar(true);
                    if (this.charactorWingController.HaveWing)
                    {
                        this.charactorSkinManager.OtherSkeletionAnimationController[0].PlayDriveCar(true);
                    }

                    if (this.charactorTailController.HaveTail)
                    {
                        this.charactorSkinManager.OtherSkeletionAnimationController[1].PlayDriveCar(true);
                    }

                    break;
                case EVehicle.Motor:
                    this.charactorSkinManager.CharactorSkeletonAnimationController.PlayDriveMotor(true);
                    if (this.charactorWingController.HaveWing)
                    {
                        this.charactorSkinManager.OtherSkeletionAnimationController[0].PlayDriveMotor(true);
                    }

                    if (this.charactorTailController.HaveTail)
                    {
                        this.charactorSkinManager.OtherSkeletionAnimationController[1].PlayDriveMotor(true);
                    }

                    break;
                case EVehicle.Skate:
                    this.charactorSkinManager.CharactorSkeletonAnimationController.PlayDriveSkate(true);
                    if (this.charactorWingController.HaveWing)
                    {
                        this.charactorSkinManager.OtherSkeletionAnimationController[0].PlayDriveSkate(true);
                    }

                    if (this.charactorTailController.HaveTail)
                    {
                        this.charactorSkinManager.OtherSkeletionAnimationController[1].PlayDriveSkate(true);
                    }

                    break;
                default:
                    break;
            }
        }
    }

    private void CheckFlip()
    {
        if (move.x > 0f && !m_FacingRight)
        {
            Flip();
        }
        else if (move.x < 0 && m_FacingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;
        // Multiply the player's x local scale by -1.
        var transform1 = this.charactorSkinManager.CharactorSkeletonAnimationController.transform;
        Vector3 theScale = transform1.localScale;
        theScale.x *= -1;
        transform1.localScale = theScale;
    }
}