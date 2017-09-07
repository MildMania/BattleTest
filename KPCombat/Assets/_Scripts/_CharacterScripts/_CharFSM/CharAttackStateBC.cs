using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class CharAttackInfo
{
    public float BaseDamage;
    public string AnimName;
    public DirectionEnum AttackDirection;
    public AttackTypeEnum AttackType;
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

    #endregion

    public FSMTransitionBehaviour FSMTransition;
    public AnimationBehaviour Animationbehaviour;
    public AnimationBasedMovementBehaviour MovementBehaviour;

    public CharAttackController AttackController;

    public float AttackResetDuration;

    public AttackerBase Attacker;

    public PusherBase Pusher;
    public PushableBase Pushable;

    public List<CharAttackInfo> AttackInfoList;
    public CharAttackInfo CurAttackInfo { get; set; }
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

        Pusher.IsInteractionActive = false;
        Pushable.IsReactionActive = false;

        Animationbehaviour.PlayAnimation(CurAttackInfo.AnimName);

        MovementBehaviour.AnimationCurve = CurAttackInfo.AnimationCurve;
        MovementBehaviour.MovementDuration = CurAttackInfo.MovementDuration;
        MovementBehaviour.MovementDistance = CurAttackInfo.MoveDistance;

        MovementBehaviour.Move().OnComplete(OnMovementCompleted);

        if (_resetAttackRoutine != null)
            StopCoroutine(_resetAttackRoutine);
    }

    void OnMovementCompleted()
    {
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

        Attacker.IsInteractionActive = false;

        MovementBehaviour.Stop();
        MovementBehaviour.UnRegisterOnComplete(OnMovementCompleted);

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

    public void ResetAttackInfo()
    {
        CurAttackInfo = AttackInfoList[0];
        CurAttackInfoIndex = 0;
    }
}
