﻿using System;
using System.Collections;
using System.Collections.Generic;
using MiniGame.Common;
using UnityEngine;

public interface IMinigamePlayer
{
    string PlayerId { get; }
    void ForceResetPlayerPosition(Vector2 pos);
}

public class BaseMiniGamePlayer : MonoBehaviour, IMinigamePlayer
{
    public string PlayerId => _playerId;
    private string _playerId;
    
    protected IGameManager _gameManager;

    protected virtual void Awake()
    {
        _playerId = this.GetHashCode().ToString();                    //TODO: For temporary
    }

    protected virtual void Start()
    {
        _gameManager = ServiceLocator.Instance.Resolve<IGameManager>();
        _gameManager.RegisterPlayer(this, transform.position);         //TODO: Request join in game
    }

    public void ForceResetPlayerPosition(Vector2 pos)
    {
        transform.position = pos;
    }
    
}
