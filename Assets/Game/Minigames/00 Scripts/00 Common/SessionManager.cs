using System;
using System.Collections;
using System.Collections.Generic;
using MiniGame.Common;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SessionManager : MonoBehaviour
{
    public bool initDone = false;
    
    private void Awake()
    {
        Debug.LogError(gameObject.name);
        ServiceLocator.Instance.RegisterSingleton(this);
    }

    private IEnumerator Start()
    {
        initDone = false;
        
        // Register singletons
        RegisterServices();
        
        yield return null;
        initDone = true;
        
    }

    private void RegisterServices()
    {
        ServiceLocator.Instance.RegisterSingleton(this);
        ServiceLocator.Instance.RegisterSingleton(new LocalData());
        ServiceLocator.Instance.RegisterSingleton(new UserData());
        ServiceLocator.Instance.Resolve<UserData>().Init();
    }
    
    
}
