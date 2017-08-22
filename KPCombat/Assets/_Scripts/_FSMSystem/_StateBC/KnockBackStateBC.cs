using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBackStateBC : FSMBehaviourController
{
    public KnockBackBehaviour KnockBackBehaviour;
    public FSMTransitionBehaviour FSMTransitionBehaviour;

    protected override void InitFSMBC()
    {
        StateID = FSMStateID.KNOCK_BACK;
    }

    public override void Execute()
    {
        KnockBackBehaviour.KnockBack().OnComplete(OnKnockBackComplete);
    }

    protected virtual void OnKnockBackComplete()
    {

    }

    public override void Exit()
    {
        base.Exit();

        KnockBackBehaviour.UnRegisterOnComplete(OnKnockBackComplete);
    }
}
