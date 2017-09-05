using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharKnockBackStateBC : KnockBackStateBC
{
    public AnimationBehaviour AnimationBehaviour;

    public override void Execute()
    {
        AnimationBehaviour.PlayAnimation(Constants.CHAR_KNOCKBACK_ANIM_STATE);

        base.Execute();
    }

    protected override void OnKnockBackComplete()
    {
        FSMTransitionBehaviour.DOFSMTransition(FSMStateID.MOVE);

        FireOnExecutionCompleted();
    }
}
