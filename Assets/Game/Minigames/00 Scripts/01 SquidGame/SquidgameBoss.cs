using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class SquidgameBoss : MonoBehaviour, IMinigameBoss
{
    [SerializeField] private LineRenderer lineLaser;
    [SerializeField] private Transform transLaserStart;
    private Vector3 posLaserStart;
    
    private void Awake()
    {
        posLaserStart = transLaserStart.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        lineLaser.SetPosition(0, posLaserStart);
        lineLaser.SetPosition(1, posLaserStart);
    }


    public async void HitPlayer(Vector3 playerPosition)
    {
        lineLaser.SetPosition(1, playerPosition + new Vector3(0, 0.5f, 0)); // character height
        await UniTask.Delay(200);
        lineLaser.SetPosition(1, posLaserStart);
    }
}
