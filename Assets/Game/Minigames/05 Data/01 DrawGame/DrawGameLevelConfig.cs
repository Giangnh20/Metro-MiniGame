using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "GameConfigs/DrawGameLevel", fileName = "DrawGameLevelConfig")]
public class DrawGameLevelConfig : ScriptableObject
{
    public float ZoomMin = 1;
    public float ZoomMax = 2;
    public float ZoomStepMouse = 0.05f;
    public float ZoomStepButton = 0.1f;
    public GameObject LevelPrefab;
    public List<Color> PremiumColors;
}
