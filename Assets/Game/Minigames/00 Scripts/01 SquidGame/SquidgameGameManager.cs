using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using MiniGame.Common;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

public class SquidgameGameManager : BaseGameManager<SquidGameData>
{
    [SerializeField] private SquidgameBoss gameBoss;
    
    [Header("Game Configs")] 
    [SerializeField] private float destinationX;
    [SerializeField] public Vector2 spawnRangeX;
    [SerializeField] public Vector2 spawnRangeY;
    public Transform tranTopLeft;
    public Transform tranBotRight;

    
    
    private bool shouldCount;
    private IDisposable disposable;
    private CompositeDisposable dpGameplays = new CompositeDisposable();
    private BoolReactiveProperty IsAllPlayerWinner;
    private bool isKillModeOn;
    
    
    
    protected override void Initialize()
    {
        base.Initialize();
        IsPlaying = false;
    }
    

    protected override void Start()
    {
        base.Start();
        TimeCountdown.Value = GameData.TotalTime;
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
                    player.WinTime = TimeCountdown.Value;
                    player.IsWinner.Value = true;
                    player.PlayerView.WinGame();
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
            KillPlayer(player);
        }
    }

    public async void KillPlayer(PlayerServerData player)
    {
        gameBoss.HitPlayer(player.Position);
        await UniTask.Delay(200);
        ForceResetPlayerPosition(player.PlayerId, EForceResetReason.DEAD);
    } 
    
    private void ForceResetPlayerPosition(string playerId, EForceResetReason reason)
    {
        if (players.ContainsKey(playerId))
        {
            var rebornPosition = new Vector2(Random.Range(spawnRangeX.x, spawnRangeX.y), Random.Range(spawnRangeY.x, spawnRangeY.y));
            players[playerId].Position = rebornPosition;
            players[playerId].PlayerView.ForceResetPlayerPosition(rebornPosition, reason);
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
        IsPlaying = true;
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
        int playTime = UnityEngine.Random.Range(GameData.RandomMin, GameData.RandomMax);
        await UniTask.Delay(TimeSpan.FromSeconds(playTime));
        ChangeGameState(EGameState.PAUSE);
    }

    private async void GameCheck()
    {
        if (!IsPlaying) 
            return;
        await UniTask.Delay(TimeSpan.FromSeconds(GameData.DelayKillModeTime));
        isKillModeOn = true;
        await UniTask.Delay(TimeSpan.FromSeconds(GameData.KillModeTime));
        isKillModeOn = false;
        ChangeGameState(EGameState.PLAYING);
    }

    private void GameEnd()
    {
        var listPlayers = players.Values.ToList();
        
        IsPlaying = false;
        var myUserId = ServiceLocator.Instance.Resolve<UserData>().UserId;
        var isWinner = players[myUserId].IsWinner.Value;
        UIPopupEndgameCanvas.Show();
        UIPopupEndgameCanvas.Instance.Populate(new EndGameScreenData(isWinner, listPlayers));
    }
}
