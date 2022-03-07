using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameConfigs/DrawGameLevel", fileName = "DrawGameLevelConfig")]
public class DrawGameLevelConfig : ScriptableObject
{
    public GameObject LevelPrefab;
    public List<Sprite> PremiumColors;

    public List<Color> GetPremiumColors()
    {
        List<Color> result = new List<Color>();
        foreach (var sprite in PremiumColors)
        {
            var color = ColorPickerHelper.GetPixelColor(sprite);
            if (!result.Contains(color))
                result.Add(color);
        }

        return result;
    }
    
}
