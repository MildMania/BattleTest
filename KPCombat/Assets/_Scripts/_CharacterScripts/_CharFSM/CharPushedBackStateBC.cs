using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharPushedBackStateBC : PushedBackStateBC
{
    public FSMTransitionBehaviour FSMBehaviour;
    public AnimationBehaviour AnimationBehaviour;

    public string PushBackAnimState { get; set; }

    public override void Execute()
    {
        base.Execute();

        AnimationBehaviour.PlayAnimation(PushBackAnimState);
    }

    protected override void OnKnockBackCompleted()
    {
        FSMBehaviour.DOFSMTransition(FSMStateID.MOVE);

        base.OnKnockBackCompleted();
    }
}
