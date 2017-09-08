using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharDashStateBC : FSMBehaviourController
{
    public CharDashExitStateBC DashExitStateBC;

    public CharAttackInfo DashAttackInfo;

    public AnimationBehaviour AnimationBehaviour;
    public AnimationBasedMovementBehaviour MovementBehaviour;
    public FSMTransitionBehaviour FSMTransitionBehaviour;

    public AttackerBase DashAttacker;

    public PusherBase Pusher;
    public PushableBase Pushable;

    protected override void InitFSMBC()
    {
        StateID = FSMStateID.DASH;
    }

    public override void Execute()
    {
        Debug.Break();

        Pusher.IsInteractionActive = false;
        Pushable.IsReactionActive = false;

        AnimationBehaviour.PlayAnimation(Constants.CHAR_DASH_ANIM_STATE);

        MovementBehaviour.MovementDuration = DashAttackInfo.MovementDuration;
        MovementBehaviour.AnimationCurve = DashAttackInfo.AnimationCurve;
        MovementBehaviour.MovementDistance = DashAttackInfo.MoveDistance;

        MovementBehaviour.Move().OnComplete(OnMovementCompleted);
    }

    void OnMovementCompleted()
    {
        DashExitStateBC.NextStateID = FSMStateID.MOVE;

        FSMTransitionBehaviour.DOFSMTransition(FSMStateID.DASH_EXIT);
    }

    public override void Exit()
    {
        MovementBehaviour.Stop();
        MovementBehaviour.UnRegisterOnComplete(OnMovementCompleted);

        base.Exit();
    }
}
