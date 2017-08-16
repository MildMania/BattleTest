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

    private void Update()
    {
        CheckInput();
    }

    void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.A))
            FireOnInput(InputEnum.Attack);

    }
}
