using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public bool IsInShadowMode { get; private set; }

    private void Update()
    {
        if (PlayerInput.Instance.GetSwitchInput())
        {
            IsInShadowMode = !IsInShadowMode;
        }
    }

    
}
