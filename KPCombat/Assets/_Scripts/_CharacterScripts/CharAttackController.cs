using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharAttackController : MMGameSceneBehaviour
{
    public FSMController FSMController;

    public CharAttackStateBC AttackBC;
    public CharChargeStateBC ChargeBC;

    const int MAX_INPUE_QUEUE_LENGTH = 3;
    int _queueInputLength;

    IEnumerator _processChargeInputRoutine;
    IEnumerator _processAttackInputRoutine;


    protected override void StartListeningGameEvents()
    {
        AttackBC.OnAttackFinished += OnAttackCompleted;
        AttackBC.OnAttackInterrupted += OnAttackInterrupted;
        ChargeBC.OnChargeCompleted += OnChargeCompleted;
        CharacterInputController.OnInput += OnInput;

        base.StartListeningGameEvents();
    }

    protected override void StopListeningGameEvents()
    {
        AttackBC.OnAttackFinished -= OnAttackCompleted;
        AttackBC.OnAttackInterrupted -= OnAttackInterrupted;
        ChargeBC.OnChargeCompleted -= OnChargeCompleted;
        CharacterInputController.OnInput -= OnInput;

        base.StopListeningGameEvents();
    }

    void OnInput(InputEnum inputType)
    {
        if (inputType == InputEnum.Charge)
            CheckChargeInput();
        else if (inputType == InputEnum.Attack)
            CheckRecordAttackInput();
    }

    void CheckChargeInput()
    {
        if (_processChargeInputRoutine != null)
            StopCoroutine(_processChargeInputRoutine);

        _processChargeInputRoutine = ProcessChargeInputRoutine();
        StartCoroutine(_processChargeInputRoutine);
    }

    IEnumerator ProcessChargeInputRoutine()
    {
        FSMStateID curBattleState = FSMController.GetCurStateIDOfFSM(FSMType.Battle);

        do
        {
            curBattleState = FSMController.GetCurStateIDOfFSM(FSMType.Battle);

            yield return null;

        } while (curBattleState != FSMStateID.IDLE);

        Charge();
    }

    void Charge()
    {
        FSMController.SetTransition(FSMStateID.MELEE_CHARGE);
    }

    void OnChargeCompleted()
    {

    } 

    void CheckRecordAttackInput()
    {
        FSMStateID curBattleState = FSMController.GetCurStateIDOfFSM(FSMType.Battle);

        if(curBattleState == FSMStateID.MELEE_CHARGE)
            _queueInputLength = 0;

        _queueInputLength++;

        if (_queueInputLength > MAX_INPUE_QUEUE_LENGTH)
            _queueInputLength = MAX_INPUE_QUEUE_LENGTH;

        if (_queueInputLength == 1 &&_processAttackInputRoutine == null)
        {
            _processAttackInputRoutine = ProcessSwingSwordQueue();
            StartCoroutine(_processAttackInputRoutine);
        }
    }

    IEnumerator ProcessSwingSwordQueue()
    {
        FSMStateID curBattleState = FSMController.GetCurStateIDOfFSM(FSMType.Battle);

        do
        {
            curBattleState = FSMController.GetCurStateIDOfFSM(FSMType.Battle);

            yield return null;

        } while (curBattleState != FSMStateID.MELEE_CHARGE);

        SwingSword();

        _processAttackInputRoutine = null;
    }

    void OnAttackCompleted()
    {
        _queueInputLength--;

        if (_queueInputLength < 0 
            || AttackBC.CurAttackInfoIndex == AttackBC.AttackInfoList.Count - 1)
            _queueInputLength = 0;

        if (_queueInputLength > 0)
            SwingSword();
        else
            FinishAttack();
    }

    void OnAttackInterrupted()
    {
        _queueInputLength = 0; 

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
