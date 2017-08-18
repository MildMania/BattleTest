using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class CharAttackInfo
{
    public float BaseDamage;
    public CharacterAnimEnum AnimEnum;
    public string AttackColliderStateName;
    public float KnockBackAmount;
    public AnimationCurve AnimationCurve;
    public Vector2 MoveDistance;
    public float MovementDuration;
}


public class CharAttackStateBC : FSMBehaviourController
{
    #region Events

    public Action OnAttackFinished;

    void FireOnAttackFinished()
    {
        if (OnAttackFinished != null)
            OnAttackFinished();
    }

    #endregion

    public FSMTransitionBehaviour FSMTransition;
    public AnimationBehaviour Animationbehaviour;
    public AnimationBasedMovementBehaviour MovementBehaviour;

    public MMBasicAnimController AttackCollderAnimController;

    public float AttackResetDuration;

    public List<CharAttackInfo> AttackInfoList;
    public CharAttackInfo CurAttackInfo { get; private set; }
    public int CurAttackInfoIndex { get; private set; }

    IEnumerator _resetAttackRoutine;

    protected override void InitFSMBC()
    {
        StateID = FSMStateID.MELEE_ATTACK;

        ResetAttackInfo();
    }

    public override void Execute()
    {
        base.Execute();

        Animationbehaviour.PlayAnimation((int)CurAttackInfo.AnimEnum).OnComplete(OnAnimComplete);

        MovementBehaviour.AnimationCurve = CurAttackInfo.AnimationCurve;
        MovementBehaviour.MovementDuration = CurAttackInfo.MovementDuration;
        MovementBehaviour.MovementDistance = CurAttackInfo.MoveDistance;

        MovementBehaviour.Move();

        AttackCollderAnimController.PlayAnimation(CurAttackInfo.AttackColliderStateName);

        if (_resetAttackRoutine != null)
            StopCoroutine(_resetAttackRoutine);
    }

    void OnAnimComplete()
    {
        _resetAttackRoutine = WaitForAttackReset();
        StartCoroutine(_resetAttackRoutine);

        FireOnExecutionCompleted();

        FireOnAttackFinished();

        UpdateAttackInfo();
    }

    IEnumerator WaitForAttackReset()
    {
        yield return new WaitForSeconds(AttackResetDuration);

        ResetAttackInfo();
    }

    void UpdateAttackInfo()
    {
        CurAttackInfoIndex++;

        if (CurAttackInfoIndex == AttackInfoList.Count)
        {
            ResetAttackInfo();

            return;
        }

        CurAttackInfo = AttackInfoList[CurAttackInfoIndex];
    }

    void ResetAttackInfo()
    {
        CurAttackInfo = AttackInfoList[0];
        CurAttackInfoIndex = 0;
    }
}
