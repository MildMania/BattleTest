using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharDashAttackerIC : AttackerICBase
{
    public CharDashStateBC DashStateBC;
    public CharDashExitStateBC DashExitStateBC;

    public FSMTransitionBehaviour FSMTransitionBehaviour;
    public KnockBackBehaviour KnockBackBehaviour;

    public ScreenShakeBehaviour ScreenShakeBehaviour;

    public float DamageGivenReflectAmount;
    public float DamageGivenReflectSpeed;

    public Vector2 ReflectDirection;
    public float ReflectAmount;
    public float ReflectSpeed;

    protected override void OnReadyToInteract()
    {
        base.OnReadyToInteract();

        AttackerBase attacker = ((AttackerBase)_targetInteraction);

        CharAttackInfo attackInfo = DashStateBC.DashAttackInfo;

        attacker.SetAttackParameters(attackInfo.BaseDamage, attackInfo.KnockBackAmount, attackInfo.AttackDirection, attackInfo.AttackType);
    }

    protected override void OnDamageGiven(DamagableBase damagable, AttackInteractionInfo attackInteractionInfo)
    {
        ScreenShakeBehaviour.Shake();

        KnockBackBehaviour.KnockBackDirection = ReflectDirection;
        KnockBackBehaviour.KnockBackGridCount = DamageGivenReflectAmount;
        KnockBackBehaviour.KnockBackGridCountPerSec = DamageGivenReflectSpeed;

        DashExitStateBC.NextStateID = FSMStateID.PUSHED_BACK;

        FSMTransitionBehaviour.DOFSMTransition(FSMStateID.DASH_EXIT);
    }

    protected override void OnAttackReflected(DamagableBase damagable, AttackInteractionInfo attackInteractionInfo)
    {
        KnockBackBehaviour.KnockBackDirection = ReflectDirection;
        KnockBackBehaviour.KnockBackGridCount = ReflectAmount;
        KnockBackBehaviour.KnockBackGridCountPerSec = ReflectSpeed;

        DashExitStateBC.NextStateID = FSMStateID.PUSHED_BACK;

        FSMTransitionBehaviour.DOFSMTransition(FSMStateID.DASH_EXIT);
    }
}
