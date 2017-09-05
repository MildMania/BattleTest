using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharPushedBackStateBC : PushedBackStateBC
{
    public FSMTransitionBehaviour FSMBehaviour;
    public AnimationBehaviour AnimationBehaviour;

    public override void Execute()
    {
        base.Execute();

        AnimationBehaviour.PlayAnimation(Constants.CHAR_KNOCKBACK_ANIM_STATE);
    }

    protected override void OnKnockBackCompleted()
    {
        FSMBehaviour.DOFSMTransition(FSMStateID.MOVE);

        base.OnKnockBackCompleted();
    }
}
