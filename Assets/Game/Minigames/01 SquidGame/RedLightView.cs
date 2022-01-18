using System;
using System.Collections;
using System.Collections.Generic;
using MiniGame.Common;
using UniRx;
using UnityEngine;

public class RedLightView : MonoBehaviour
{
    [SerializeField] private GameObject goRed;
    [SerializeField] private GameObject goGreen;

    // Start is called before the first frame update
    void Start()
    {
        ServiceLocator.Instance.Resolve<IGameManager>().GameState.Subscribe(UpdateLightState);
    }

    private void UpdateLightState(EGameState state)
    {
        switch (state)
        {
            case EGameState.INIT:
                goRed.SetActive(false);
                goGreen.SetActive(false);
                break;
            case EGameState.PLAYING:
                goRed.SetActive(false);
                goGreen.SetActive(true);
                break;
            case EGameState.PAUSE:
                goRed.SetActive(true);
                goGreen.SetActive(false);
                break;
        }
    }
}
