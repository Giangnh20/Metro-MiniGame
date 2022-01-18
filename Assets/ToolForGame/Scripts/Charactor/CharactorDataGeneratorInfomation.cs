using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "CharactorDataGenerator", menuName = "MyGame/CharactorDataGeneratorInfomation")]
[System.Serializable]
public class CharactorDataGeneratorInfomation : ScriptableObject
{
    public ETYPECHARACTOR Etypecharactor;
    public ESKINCOLOR Eskincolor;
    public string Body;
    public string[] BodyComponent;
    public string Face;
    public string Hair;
    public string HairLong;
    public string Top;
    public string[] TopComponent;
    public string Bottom;
    public string[] BottomComponent;
    public string shoes;
    public string Accessories;
    public string Vehicle;
    public string wings;
    public string tail;
}

[System.Serializable]
public class CharactorDataGeneratorInfomationConvertToSprite
{
    public ETYPECHARACTOR Etypecharactor;
    public ESKINCOLOR Eskincolor;
    public Sprite Body;
    public Sprite[] BodyComponent;
    public Sprite Face;
    public Sprite Hair;
    public Sprite HairLong;
    public Sprite Top;
    public Sprite[] TopComponent;
    public Sprite Bottom;
    public Sprite[] BottomComponent;
    public Sprite[] Shoes;
    public Sprite Accessories;
    public Sprite Vehicle;
    public Sprite Wing;
    public Sprite Tail;
}