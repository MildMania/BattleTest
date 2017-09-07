using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShieldedPushedBackStateBC : PushedBackStateBC
{
    public FSMTransitionBehaviour FSMBehaviour;

    protected override void InitFSMBC()
    {
        StateID = FSMStateID.SHIELDED_PUSHED_BACK;
    }

    protected override void OnKnockBackCompleted()
    {
        FSMBehaviour.DOFSMTransition(FSMStateID.SHIELDED_STANCE);

        base.OnKnockBackCompleted();
    }
}
