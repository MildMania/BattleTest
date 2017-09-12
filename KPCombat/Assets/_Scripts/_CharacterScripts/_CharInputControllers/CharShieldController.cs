using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharShieldController : MMGameSceneBehaviour
{
    public CharShieldUpStateBC ShieldUpStateBC;
    public CharShieldDownStateBC ShieldDownStateBC;


    public FSMController FSMController;

    public float ShieldQueueDuration;

    IEnumerator _processShieldUpInputRoutine;
    IEnumerator _processShieldDownInputRoutine;

    bool _hasShieldUpInputQueued;

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
        else if (inputType == CharacterInputType.ShieldDown)
            CheckShieldDownInput();
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

        _hasShieldUpInputQueued = false;
    }

    void StartProcessShieldUpInputProgress()
    {
        StopProcessShieldUpInputProgress();

        _processShieldUpInputRoutine = ProcessShieldUpInputProgress();
        StartCoroutine(_processShieldUpInputRoutine);
    }

    IEnumerator ProcessShieldUpInputProgress()
    {
        _hasShieldUpInputQueued = true;

        float lastInputTime = Time.realtimeSinceStartup;

        FSMStateID curMovementState = FSMController.GetCurStateIDOfFSM(FSMType.Movement);

        do
        {
            curMovementState = FSMController.GetCurStateIDOfFSM(FSMType.Movement);

            yield return null;

        } while (curMovementState != FSMStateID.MOVE);

        if (Time.realtimeSinceStartup - lastInputTime <= ShieldQueueDuration)
            ShieldUp();

        _hasShieldUpInputQueued = false;

        _processShieldUpInputRoutine = null;
    }


    void CheckShieldDownInput()
    {
        FSMStateID curMovementState = FSMController.GetCurStateIDOfFSM(FSMType.Movement);

        if (curMovementState != FSMStateID.SHIELD_UP 
            && !_hasShieldUpInputQueued)
            return;

        StartProcessShieldDownInputProgress();
    }

    void StartProcessShieldDownInputProgress()
    {
        StopProcessShieldDownInputProgress();

        _processShieldDownInputRoutine = ProcessShieldDownInputProgress();
        StartCoroutine(_processShieldDownInputRoutine);
    }

    void StopProcessShieldDownInputProgress()
    {
        if (_processShieldDownInputRoutine != null)
            StopCoroutine(_processShieldDownInputRoutine);
    }

    IEnumerator ProcessShieldDownInputProgress()
    {
        FSMStateID curMovementState = FSMController.GetCurStateIDOfFSM(FSMType.Movement);

        if (_hasShieldUpInputQueued)
        {
            do
            {
                curMovementState = FSMController.GetCurStateIDOfFSM(FSMType.Movement);

                yield return null;
            } while (curMovementState != FSMStateID.SHIELD_UP);
        }


        do
        {
            yield return null;
        } while (ShieldUpStateBC.CurShieldedModeDuration < ShieldUpStateBC.MinShieldedModeDuration);

        curMovementState = FSMController.GetCurStateIDOfFSM(FSMType.Movement);

        if (curMovementState == FSMStateID.SHIELD_UP)
            ShieldDown();
    }

    void ShieldUp()
    {
        FSMController.SetTransition(FSMStateID.SHIELD_UP);
    }

    void ShieldDown()
    {
        ShieldDownStateBC.NextStateID = FSMStateID.MOVE;

        FSMController.SetTransition(FSMStateID.SHIELD_DOWN);
    }
}
