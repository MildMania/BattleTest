using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShieldDamagableIC : DamagableICBase
{
    public EnemyShieldedTookDamageStateBC TookDamageStateBC;

    public FSMTransitionBehaviour FsmTransitionBehaviour;

    public KnockBackBehaviour KnockBackBehaviour;

    public Vector2 ReflectDirection;
    public float ReflectAmount;
    public float ReflectSpeed;

    protected override void OnDamagableSurvived(DamagableBase damagable, AttackInteractionInfo attackInfo)
    {
        Collider2D attackerCollider = attackInfo.Attacker.GetComponent<Collider2D>();
        Collider2D damagableCollider = damagable.GetComponent<Collider2D>();

        float yDist = attackerCollider.bounds.max.y - damagableCollider.bounds.min.y;

        attackInfo.KnockBackGridAmout += yDist;

        TookDamageStateBC.AttackInteractionInfo = attackInfo;

        FsmTransitionBehaviour.DOFSMTransition(FSMStateID.TOOK_DAMAGE);
    }

    protected override void OnAtackReflected(DamagableBase damagable, AttackInteractionInfo attackInfo)
    {
        KnockBackBehaviour.KnockBackDirection = ReflectDirection;
        KnockBackBehaviour.KnockBackGridCount = ReflectAmount;
        KnockBackBehaviour.KnockBackGridCountPerSec = ReflectSpeed;

        FsmTransitionBehaviour.DOFSMTransition(FSMStateID.PUSHED_BACK);
    }
}
