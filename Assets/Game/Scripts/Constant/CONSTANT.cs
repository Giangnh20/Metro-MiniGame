﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CONSTANT
{
    public static T ParseEnum<T>(string value)
    {
        return (T) Enum.Parse(typeof(T), value, true);
    }
}