using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharShieldController : MMGameSceneBehaviour
{
    public FSMController FSMController;

    public float ShieldQueueDuration;

    IEnumerator _processShieldUpInputRoutine;

    protected override void StartListeningGameEvents()
    {
        CharacterInputController.OnInput += OnInput;

        base.StartListeningGameEvents();
    }

    protected override void StopListeningGameEvents()
    {
        CharacterInputController.OnInput -= OnInput;

        base.StopListeningGameEvents();
    }

    void OnInput(CharacterInputType inputType)
    {
        if (inputType == CharacterInputType.ShieldUp)
            CheckShieldUpInput();
    }

    void CheckShieldUpInput()
    {
        if (_processShieldUpInputRoutine == null)
            StartProcessShieldUpInputProgress();
    }

    void StopProcessShieldUpInputProgress()
    {
        if (_processShieldUpInputRoutine != null)
            StopCoroutine(_processShieldUpInputRoutine);
    }

    void StartProcessShieldUpInputProgress()
    {
        StopProcessShieldUpInputProgress();

        _processShieldUpInputRoutine = ProcessShieldUpInputProgress();
        StartCoroutine(_processShieldUpInputRoutine);
    }

    IEnumerator ProcessShieldUpInputProgress()
    {
        float lastInputTime = Time.realtimeSinceStartup;

        FSMStateID curMovementState = FSMController.GetCurStateIDOfFSM(FSMType.Movement);

        do
        {
            curMovementState = FSMController.GetCurStateIDOfFSM(FSMType.Movement);

            yield return null;

        } while (curMovementState != FSMStateID.MOVE);

        if (Time.realtimeSinceStartup - lastInputTime <= ShieldQueueDuration)
            ShieldUp();

        _processShieldUpInputRoutine = null;
    }

    void ShieldUp()
    {
        FSMController.SetTransition(FSMStateID.SHIELD_UP);
    }
}
