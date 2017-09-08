using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharDashExitStateBC : FSMBehaviourController
{
    public FSMStateID NextStateID { get; set; }

    public AnimationBehaviour AnimationBehaviour;
    public AnimationBasedMovementBehaviour MovementBehaviour;
    public FSMTransitionBehaviour FSMTransitionBehaviour;

    public AttackerBase DashAttacker;

    public PusherBase Pusher;
    public PushableBase Pushable;

    protected override void InitFSMBC()
    {
        StateID = FSMStateID.DASH_EXIT;
    }

    public override void Execute()
    {
        base.Execute();

        AnimationBehaviour.PlayAnimation(Constants.CHAR_DASH_EXIT_ANIM_STATE);

        FSMTransitionBehaviour.DOFSMTransition(NextStateID);
    }

    public override void Exit()
    {
        base.Exit();

        DashAttacker.IsInteractionActive = false;

        Pusher.IsInteractionActive = true;
        Pushable.IsReactionActive = true;
    }
}
