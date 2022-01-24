using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//[CreateAssetMenu(fileName = "MinigameConfig", menuName = "MetroVerse/MinigameConfig")]
public class MiniGameDataConfig<T> : ScriptableObject
{
    public EGameName GameName;
    public List<T> ConfigItems;
}


[CreateAssetMenu(fileName = "SquidGameDataConfig", menuName = "MetroVerse/SquidGameDataConfig")]
public class SquidGameDataConfig : MiniGameDataConfig<SquidGameData>
{
    
}


[Serializable]
public class MinigameData
{
    public EGameDifficulty Difficulty;
}

[Serializable]
public class SquidGameData : MinigameData
{
    public int TotalTime;
    public int RandomMin;
    public int RandomMax;
    public int KillModeTime;
    public float DelayKillModeTime;

    public SquidGameData(int totalTime, int randomMin, int randomMax, int killModeTime, float delayKillModeTime)
    {
        TotalTime = totalTime;
        RandomMin = randomMin;
        RandomMax = randomMax;
        KillModeTime = killModeTime;
        DelayKillModeTime = delayKillModeTime;
    }
    
}
