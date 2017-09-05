using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKnockBackBC : KnockBackStateBC
{
    public AnimationBehaviour AnimBehaviour;

    public PusherBase Pusher;
    public PushableBase Pushable;

    public string KnockBackAnimation { get; set; }

    public override void Execute()
    {
        base.Execute();

        AnimBehaviour.PlayAnimation(KnockBackAnimation).OnComplete(OnKnockBackAnimationCompleted);
    }

    protected override void OnKnockBackComplete()
    {
        base.OnKnockBackComplete();

        Pusher.IsInteractionActive = true;
        Pushable.IsReactionActive = true;
    }

    void OnKnockBackAnimationCompleted()
    {
        Pusher.IsInteractionActive = false;
        Pushable.IsReactionActive = false;

        FSMTransitionBehaviour.DOFSMTransition(FSMStateID.MOVE);

        FireOnExecutionCompleted();
    }

    public override void Exit()
    {
        Pusher.IsInteractionActive = false;
        Pushable.IsReactionActive = false;

        base.Exit();

        AnimBehaviour.UnRegisterOnComplete(OnKnockBackAnimationCompleted);
    }
}
