using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharWeaponAttackerIC : AttackerICBase
{
    public CharAttackStateBC AttackStateBC;

    public ScreenShakeBehaviour ScreenShakeBehaviour;

    protected override void OnReadyToInteract()
    {
        base.OnReadyToInteract();

		AttackerBase attacker = ((AttackerBase)_targetInteraction);

        CharAttackInfo attackInfo = AttackStateBC.CurAttackInfo;

        attacker.SetAttackParameters(attackInfo.BaseDamage, attackInfo.KnockBackAmount, attackInfo.AttackDirection, attackInfo.AttackType);
    }

    protected override void OnDamageGiven(DamagableBase damagable, AttackInteractionInfo attackInteractionInfo)
    {
        ScreenShakeBehaviour.Shake();

        base.OnDamageGiven(damagable, attackInteractionInfo);


    }
}
