using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PushedBackStateBC : FSMBehaviourController
{
    public KnockBackBehaviour KnockbackBehaviour;

    protected override void InitFSMBC()
    {
        StateID = FSMStateID.PUSHED_BACK;
    }

    public override void Execute()
    {
        base.Execute();

        KnockbackBehaviour.KnockBack().OnComplete(OnKnockBackCompleted);
    }

    protected virtual void OnKnockBackCompleted()
    {
        FireOnExecutionCompleted();
    }

    public override void Exit()
    {
        base.Exit();

        KnockbackBehaviour.UnRegisterOnComplete(OnKnockBackCompleted);
    }
}
