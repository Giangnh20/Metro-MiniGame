using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class SquidgameUIManager : MonoBehaviour
{
    
    [Button]
    public void ShowPreStartParamsPopup()
    {
        PreStartGameCanvas.Show();
    }
    
    [Button]
    public void HidePreStartParamsPopup()
    {
        PreStartGameCanvas.Hide();
    }
}
