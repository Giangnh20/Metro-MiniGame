﻿using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public enum EGameState
{
    INIT = 0,
    READY,
    PLAYING,
    PAUSE,
    ENDGAME
}

public interface IGameManager
{
    MinigameData Data { get; }

    ReactiveProperty<EGameState> GameState { get; }

    bool IsPlaying { get; }

    bool InitDone { get; }
    IntReactiveProperty TimeCountdown { get; }
    BoolReactiveProperty TimeOver { get; }

    void RegisterPlayer(IMinigamePlayer player, Vector2 playerPos);

    void SendPlayerMovement(int playerId, Vector2 playerPos, float jumpValue);
}
