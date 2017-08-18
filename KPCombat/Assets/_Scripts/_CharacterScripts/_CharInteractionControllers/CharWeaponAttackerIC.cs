using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharWeaponAttackerIC : AttackerICBase
{
    public CharAttackStateBC AttackStateBC;

    protected override void OnReadyToInteract()
    {
        base.OnReadyToInteract();

		AttackerBase attacker = ((AttackerBase)_targetInteraction);

        CharAttackInfo attackInfo = AttackStateBC.CurAttackInfo;

        attacker.SetAttackParameters(attackInfo.BaseDamage, attackInfo.KnockBackAmount);
    }
}
