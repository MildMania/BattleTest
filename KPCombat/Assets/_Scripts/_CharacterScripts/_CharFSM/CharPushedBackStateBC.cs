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

        AnimationBehaviour.PlayAnimation((int)CharacterAnimEnum.Knockback);
    }

    protected override void OnKnockBackCompleted()
    {
        FSMBehaviour.DOFSMTransition(FSMStateID.MOVE);

        base.OnKnockBackCompleted();
    }
}
