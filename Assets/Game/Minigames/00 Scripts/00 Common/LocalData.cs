using System.Collections;
using System.Collections.Generic;
using MiniGame.Common;
using UnityEngine;

public class LocalData
{
    public bool IsInitDone;
    public PreStartGameData PreStartGameData;
    
    public LocalData()
    {
        Init();
    }

    private void Init()
    {
        if (IsInitDone)
            return;
        IsInitDone = true;
    }
    
}
