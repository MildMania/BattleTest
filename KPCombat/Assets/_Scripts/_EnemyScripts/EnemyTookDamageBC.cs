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
                KnockBackBC.KnockBackAnimation = EnemyAnimEnum.TookDamage_Left;
            else if(AttackInteractionInfo.AttackDirection == DirectionEnum.Right)
                KnockBackBC.KnockBackAnimation = EnemyAnimEnum.TookDamage_Right;
        }
        else if(AttackInteractionInfo.AttackType == AttackTypeEnum.Heavy)
        {
            KnockBackBC.KnockBackAnimation = EnemyAnimEnum.KnockBack;
        }

    }
}
