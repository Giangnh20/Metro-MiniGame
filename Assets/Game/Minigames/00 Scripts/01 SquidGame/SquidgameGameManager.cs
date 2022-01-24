using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

//public class SquidGameData : IGameData
//{
//    public int TotalTime;
//    public int RandomMin;
//    public int RandomMax;
//    public float DestinationX;
//    public int KillModeTime;
//    public float DelayKillModeTime;
//
//    public SquidGameData(int totalTime, int randomMin, int randomMax, float destinationX, int killModeTime, float delayKillModeTime)
//    {
//        TotalTime = totalTime;
//        RandomMin = randomMin;
//        RandomMax = randomMax;
//        DestinationX = destinationX;
//        KillModeTime = killModeTime;
//        DelayKillModeTime = delayKillModeTime;
//    }
//}

public class SquidgameGameManager : BaseGameManager<SquidGameData>
{
    [Header("Game Configs")] 
    [SerializeField] private int totalTime;        // in seconds
    [SerializeField] private int randomMin;        // in seconds
    [SerializeField] private int randomMax;        // in seconds
    [SerializeField] private int killModeTime;    // in seconds
    [SerializeField] private float delayKillModeTime;
    
    [SerializeField] private float destinationX;
    [SerializeField] private Vector2 spawnRangeX;
    [SerializeField] private Vector2 spawnRangeY;

    private SquidGameData data => Data as SquidGameData;
    private bool shouldCount;
    private IDisposable disposable;
    private CompositeDisposable dpGameplays = new CompositeDisposable();
    private BoolReactiveProperty IsAllPlayerWinner;
    private bool isKillModeOn;
    private bool IsPlaying => gameState == EGameState.PAUSE || gameState == EGameState.PLAYING;
    
    protected override void Awake()
    {
        base.Awake();
        // For temporary, it should be configured by Scriptable Object
//        data = new SquidGameData(totalTime, randomMin, randomMax, killModeTime, delayKillModeTime);    
    }

    

    protected override void Start()
    {
        base.Start();
        TimeCountdown.Value = data.TotalTime;
        shouldCount = false;
        IsAllPlayerWinner = new BoolReactiveProperty();
        isKillModeOn = false;
        TimerManager.EverySecondOnMainThread += UpdateTick;
        disposable = GameState.Subscribe(OnGameStateChanged);
        //
        FakeGameFlow();
    }

    


    protected override void OnDestroy()
    {
        base.OnDestroy();
        TimerManager.EverySecondOnMainThread -= UpdateTick;
        disposable?.Dispose();
    }

    private void FixedUpdate()
    {
        if (gameState == EGameState.PLAYING)
        {
            foreach (var player in players.Values)
            {
                if (!player.IsWinner.Value && player.Position.x >= destinationX)
                {
                    player.IsWinner.Value = true;
                    player.WinTime = TimeCountdown.Value;
                }
            }
        }
    }

    protected override void ProcessPlayerMovement(PlayerServerData player)
    {
        base.ProcessPlayerMovement(player);
        if (isKillModeOn && !player.IsWinner.Value)
        {
            Debug.LogError($"<color='magenta'>KILL PLAYER: {player.PlayerId}</color>");
            ForceResetPlayerPosition(player.PlayerId);
        }
    }

    private void ForceResetPlayerPosition(int playerId)
    {
        if (players.ContainsKey(playerId))
        {
            var rebornPosition = new Vector2(Random.Range(spawnRangeX.x, spawnRangeX.y), Random.Range(spawnRangeY.x, spawnRangeY.y));
            players[playerId].Position = rebornPosition;
            players[playerId].PlayerView.ForceResetPlayerPosition(rebornPosition);
        }
    }

    private void UpdateTick()
    {
        if (!shouldCount || TimeCountdown.Value <= 0)
            return;
        
        --TimeCountdown.Value;
    }
    
    private void OnGameStateChanged(EGameState state)
    {
        Debug.Log("GameState change to: " + state);
        shouldCount = state == EGameState.PLAYING;
//        isKillModeOn = state == EGameState.PAUSE;
        switch (state)
        {
            case EGameState.READY:
                GameReady();
                break;
            case EGameState.PLAYING:
                GamePlay();
                break;
            case EGameState.PAUSE:
                GameCheck();
                break;
            case EGameState.ENDGAME:
                GameEnd();
                break;
        }
    }

    async void FakeGameFlow()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(5));
        ChangeGameState(EGameState.READY);
        
        await UniTask.Delay(TimeSpan.FromSeconds(3));
        ChangeGameState(EGameState.PLAYING);
    }

    private void GameReady()
    {
        players.Values.ToList().Select(p => p.IsWinner).CombineLatest().Subscribe(winners => IsAllPlayerWinner.Value = winners.All(x => x)).AddTo(dpGameplays);
        // ReactiveEndGame
        IsAllPlayerWinner.CombineLatest(TimeOver, (win, timeOver)=> win || timeOver).Subscribe(ended =>
        {
            if (ended) ChangeGameState(EGameState.ENDGAME);
        }).AddTo(dpGameplays);
    }

    private async void GamePlay()
    {
        if (!IsPlaying) 
            return;
        int playTime = UnityEngine.Random.Range(data.RandomMin, data.RandomMax);
        await UniTask.Delay(TimeSpan.FromSeconds(playTime));
        ChangeGameState(EGameState.PAUSE);
    }

    private async void GameCheck()
    {
        if (!IsPlaying) 
            return;
        await UniTask.Delay(TimeSpan.FromSeconds(data.DelayKillModeTime));
        isKillModeOn = true;
        await UniTask.Delay(TimeSpan.FromSeconds(data.KillModeTime));
        isKillModeOn = false;
        ChangeGameState(EGameState.PLAYING);
    }

    private void GameEnd()
    {
        if (IsAllPlayerWinner.Value)
        {
            Debug.LogError("All players are winner");
        }
        else
        {
            var winners = players.Values.Where(p => p.IsWinner.Value).ToList();
            if (winners.Count > 0)
            {
                // Get top 3 winners
                var top3 = winners.OrderByDescending(w => w.WinTime).ToList().GetRange(0, Math.Min(winners.Count, 3));
                string outString = string.Join(",", top3);
                Debug.LogError("WINNERS: " + outString);
            }
            else
            {
                Debug.LogError("All LOSE~~~~");
            }
        }
        //TODO: Update UI
    }
}
