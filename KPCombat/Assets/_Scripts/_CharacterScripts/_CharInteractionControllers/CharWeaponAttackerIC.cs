using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharWeaponAttackerIC : AttackerICBase
{
    public CharAttackStateBC AttackStateBC;
    public CharPushedBackStateBC PushedBackStateBC;

    public ScreenShakeBehaviour ScreenShakeBehaviour;

    public FSMTransitionBehaviour FSMTransitionBehaviour;
    public KnockBackBehaviour KnockBackBehaviour;

    public Vector2 ReflectDirection;
    public float ReflectAmount;
    public float ReflectSpeed;

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
    }

    protected override void OnAttackReflected(DamagableBase damagable, AttackInteractionInfo attackInteractionInfo)
    {
        KnockBackBehaviour.KnockBackDirection = ReflectDirection;
        KnockBackBehaviour.KnockBackGridCount = ReflectAmount;
        KnockBackBehaviour.KnockBackGridCountPerSec = ReflectSpeed;

        PushedBackStateBC.PushBackAnimState = Constants.CHAR_KNOCKBACK_ANIM_STATE;

        FSMTransitionBehaviour.DOFSMTransition(FSMStateID.PUSHED_BACK);
    }
}
