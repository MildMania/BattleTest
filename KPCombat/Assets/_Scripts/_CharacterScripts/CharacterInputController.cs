using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum InputEnum
{
    Attack,

}

public class CharacterInputController : MMGameSceneBehaviour {

    #region Events
    public static Action<InputEnum> OnInput;

    void FireOnInput(InputEnum inputType)
    {
        if (OnInput != null)
            OnInput(inputType);
    }
    #endregion

    protected override void StartListeningGameEvents()
    {
        base.StartListeningGameEvents();

        KeyboardManager.AddListener(KeyCode.A, KeyState.Down, OnAttackPressed);
    }

    protected override void StopListeningGameEvents()
    {
        base.StopListeningGameEvents();

        KeyboardManager.RemoveListener(KeyCode.A, KeyState.Down, OnAttackPressed);
    }

    void OnAttackPressed()
    {
        FireOnInput(InputEnum.Attack);
    }

    private void Update()
    {
        //FireOnInput(InputEnum.Attack);
    }
}
