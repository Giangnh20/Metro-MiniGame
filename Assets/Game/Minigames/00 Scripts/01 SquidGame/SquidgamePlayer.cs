using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SquidgamePlayer : BaseMiniGamePlayer
{
    [SerializeField] private TextMeshProUGUI txtPlayerName;
    
    private IMove _move;
    private IJump _jump;

    protected override void Awake()
    {
        base.Awake();
        _move = GetComponent<IMove>();
        _jump = GetComponent<IJump>();
    }

    protected override void Start()
    {
        base.Start();
        txtPlayerName.text = PlayerId.ToString();
    }

    private void Update()
    {
        if (_move.Moving || _jump.Jumping)
        {
            SendPositionToServer();
        }
            
    }

    private void SendPositionToServer()
    {
        _gameManager.SendPlayerMovement(PlayerId, new Vector2(transform.position.x, transform.position.y), _jump.jumpValue);
    }
}
