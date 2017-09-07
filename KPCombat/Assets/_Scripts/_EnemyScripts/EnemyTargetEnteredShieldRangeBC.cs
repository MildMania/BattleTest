using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTargetEnteredShieldRangeBC : BehaviourController
{
    public FSMTransitionBehaviour FSMTransition;

    public override void Execute()
    {
        FSMTransition.DOFSMTransition(FSMStateID.SHIELD_UP);
    }
}
