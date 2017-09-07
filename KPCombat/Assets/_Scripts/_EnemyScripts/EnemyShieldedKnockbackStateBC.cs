using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShieldedKnockbackStateBC : KnockBackStateBC
{
    public AnimationBehaviour AnimBehaviour;

    public string KnockBackAnimation { get; set; }

    protected override void InitFSMBC()
    {
        StateID = FSMStateID.SHIELDED_KNOCK_BACK;
    }

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
