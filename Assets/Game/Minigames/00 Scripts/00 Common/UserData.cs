using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData
{
    public string UserId;

    public UserData()
    {
    }

    public void Init()
    {
        // TODO: Load current user data from cache or server...
        // Init dummy data
        UserId = "vietanhva";
    }
    
}
