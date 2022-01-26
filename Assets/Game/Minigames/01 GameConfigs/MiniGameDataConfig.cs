using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//[CreateAssetMenu(fileName = "MiniGameDataConfig", menuName = "MetroVerse/MiniGameDataConfig")]
[Serializable]
public class MiniGameDataConfig<T> : ScriptableObject
{
    public EGameName GameName;
    public List<T> ConfigItems;
}


[Serializable]
public class MinigameData
{
    public EGameDifficulty Difficulty;
}


