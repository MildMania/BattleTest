using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharSuperStrikeStateBC : FSMBehaviourController
{
    public CharAttackInfo AttackInfo;

    public CharAttackStateBC AttackBC;

    public FSMTransitionBehaviour FSMTransition;
    public AnimationBehaviour AnimationBehaviour;
    public AnimationBasedMovementBehaviour MovementBehaviour;

    public AttackerBase Attacker;

    public PusherBase Pusher;
    public PushableBase Pushable;

    protected override void InitFSMBC()
    {
        StateID = FSMStateID.SUPER_STRIKE;
    }

    public override void Execute()
    {
        Pusher.IsInteractionActive = false;
        Pushable.IsReactionActive = false;

        AttackBC.CurAttackInfo = AttackInfo;

        AnimationBehaviour.PlayAnimation(Constants.CHAR_SUPER_STRIKE_ANIM_STATE);

        MovementBehaviour.AnimationCurve = AttackInfo.AnimationCurve;
        MovementBehaviour.MovementDuration = AttackInfo.MovementDuration;
        MovementBehaviour.MovementDistance = AttackInfo.MoveDistance;

        MovementBehaviour.Move().OnComplete(OnMovementCompleted);
    }

    void OnMovementCompleted()
    {
        FireOnExecutionCompleted();

        FSMTransition.DOFSMTransition(FSMStateID.MOVE);

        AttackBC.ResetAttackInfo();
    }

    public override void Exit()
    {
        base.Exit();

        Attacker.IsInteractionActive = false;

        Pusher.IsInteractionActive = true;
        Pushable.IsReactionActive = true;
    }
}
