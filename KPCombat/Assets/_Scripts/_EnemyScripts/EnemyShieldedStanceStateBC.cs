using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShieldedStanceStateBC : FSMBehaviourController
{
    public AnimationBehaviour AnimBehaviour;

    protected override void InitFSMBC()
    {
        StateID = FSMStateID.SHIELDED_STANCE;
    }

    public override void Execute()
    {
        base.Execute();

        AnimBehaviour.PlayAnimation(Constants.ENEMY_SHIELDED_STANCE_ANIM_STATE);
    }
}
