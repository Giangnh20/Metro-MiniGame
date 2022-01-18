using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UniRx;
using UnityEngine;

public class TimerManager : IDisposable
{
    public readonly IDisposable dpOneSecondOnMainThread;
    public static Action EverySecondOnMainThread;
    
    public TimerManager()
    {
        dpOneSecondOnMainThread?.Dispose();
        EverySecondOnMainThread = null;

        dpOneSecondOnMainThread = Observable.Timer(TimeSpan.FromSeconds(1), Scheduler.MainThread).Repeat()
            .Subscribe(NextMainThread);
    }

    ~TimerManager()
    {
        Dispose();
    }

    private void NextMainThread(long obj)
    {
        EverySecondOnMainThread?.Invoke();
    }
    
    public void Dispose()
    {
        dpOneSecondOnMainThread?.Dispose();
        EverySecondOnMainThread = null;
    }
}
