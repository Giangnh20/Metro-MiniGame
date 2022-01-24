using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using MiniGame.Common;
using TMPro;
using UniRx;
using UnityEngine;

public class SquidgameGameTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtGameState;
    private TextMeshProUGUI txtCountdown;
    
    private IDisposable disposable;
    private IDisposable dpState;

    private IGameManager gameManager;
    

    private void Awake()
    {
        txtCountdown = GetComponent<TextMeshProUGUI>();
    }

    // Start is called before the first frame update
    async void Start()
    {
        gameManager = ServiceLocator.Instance.Resolve<IGameManager>();
        disposable = gameManager.TimeCountdown.Subscribe(UpdateTimer);
        dpState = gameManager.GameState.Subscribe(_ => { txtGameState.text = _.ToString(); });
    }

    private void OnDestroy()
    {
        disposable?.Dispose();
        dpState?.Dispose();
    }

    private void UpdateTimer(int totalTime)
    {
        txtCountdown.text = $"{totalTime / 60:00}:{totalTime % 60:00}";
    }
    
    
    
    
}
