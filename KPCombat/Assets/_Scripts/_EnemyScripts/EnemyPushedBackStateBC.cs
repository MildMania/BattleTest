using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPushedBackStateBC : PushedBackStateBC
{
    public FSMTransitionBehaviour FSMBehaviour;

    protected override void OnKnockBackCompleted()
    {
        FSMBehaviour.DOFSMTransition(FSMStateID.MOVE);

        base.OnKnockBackCompleted();
    }
}
