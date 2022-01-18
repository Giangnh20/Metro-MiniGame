using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IJump
{
    float jumpValue { get; set; }
    bool IsJump { get; set; }
    bool Jumping { get; set; }

    bool Jumped { get; set; }

    void OnJump();
}