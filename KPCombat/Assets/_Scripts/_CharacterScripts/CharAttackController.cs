using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharAttackController : MonoBehaviour
{
    public FSMController FSMController;


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
        CharacterInputController.OnInput += OnInput;
    }

    void StopListeningEvents()
    {
        CharacterInputController.OnInput -= OnInput;
    }

    void OnInput(InputEnum inputType)
    {
        if (inputType != InputEnum.Attack)
            return;

        CheckRecordInput();
    }

    void CheckRecordInput()
    {
        FSMController.SetTransition(FSMStateID.MELEE_ATTACK);
    }
}
