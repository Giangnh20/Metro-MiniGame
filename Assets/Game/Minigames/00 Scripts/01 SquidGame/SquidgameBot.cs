using System;
using System.Collections;
using System.Collections.Generic;
using MiniGame.Common;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

public class SquidgameBot : MonoBehaviour, IInputListener
{
    private IGameManager _gameManager;
    private IDisposable _disposable;

    private Vector2 _movementVal;

    private Vector2 horizontalEdge;
    private Vector2 verticalEdge;
    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = ServiceLocator.Instance.Resolve<IGameManager>();
        var squidgameManager = (SquidgameGameManager) _gameManager;
        horizontalEdge = new Vector2(squidgameManager.tranTopLeft.position.x, squidgameManager.tranBotRight.position.x);
        verticalEdge = new Vector2(squidgameManager.tranBotRight.position.y, squidgameManager.tranTopLeft.position.y);
        _disposable = _gameManager.GameState.Subscribe(OnChangeGameState);
    }

    private void OnDestroy()
    {
        _disposable?.Dispose();
    }

    private void FixedUpdate()
    {
        if (transform.position.x <= horizontalEdge.x || transform.position.x >= horizontalEdge.y)
            _movementVal.x *= -1;
//        else if (transform.position.x >= horizontalEdge.y) 
//        {
//            
//        }
            
        if (transform.position.y <= verticalEdge.x || transform.position.y >= verticalEdge.y)
            _movementVal.y *= -1;
    }

    private void OnChangeGameState(EGameState state)
    {
        switch (state)
        {
            case EGameState.PLAYING:
                if (ShouldGo(30))
                {
                    float x = Random.Range(-0.2f, 0.5f);
                    float y = Random.Range(-0.2f, 0.2f);
                    _movementVal = new Vector2(x, y);
                }
                break;
            case EGameState.PAUSE:
                _movementVal = new Vector2(0f, 0f);
                break;
            default:
                _movementVal = new Vector2(0f, 0f);
                break;
        }
    }

    public Vector2 GetMovementInput()
    {
        return _movementVal;
    }

    bool ShouldGo(int percent)
    {
        var rd = Random.Range(0, percent);
        return rd <= percent;
    }
}
