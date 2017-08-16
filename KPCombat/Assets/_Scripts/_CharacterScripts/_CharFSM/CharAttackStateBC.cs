using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class CharAttackInfo
{
    public float AttackDuration;
    public float MoveSpeed;
    public string StateName;
    public float KnockBackAmount;
}


public class CharAttackStateBC : FSMBehaviourController
{
    public FSMController FSMController;

    public float AttackResetDuration;

    public RunBehaviour RunBehaviour;
    public Vector2 MoveDirection;

    public List<CharAttackInfo> AttackInfoList;
    CharAttackInfo _curAttackInfo;
    int _curAttackInfoIndex;

    IEnumerator _attackRoutine;
    IEnumerator _resetAttackRoutine;

    protected override void InitFSMBC()
    {
        StateID = FSMStateID.MELEE_ATTACK;

        ResetAttackInfo();
    }

    public override void Execute()
    {
        base.Execute();

        RunBehaviour.HeadDirection = MoveDirection;
        RunBehaviour.RunSpeed = _curAttackInfo.MoveSpeed;

        RunBehaviour.StartMovement();

        if (_resetAttackRoutine != null)
            StopCoroutine(_resetAttackRoutine);

        _attackRoutine = AttackProgress();
        StartCoroutine(_attackRoutine);
    }

    IEnumerator AttackProgress()
    {
        yield return new WaitForSeconds(_curAttackInfo.AttackDuration);

        _resetAttackRoutine = WaitForAttackReset();
        StartCoroutine(_resetAttackRoutine);

        RunBehaviour.Stop();

        FSMController.SetTransition(FSMStateID.MOVE);

        FireOnExecutionCompleted();
    }

    public override void Stop()
    {
        base.Stop();

        RunBehaviour.Stop();

        if (_attackRoutine != null)
            StopCoroutine(_attackRoutine);
    }

    IEnumerator WaitForAttackReset()
    {
        yield return new WaitForSeconds(AttackResetDuration);

        ResetAttackInfo();
    }

    void UpdateAttackInfo()
    {
        _curAttackInfoIndex++;
        _curAttackInfo = AttackInfoList[_curAttackInfoIndex];
    }

    void ResetAttackInfo()
    {
        _curAttackInfo = AttackInfoList[0];
        _curAttackInfoIndex = 0;
    }
}
