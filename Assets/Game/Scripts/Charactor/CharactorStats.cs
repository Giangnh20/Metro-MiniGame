using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactorStats
{
    
}

public class Brain
{
    public BrainType BrainType;
    public int[] stats = new int[4];
}

public enum BrainType
{
    None,
    Left,
    Right
}