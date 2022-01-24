using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class PlayerServerData
{
    public IMinigamePlayer PlayerView;
    public int PlayerId;
    public Vector2 Position;
    public float JumpValue;
    public BoolReactiveProperty IsWinner;
    public int WinTime;

    public PlayerServerData(IMinigamePlayer playerView, int playerId, Vector2 position, float jumpValue)
    {
        PlayerView = playerView;
        PlayerId = playerId;
        Position = position;
        JumpValue = jumpValue;
        IsWinner = new BoolReactiveProperty();
        WinTime = -1;
    }
}
