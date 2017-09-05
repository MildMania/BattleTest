using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTookDamageBC : TookDamageStateBC
{
    public EnemyKnockBackBC KnockBackBC;

    public FlashBehaviour FlashBehaviour;

    public override void Execute()
    {
        base.Execute();

        FlashBehaviour.Flash();

        if (AttackInteractionInfo.AttackType == AttackTypeEnum.Light)
        {
            if (AttackInteractionInfo.AttackDirection == DirectionEnum.Left)
                KnockBackBC.KnockBackAnimation = Constants.ENEMY_TOOK_DAMAGE_LEFT_ANIM_STATE;
            else if(AttackInteractionInfo.AttackDirection == DirectionEnum.Right)
                KnockBackBC.KnockBackAnimation = Constants.ENEMY_TOOK_DAMAGE_RIGHT_ANIM_STATE;
        }
        else if(AttackInteractionInfo.AttackType == AttackTypeEnum.Heavy)
        {
            KnockBackBC.KnockBackAnimation = Constants.ENEMY_KNOCKBACK_ANIM_STATE;
        }

    }
}
