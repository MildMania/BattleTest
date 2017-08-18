using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseDamagableIC : DamagableICBase
{
    public EnemyTookDamageBC TookDamageStateBC;

    public FSMTransitionBehaviour FsmTransitionBehaviour;

    protected override void OnDamagableSurvived(DamagableBase damagable, AttackInteractionInfo attackInfo)
    {
        Collider2D attackerCollider = attackInfo.Attacker.GetComponent<Collider2D>();
        Collider2D damagableCollider = damagable.GetComponent<Collider2D>();

        float yDist = attackerCollider.bounds.max.y - damagableCollider.bounds.min.y;

        attackInfo.KnockBackGridAmout += yDist;

        TookDamageStateBC.AttackInteractionInfo = attackInfo;

        FsmTransitionBehaviour.DOFSMTransition(FSMStateID.TOOK_DAMAGE);
    }

    /*protected override void OnDamagableDestructed(DamagableBase damagable, AttackInteractionInfo attackInfo)
    {
        DeadStateBC.AttackInteractionInfo = attackInfo;

        _fsmTransitiomBehaviour.DOFSMTransition(FSMStateID.DEAD);
    }*/
}
