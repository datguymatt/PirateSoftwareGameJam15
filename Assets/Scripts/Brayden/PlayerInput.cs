using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput Instance { get; private set; }

    bool allowInput;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void OnEnable()
    {
        Actions.OnPlayerDied += OnPlayerDied;
        Actions.OnPlayerSpawned += OnPlayerSpawned;
    }

    private void OnDisable()
    {
        Actions.OnPlayerDied -= OnPlayerDied;
        Actions.OnPlayerSpawned -= OnPlayerSpawned;
    }

    void OnPlayerSpawned()
    {
        allowInput = true;
    }

    void OnPlayerDied()
    {
        allowInput = false;
    }

    public float GetMovementInput()
    {
        return allowInput ? Input.GetAxis("Horizontal") : 0f;
    }

    public bool GetJumpInput()
    {
        return allowInput ? Input.GetButtonDown("Jump") : false;
    }

    public bool GetSwitchInput()
    {
        return allowInput ? Input.GetKeyDown(KeyCode.S) : false;
    }
}
