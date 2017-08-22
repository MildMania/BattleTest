using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class CharAttackInfo
{
    public float BaseDamage;
    public CharacterAnimEnum AnimEnum;
    public DirectionEnum AttackDirection;
    public AttackTypeEnum AttackType;
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

        //Debug.Log("<color=green>Attack Completed</color>");
    }

    public Action OnAttackInterrupted;

    void FireOnAttackInterrupted()
    {
        if (OnAttackInterrupted != null)
            OnAttackInterrupted();

        //Debug.Log("<color=red>Attack Interrupted</color>");
    }
    #endregion

    public FSMTransitionBehaviour FSMTransition;
    public AnimationBehaviour Animationbehaviour;
    public AnimationBasedMovementBehaviour MovementBehaviour;

    public MMBasicAnimController AttackCollderAnimController;

    public float AttackResetDuration;

    public PusherBase Pusher;
    public PushableBase Pushable;

    public List<CharAttackInfo> AttackInfoList;
    public CharAttackInfo CurAttackInfo { get; private set; }
    public int CurAttackInfoIndex { get; private set; }

    IEnumerator _resetAttackRoutine;

    bool _completedSuccessfully;

    protected override void InitFSMBC()
    {
        StateID = FSMStateID.MELEE_ATTACK;

        ResetAttackInfo();
    }

    public override void Execute()
    {
        base.Execute();

        Pusher.IsInteractionActive = false;
        Pushable.IsReactionActive = false;

        _completedSuccessfully = false;

        Animationbehaviour.PlayAnimation((int)CurAttackInfo.AnimEnum);

        MovementBehaviour.AnimationCurve = CurAttackInfo.AnimationCurve;
        MovementBehaviour.MovementDuration = CurAttackInfo.MovementDuration;
        MovementBehaviour.MovementDistance = CurAttackInfo.MoveDistance;

        MovementBehaviour.Move().OnComplete(OnMovementCompleted);

        AttackCollderAnimController.PlayAnimation(CurAttackInfo.AttackColliderStateName);

        if (_resetAttackRoutine != null)
            StopCoroutine(_resetAttackRoutine);
    }

    void OnMovementCompleted()
    {
        _completedSuccessfully = true;

        _resetAttackRoutine = WaitForAttackReset();
        StartCoroutine(_resetAttackRoutine);

        FireOnExecutionCompleted();

        FireOnAttackFinished();

        UpdateAttackInfo();
    }

    public override void Exit()
    {
        Pusher.IsInteractionActive = true;
        Pushable.IsReactionActive = true;

        if (!_completedSuccessfully)
            FireOnAttackInterrupted();

        base.Exit();
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
