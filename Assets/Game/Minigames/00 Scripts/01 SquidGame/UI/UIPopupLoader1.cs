using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPopupLoader1 : Dialog<UIPopupLoader1>
{
    public static void Show()
    {
        Open();
    }

    public static void Hide()
    {
        Close();
    }
    
}
