using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMove
{
    bool IsMove { get; set; }
    bool Moving { get; set; }
    bool Moved { get; set; }

    bool blockMove { get; set; }
}