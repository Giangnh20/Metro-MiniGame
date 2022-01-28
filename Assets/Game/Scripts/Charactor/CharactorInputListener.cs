using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactorInputListener : MonoBehaviour, IInputListener
{
    private Vector2 _move;
    // Update is called once per frame
    void Update()
    {
        _move.x = Input.GetAxisRaw("Horizontal");
        _move.y = Input.GetAxisRaw("Vertical");
    }

    public Vector2 GetMovementInput()
    {
        return _move;
    }
}
