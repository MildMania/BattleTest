using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharDashController : MMGameSceneBehaviour
{
    public FSMController FSMController;

    public float DashQueueDuration;

    IEnumerator _processDashInputRoutine;

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
        if (inputType == CharacterInputType.Dash)
            CheckDashInput();
    }

    void CheckDashInput()
    {
        if(_processDashInputRoutine == null)
        StartProcessDashInputProgress();
    }

    void StopProcessDashInputProgress()
    {
        if (_processDashInputRoutine != null)
            StopCoroutine(_processDashInputRoutine);
    }

    void StartProcessDashInputProgress()
    {
        StopProcessDashInputProgress();

        _processDashInputRoutine = ProcessDashInputProgress();
        StartCoroutine(_processDashInputRoutine);
    }

    IEnumerator ProcessDashInputProgress()
    {
        float lastInputTime = Time.realtimeSinceStartup;

        FSMStateID curMovementState = FSMController.GetCurStateIDOfFSM(FSMType.Movement);

        do
        {
            curMovementState = FSMController.GetCurStateIDOfFSM(FSMType.Movement);

            yield return null;

        } while (curMovementState != FSMStateID.MOVE);

        if (Time.realtimeSinceStartup - lastInputTime <= DashQueueDuration)
            Dash();

        _processDashInputRoutine = null;
    }

    void Dash()
    {
        FSMController.SetTransition(FSMStateID.DASH);
    }
}
