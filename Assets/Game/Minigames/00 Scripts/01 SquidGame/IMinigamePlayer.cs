using System;
using System.Collections;
using System.Collections.Generic;
using MiniGame.Common;
using UnityEngine;

public interface IMinigamePlayer
{
    string PlayerId { get; }
    void ForceResetPlayerPosition(Vector2 pos, EForceResetReason reason);
    void WinGame();
}


