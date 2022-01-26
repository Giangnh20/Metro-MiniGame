using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MiniGame.Common;
using UniRx;
using UnityEngine;

public abstract class BaseGameManager<T> : MonoBehaviour, IGameManager where T : MinigameData
{
    public EGameName GameName;
    
    protected EGameState gameState;
    private TimerManager _timerManager;

    protected Dictionary<int, PlayerServerData> players;
    private IDisposable disposable;
    
    private MiniGameDataConfig<T> minigameConfigs;

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


    public MinigameData Data { get; private set; }
    public T GameData => Data as T;
    public ReactiveProperty<EGameState> GameState { get; protected set; }
    public bool IsPlaying { get; protected set; }

    protected virtual void Initialize()
    {
        InitDone = false;
        
        // Read game Data
        LoadPreStartGameData();
        
        GameState = new ReactiveProperty<EGameState>(EGameState.INIT);
        _timerManager = new TimerManager();
        players = new Dictionary<int, PlayerServerData>();
        ChangeGameState(EGameState.INIT);
        
        TimeCountdown = new IntReactiveProperty();
        TimeOver = new BoolReactiveProperty();
        disposable = TimeCountdown.Subscribe(t => TimeOver.Value = t <= 0);
        InitDone = true;
    }

    protected virtual void LoadPreStartGameData()
    {
        var preStart = ServiceLocator.Instance.Resolve<LocalData>().PreStartGameData;
        minigameConfigs = Resources.Load($"{GameName}DataConfig") as MiniGameDataConfig<T>;
        try
        {
            Data = minigameConfigs.ConfigItems.FirstOrDefault(c => c.Difficulty == preStart.Difficulty);
            if (Data == null)
                throw new NullReferenceException($"{preStart.Difficulty} data not found!");
        }
        catch
        {
            throw new NullReferenceException("Gameconfig not found!");
        }
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
