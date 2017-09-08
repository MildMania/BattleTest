using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKnockBackBC : KnockBackStateBC
{
    public AnimationBehaviour AnimBehaviour;

    public string KnockBackAnimation { get; set; }

    public override void Execute()
    {
        base.Execute();

        AnimBehaviour.PlayAnimation(KnockBackAnimation).OnComplete(OnKnockBackAnimationCompleted);
    }

    void OnKnockBackAnimationCompleted()
    {
        FSMTransitionBehaviour.DOFSMTransition(FSMStateID.MOVE);

        FireOnExecutionCompleted();
    }

    public override void Exit()
    {
        base.Exit();

        AnimBehaviour.UnRegisterOnComplete(OnKnockBackAnimationCompleted);
    }
}
