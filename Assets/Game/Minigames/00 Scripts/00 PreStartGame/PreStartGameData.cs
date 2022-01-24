using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreStartGameData
{
    public EGameName GameName;
    public EGameDifficulty Difficulty;
    public int PlayerNum;

    public PreStartGameData(EGameName gameName, EGameDifficulty difficulty, int playerNum)
    {
        GameName = gameName;
        Difficulty = difficulty;
        PlayerNum = playerNum;
    }
}
