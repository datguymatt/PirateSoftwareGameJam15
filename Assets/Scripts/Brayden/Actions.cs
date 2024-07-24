using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Actions
{
    public static Action OnPlayerModeChange;

    public static Action<int> OnPlayerAttacked;

    public static Action OnPlayerDied;

    public static Action OnPlayerSpawned;
}
