using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharAttackController : MMGameSceneBehaviour
{
    public FSMController FSMController;

    public CharAttackStateBC AttackBC;

    const int MAX_INPUE_QUEUE_LENGTH = 3;
    int _queueInputLength;

    IEnumerator _swingSwordRoutine;

    IEnumerator _processSwordSwingQueueRoutine;


    protected override void StartListeningGameEvents()
    {
        AttackBC.OnAttackFinished += OnAttackCompleted;
        CharacterInputController.OnInput += OnInput;

        base.StartListeningGameEvents();
    }

    protected override void StopListeningGameEvents()
    {
        AttackBC.OnAttackFinished -= OnAttackCompleted;
        CharacterInputController.OnInput -= OnInput;

        base.StopListeningGameEvents();
    }

    void OnInput(InputEnum inputType)
    {
        if (inputType != InputEnum.Attack)
            return;

        CheckRecordInput();
    }

    void CheckRecordInput()
    {
        _queueInputLength++;

        if (_queueInputLength > MAX_INPUE_QUEUE_LENGTH)
            _queueInputLength = MAX_INPUE_QUEUE_LENGTH;

        if (_queueInputLength == 1 &&_processSwordSwingQueueRoutine == null)
        {
            _processSwordSwingQueueRoutine = ProcessSwingSwordQueue();
            StartCoroutine(_processSwordSwingQueueRoutine);
        }
    }

    IEnumerator ProcessSwingSwordQueue()
    {
        FSMStateID curMovementState = FSMController.GetCurStateIDOfFSM(FSMType.Movement);

        do
        {
            curMovementState = FSMController.GetCurStateIDOfFSM(FSMType.Movement);

            yield return null;

        } while (curMovementState == FSMStateID.MELEE_ATTACK);

        SwingSword();

        _processSwordSwingQueueRoutine = null;
    }

    void OnAttackCompleted()
    {
        _queueInputLength--;

        if (_queueInputLength < 0 || AttackBC.CurAttackInfoIndex == AttackBC.AttackInfoList.Count - 1)
            _queueInputLength = 0;

        if (_queueInputLength > 0)
            SwingSword();
        else
            FinishAttack();
    }

    void SwingSword()
    {
        FSMController.SetTransition(FSMStateID.MELEE_ATTACK);
    }

    void FinishAttack()
    {
        FSMController.SetTransition(FSMStateID.MOVE);
    }
}
