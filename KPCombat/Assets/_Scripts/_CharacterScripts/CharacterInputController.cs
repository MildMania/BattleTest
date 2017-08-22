using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum InputEnum
{
    Charge,
    Attack,

}

public class CharacterInputController : MMGameSceneBehaviour {

    public static CharacterInputController Instance { get; private set; } 

    public bool IsDebugEnabled;

    #region Events
    public static Action<InputEnum> OnInput;

    void FireOnInput(InputEnum inputType)
    {
        if (OnInput != null)
            OnInput(inputType);

        if(IsDebugEnabled)
            Debug.Log("<color=green>Input Event Fired: " + inputType + "</color>");
    }
    #endregion

    InputSchemeBase _curInputScheme;

    public FSMController FSMController;

    protected override void Awake()
    {
        Instance = this;

        InitInputScheme();

        base.Awake();
    }

    protected override void OnDestroy()
    {
        Instance = null;

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

        KeyboardManager.AddListener(KeyCode.A, KeyState.Down, OnAttackPressed);
        KeyboardManager.AddListener(KeyCode.A, KeyState.Up, OnAttackReleased);
    }

    protected override void StopListeningGameEvents()
    {
        base.StopListeningGameEvents();

        KeyboardManager.RemoveListener(KeyCode.A, KeyState.Down, OnAttackPressed);
        KeyboardManager.RemoveListener(KeyCode.A, KeyState.Up, OnAttackReleased);

    }

    public void OnAttackPressed()
    {
        FSMStateID battleStateID = FSMController.GetCurStateIDOfFSM(FSMType.Battle);

        if (battleStateID == FSMStateID.MELEE_ATTACK)
            FireOnInput(InputEnum.Attack);
        else
            FireOnInput(InputEnum.Charge);
    }

    public void OnAttackReleased()
    {
        FSMStateID battleStateID = FSMController.GetCurStateIDOfFSM(FSMType.Battle);

        if (battleStateID == FSMStateID.MELEE_CHARGE)
            FireOnInput(InputEnum.Attack);
    }
}
