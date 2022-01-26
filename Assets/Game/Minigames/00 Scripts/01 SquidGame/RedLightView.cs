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

    private IGameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = ServiceLocator.Instance.Resolve<IGameManager>();
        _gameManager.GameState.Subscribe(UpdateLightState);
    }

    private void UpdateLightState(EGameState state)
    {
        switch (state)
        {
            case EGameState.PLAYING:
                if (!_gameManager.IsPlaying)
                    return;
                goRed.SetActive(false);
                goGreen.SetActive(true);
                break;
            case EGameState.PAUSE:
                if (!_gameManager.IsPlaying)
                    return;
                goRed.SetActive(true);
                goGreen.SetActive(false);
                break;
            default:
                goRed.SetActive(false);
                goGreen.SetActive(false);
                break;
        }
    }
}
