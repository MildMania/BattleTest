using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseAttackerIC : AttackerICBase
{
    public EnemyAttackStateBC AttackStateBC;

    public float BaseDamage;
    public float KnockBackAmount;

    protected override void OnReadyToInteract()
    {
        base.OnReadyToInteract();

        AttackerBase attacker = ((AttackerBase)_targetInteraction);

        attacker.SetAttackParameters(BaseDamage, KnockBackAmount, DirectionEnum.Front, AttackTypeEnum.Default);
    }
}
