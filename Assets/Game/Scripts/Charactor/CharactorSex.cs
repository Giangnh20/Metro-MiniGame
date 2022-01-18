using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactorSex : MonoBehaviour
{
    [SerializeField] private ETYPECHARACTOR etypecharactor;

    public ETYPECHARACTOR Etypecharactor
    {
        get => this.etypecharactor;
        set => this.etypecharactor = value;
    }
}