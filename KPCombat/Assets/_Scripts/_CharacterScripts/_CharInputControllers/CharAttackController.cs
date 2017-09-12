using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharAttackController : MMGameSceneBehaviour
{
    public FSMController FSMController;

    public CharAttackStateBC AttackBC;
    public CharChargeStateBC ChargeBC;

    public float AttackInputQueueDuration;

    float _lastAttackInputTime;

    const int MAX_INPUE_QUEUE_LENGTH = 3;
    int _queueInputLength;

    IEnumerator _processChargeInputRoutine;
    IEnumerator _processAttackInputRoutine;

    bool _isChargeCompleted;


    protected override void StartListeningGameEvents()
    {
        AttackBC.OnAttackFinished += OnAttackCompleted;
        ChargeBC.OnChargeCompleted += OnChargeCompleted;
        ChargeBC.OnChargeInterrupted += OnChargeInterrupted;
        CharacterInputController.OnInput += OnInput;

        base.StartListeningGameEvents();
    }

    protected override void StopListeningGameEvents()
    {
        AttackBC.OnAttackFinished -= OnAttackCompleted;
        ChargeBC.OnChargeCompleted -= OnChargeCompleted;
        ChargeBC.OnChargeInterrupted -= OnChargeInterrupted;
        CharacterInputController.OnInput -= OnInput;

        base.StopListeningGameEvents();
    }

    void OnInput(CharacterInputType inputType)
    {
        if (inputType == CharacterInputType.ChargeStarted)
            CheckChargeInput();
        else if(inputType == CharacterInputType.ChargeReleased)
            CheckChargeReleasedInput();
        else if (inputType == CharacterInputType.Attack)
        {
            if (!_isChargeCompleted)
                CheckRecordAttackInput();
            else
                PerformSuperStrike();
        }
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

        if(CharacterInputController.Instance.IsChargePressed)
            Charge();
    }

    void Charge()
    {
        ResetAttackQueue();

        if (_processAttackInputRoutine != null)
        {
            StopCoroutine(_processAttackInputRoutine);
            _processAttackInputRoutine = null;
        }

        _isChargeCompleted = false;

        FSMController.SetTransition(FSMStateID.MELEE_CHARGE);
    }

    void OnChargeCompleted()
    {
        _isChargeCompleted = true;
    } 

    void OnChargeInterrupted()
    {
        _isChargeCompleted = false;
    }

    void CheckChargeReleasedInput()
    {
        FSMStateID curBattleState = FSMController.GetCurStateIDOfFSM(FSMType.Battle);

        if(curBattleState == FSMStateID.MELEE_CHARGE)
            ReleaseCharge();
    }

    void ReleaseCharge()
    {
        FSMController.SetTransition(FSMStateID.MELEE_CHARGE_EXIT);
    }

    void CheckRecordAttackInput()
    {
        _lastAttackInputTime = Time.realtimeSinceStartup;

        FSMStateID curBattleState = FSMController.GetCurStateIDOfFSM(FSMType.Battle);

        if (curBattleState == FSMStateID.MELEE_ATTACK)
            _queueInputLength++;
        else
            _queueInputLength = 1;

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

        } while (curBattleState != FSMStateID.MELEE_CHARGE
        && curBattleState != FSMStateID.IDLE);

        if(Time.realtimeSinceStartup - _lastAttackInputTime <= AttackInputQueueDuration)
            SwingSword();

        _processAttackInputRoutine = null;
    }

    void OnAttackCompleted()
    {
        _queueInputLength--;

        if (_queueInputLength < 0 
            || AttackBC.CurAttackInfoIndex == AttackBC.AttackInfoList.Count - 1)
            _queueInputLength = 0;

        if (_queueInputLength > 0
            && Time.realtimeSinceStartup - _lastAttackInputTime <= AttackInputQueueDuration)
            SwingSword();
        else
        {
            FinishAttack();

            if (CharacterInputController.Instance.IsChargePressed)
                CheckChargeInput();
        }
    }

    void SwingSword()
    {
        //Debug.Log("swing sword: " + Time.renderedFrameCount);

        FSMController.SetTransition(FSMStateID.MELEE_ATTACK);
    }

    void FinishAttack()
    {
        FSMController.SetTransition(FSMStateID.MOVE);
    }

    void PerformSuperStrike()
    {
        FSMController.SetTransition(FSMStateID.SUPER_STRIKE);
    }

    void ResetAttackQueue()
    {
        AttackBC.ResetAttackInfo();
        _queueInputLength = 0;
    }
}
