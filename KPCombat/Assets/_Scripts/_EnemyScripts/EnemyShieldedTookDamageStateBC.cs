using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShieldedTookDamageStateBC : TookDamageStateBC
{
    public EnemyShieldedKnockbackStateBC KnockBackBC;

    public FlashBehaviour FlashBehaviour;

    public DamagableBase ShieldDamagable;
    public DamagableBase BaseDamagable;

    protected override void InitFSMBC()
    {
        StateID = FSMStateID.SHIELDED_TOOK_DAMAGE;
    }

    public override void Execute()
    {
        FlashBehaviour.Flash();

        KnockBackBC.KnockBackAnimation = Constants.ENEMY_SHIELDED_TOOK_DAMAGE_ANIM_STATE;

        base.Execute();
    }


    public override void Exit()
    {
        base.Exit();

        ShieldDamagable.IsReactionActive = false;
        BaseDamagable.IsReactionActive = true;
    }
}
