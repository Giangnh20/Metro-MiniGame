using System;
using System.Collections;
using System.Collections.Generic;
using MiniGame.Common;
using UniRx;
using UnityEngine;

public abstract class BaseGameManager : MonoBehaviour, IGameManager
{
    protected EGameState gameState;
    private TimerManager _timerManager;

    protected Dictionary<int, PlayerServerData> players;
    private IDisposable disposable;
    
    protected virtual void Awake()
    {
        InitDone = false;
        ServiceLocator.Instance.RegisterSingleton<IGameManager>(this);
        Initialize();
    }
    
    // Start is called before the first frame update
    protected virtual void Start()
    {
    }


    public abstract IGameData Data { get; }
    public ReactiveProperty<EGameState> GameState { get; protected set; }

    public virtual void Initialize()
    {
        InitDone = false;
        GameState = new ReactiveProperty<EGameState>(EGameState.INIT);
        _timerManager = new TimerManager();
        players = new Dictionary<int, PlayerServerData>();
        ChangeGameState(EGameState.INIT);
        
        TimeCountdown = new IntReactiveProperty();
        TimeOver = new BoolReactiveProperty();
        disposable = TimeCountdown.Subscribe(t => TimeOver.Value = t <= 0);
        InitDone = true;
    }

    public bool InitDone { get; protected set; }
    public IntReactiveProperty TimeCountdown { get; private set; }
    public BoolReactiveProperty TimeOver { get; private set;}

    public void RegisterPlayer(IMinigamePlayer player, Vector2 playerPos)
    {
        if (!players.ContainsKey(player.PlayerId))
        {
            PlayerServerData tmpPlayer = new PlayerServerData(player, player.PlayerId, playerPos, 0);
            players.Add(tmpPlayer.PlayerId, tmpPlayer);
        }
    }
    public void SendPlayerMovement(int playerId, Vector2 playerPos, float jumpValue)
    {
        if (players.ContainsKey(playerId))
        {
            var player = players[playerId];
            player.Position = playerPos;
            player.JumpValue = jumpValue;
            ProcessPlayerMovement(player);
        }
    }

    protected virtual void ProcessPlayerMovement(PlayerServerData player)
    {
//        Debug.Log($"Player {player.PlayerId} move to {player.Position}, jump {player.JumpValue}");
    }

    protected void ChangeGameState(EGameState state)
    {
        gameState = state;
        GameState.Value = state;
    }

    
    protected virtual void OnDestroy()
    {
        _timerManager?.Dispose();
        disposable?.Dispose();
        GC.SuppressFinalize(this);
    }
}
