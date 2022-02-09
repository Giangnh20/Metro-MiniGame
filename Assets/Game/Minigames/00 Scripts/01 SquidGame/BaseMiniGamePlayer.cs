using System.Collections;
using System.Collections.Generic;
using MiniGame.Common;
using UnityEngine;

public class BaseMiniGamePlayer : MonoBehaviour, IMinigamePlayer
{
    public string PlayerId => _playerId;
    protected string _playerId;
    
    protected IGameManager _gameManager;

    protected virtual void InitPlayerId()
    {
        _playerId = this.GetHashCode().ToString();                    //TODO: For temporary
    }
    
    protected virtual void Awake()
    {
        InitPlayerId();
    }
    
    protected virtual void Start()
    {
        _gameManager = ServiceLocator.Instance.Resolve<IGameManager>();
        _gameManager.RegisterPlayer(this, transform.position);         //TODO: Request join in game
    }

    public virtual void ForceResetPlayerPosition(Vector2 pos, EForceResetReason reason)
    {
        transform.position = pos;
    }

    public virtual void WinGame()
    {
        
    }
    
}
