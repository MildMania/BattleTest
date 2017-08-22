using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharKnockBackStateBC : KnockBackStateBC
{
    public AnimationBehaviour AnimationBehaviour;

    public override void Execute()
    {
        AnimationBehaviour.PlayAnimation((int)CharacterAnimEnum.Knockback);

        base.Execute();
    }

    protected override void OnKnockBackComplete()
    {
        FSMTransitionBehaviour.DOFSMTransition(FSMStateID.MOVE);

        FireOnExecutionCompleted();
    }
}
