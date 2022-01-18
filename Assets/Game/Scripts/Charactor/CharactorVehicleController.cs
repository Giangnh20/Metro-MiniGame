using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class CharactorVehicleController : MonoBehaviour, IVehicle
{
    [SerializeField] private bool haveVehicle = false;

    private CharactorSkinManager charactorSkinManager;

    private void Start()
    {
        this.charactorSkinManager = this.GetComponent<CharactorSkinManager>();
    }

    public bool HaveVehicle
    {
        get { return this.haveVehicle; }
        set { this.haveVehicle = value; }
    }

    [SerializeField] private EVehicle eVehicle = EVehicle.none;

    public EVehicle EVehicle
    {
        get { return this.eVehicle; }
        set { this.eVehicle = value; }
    }

    public void SetHaveVehicle(bool have)
    {
        this.HaveVehicle = have;
    }

    public void SetCurVehicle(EVehicle _eVehicle)
    {
        this.EVehicle = _eVehicle;
    }

    [Button]
    public void EquipVehicle()
    {
        this.charactorSkinManager.DataChangeSkin.SelectVehicle(this.EVehicle);
    }
}