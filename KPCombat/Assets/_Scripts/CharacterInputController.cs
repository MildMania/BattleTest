using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum InputEnum
{
    Attack,

}

public class CharacterInputController : MonoBehaviour {

    public static Action<InputEnum> OnInput;

    void FireOnInput(InputEnum inputType)
    {
        if (OnInput != null)
            OnInput(inputType);
    }

    private void Awake()
    {
        StartListeningEvents();
    }

    private void OnDestroy()
    {
        StopListeningEvents();
    }

    void StartListeningEvents()
    {
        KeyboardManager.AddListener(KeyCode.A, KeyState.Down, OnAttackPressed);
    }

    void StopListeningEvents()
    {
        KeyboardManager.RemoveListener(KeyCode.A, KeyState.Down, OnAttackPressed);
    }

    void OnAttackPressed()
    {
        FireOnInput(InputEnum.Attack);
    }
}
