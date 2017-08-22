using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTargetEnteredAttackRangeBC : BehaviourController
{
    public FSMTransitionBehaviour FSMTransition;

    public override void Execute()
    {
        FSMTransition.DOFSMTransition(FSMStateID.MELEE_ATTACK);
    }

}
