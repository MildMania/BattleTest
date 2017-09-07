using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShieldedStanceStateBC : FSMBehaviourController
{
    public AnimationBehaviour AnimBehaviour;

    public DamagableBase ShieldDamagable;
    public DamagableBase BaseDamagable;

    protected override void InitFSMBC()
    {
        StateID = FSMStateID.SHIELDED_STANCE;
    }

    public override void Execute()
    {
        base.Execute();

        ShieldDamagable.IsReactionActive = true;
        BaseDamagable.IsReactionActive = false;

        AnimBehaviour.PlayAnimation(Constants.ENEMY_SHIELDED_STANCE_ANIM_STATE);
    }
}
