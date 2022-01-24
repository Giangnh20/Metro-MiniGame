using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPopupLoader : Dialog<UIPopupLoader>
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
