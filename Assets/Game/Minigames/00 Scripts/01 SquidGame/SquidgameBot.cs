using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using MiniGame.Common;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

public class SquidgameBot : SquidgamePlayer, IInputListener
{
    private IDisposable _disposable;

    private Vector2 _movementVal;

    private Vector2 horizontalEdge;
    private Vector2 verticalEdge;
    private Vector2 spawnRangeX;
    private Vector2 spawnRangeY;
    private EGameState _state;

    private bool shouldRun;
    private bool isWin;

    private Vector3 destination;
    private Vector3 direction;
    private bool isDead;
    

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        var squidgameManager = (SquidgameGameManager) _gameManager;
        horizontalEdge = new Vector2(squidgameManager.tranTopLeft.position.x, squidgameManager.tranBotRight.position.x);
        verticalEdge = new Vector2(squidgameManager.tranBotRight.position.y, squidgameManager.tranTopLeft.position.y);
        spawnRangeX = squidgameManager.spawnRangeX;
        spawnRangeY = squidgameManager.spawnRangeY;
        _disposable = _gameManager.GameState.Subscribe(OnChangeGameState);
        
        // initial data
        direction = GetRandomDirection();
        shouldRun = ShouldGo(50);
        isDead = false;
        isWin = false;
    }

    private void OnDestroy()
    {
        _disposable?.Dispose();
    }
    
    public override void ForceResetPlayerPosition(Vector2 pos, EForceResetReason reason)
    {
        base.ForceResetPlayerPosition(pos, reason);
        if (reason == EForceResetReason.DEAD)
        {
            isDead = true;
            shouldRun = false;
        }
    }

    public override void WinGame()
    {
        isWin = true;
    }
    

    private async void OnChangeGameState(EGameState state)
    {
        _state = state;
        isDead = false;
        switch (state)
        {
            case EGameState.INIT:
                shouldRun = ShouldGo(50);
                break;
            case EGameState.PLAYING:
                await GetRandomDelayTime();
                if (state == EGameState.PLAYING)
                    shouldRun = ShouldGo(40);
                break;
            case EGameState.PAUSE:
                shouldRun = ShouldGo(25);
                break;
            default:
                shouldRun = false;
                break;
        }
    }

    /// <summary>
    /// Run in loop Update
    /// </summary>
    /// <returns></returns>
    public Vector2 GetMovementInput()
    {
        if (isWin)
        {
            // TODO: 
            return Vector2.zero;
        }
        
        if (shouldRun && !isDead)
        {
            if (Vector2.Distance(transform.position, destination) <= 0.2f
                || transform.position.x <= horizontalEdge.x || transform.position.x >= horizontalEdge.y
                || transform.position.y <= verticalEdge.x || transform.position.y >= verticalEdge.y)
            {
                direction = GetRandomDirection();            
            }
            return direction;
        }
        else
        {
            return Vector2.zero;
        }
    }


    private Vector3 GetRandomDirection()
    {
        destination = GetRandomDestination();
        var tmpDirection = (destination - transform.position).normalized;
        float speed = Random.Range(0.5f, 0.8f);
        tmpDirection = tmpDirection * speed;
        return tmpDirection;
    }
    
    private Vector3 GetRandomDestination()
    {
        float x = 0f;
        float y = 0f;
        if (_state == EGameState.INIT || _state == EGameState.READY)
        {
            x = Random.Range(spawnRangeX.x, spawnRangeX.y);
            y = Random.Range(spawnRangeY.x, spawnRangeY.y);
        }
        else
        {
            x = Random.Range(horizontalEdge.x, horizontalEdge.y);
            y = Random.Range(verticalEdge.x, verticalEdge.y);
        }
        return new Vector3(x, y, 0);
    }

    private UniTask GetRandomDelayTime()
    {
        return UniTask.Delay(Random.Range(400, 1000));
    }

    bool ShouldGo(int percent)
    {
        var rd = Random.Range(0, 100);
        return rd <= percent;
    }
}
