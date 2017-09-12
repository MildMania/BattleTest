using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum CharacterInputType
{
    ChargeStarted,
    Attack,
    ChargeReleased,
    Dash,
    ShieldUp,
    ShieldDown,
}

public class CharacterInputController : MMGameSceneBehaviour {

    public static CharacterInputController Instance { get; private set; }

    public FSMController FSMController;

    public bool IsChargePressed;

    public bool IsDebugEnabled;

    InputSchemeBase _curInputScheme;

    #region Events
    public static Action<CharacterInputType> OnInput;

    void FireOnCharacterInput(CharacterInputType inputType)
    {
        if (IsDebugEnabled)
            Debug.Log("<color=green>Input Event Fired: " + inputType + " on time:  "  + Time.realtimeSinceStartup + "</color>");

        if (OnInput != null)
            OnInput(inputType);
    }
    #endregion

    protected override void Awake()
    {
        Instance = this;

        InitInputScheme();

        base.Awake();
    }

    protected override void OnDestroy()
    {
        Instance = null;

        StopCheckInput();

        base.OnDestroy();
    }

    void InitInputScheme()
    {
        _curInputScheme = new InputScheme1();
    }

    protected override void OnGameStarted()
    {
        base.OnGameStarted();

        StartCheckInput();
    }

    protected override void OnGameEnded()
    {
        base.OnGameEnded();

        StopCheckInput();
    }

    void StartCheckInput()
    {
        if (_curInputScheme != null)
            _curInputScheme.StartListeningInputEvents();
    }

    void StopCheckInput()
    {
        if (_curInputScheme != null)
            _curInputScheme.FinishListeningInputEvents();
    }

    protected override void StartListeningGameEvents()
    {
        base.StartListeningGameEvents();

        KeyboardManager.AddListener(KeyCode.A, KeyState.Down, OnChargePressed);
        KeyboardManager.AddListener(KeyCode.A, KeyState.Up, OnAttackReleased);
    }

    protected override void StopListeningGameEvents()
    {
        base.StopListeningGameEvents();

        KeyboardManager.RemoveListener(KeyCode.A, KeyState.Down, OnChargePressed);
        KeyboardManager.RemoveListener(KeyCode.A, KeyState.Up, OnAttackReleased);

    }

    public void OnChargePressed()
    {
        IsChargePressed = true;

        FireOnCharacterInput(CharacterInputType.ChargeStarted);
    }

    public void OnAttackReleased()
    {
        IsChargePressed = false;

        FireOnCharacterInput(CharacterInputType.Attack);
    }

    public void OnChargeReleased()
    {
        IsChargePressed = false;

        FireOnCharacterInput(CharacterInputType.ChargeReleased);
    }

    public void OnDashPressed()
    {
        IsChargePressed = false;

        FireOnCharacterInput(CharacterInputType.Dash);
    }

    public void OnShieldUpPressed()
    {
        IsChargePressed = false;

        FireOnCharacterInput(CharacterInputType.ShieldUp);
    }

    public void OnShieldDownPressed()
    {
        IsChargePressed = false;

        FireOnCharacterInput(CharacterInputType.ShieldDown);
    }

    /*public void OnJumpLeftPressed()
    {
        FireOnCharacterInput(CharacterInputType.LeftJump);
    }

    public void OnRightJumpPressed()
    {
        FireOnCharacterInput(CharacterInputType.RightJump);
    }*/
}
