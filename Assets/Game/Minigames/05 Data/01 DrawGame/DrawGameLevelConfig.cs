using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "GameConfigs/DrawGameLevel", fileName = "DrawGameLevelConfig")]
public class DrawGameLevelConfig : ScriptableObject
{
    public GameObject LevelPrefab;
    public List<Color> PremiumColors;
}
