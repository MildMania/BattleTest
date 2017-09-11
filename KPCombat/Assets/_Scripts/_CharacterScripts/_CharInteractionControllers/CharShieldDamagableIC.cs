using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharShieldDamagableIC : DamagableICBase
{
    public CharTookDamageBC TookDamageStateBC;

    public FSMTransitionBehaviour FsmTransitionBehaviour;

    protected override void OnDamagableSurvived(DamagableBase damagable, AttackInteractionInfo attackInfo)
    {
        TookDamageStateBC.AttackInteractionInfo = attackInfo;

        FsmTransitionBehaviour.DOFSMTransition(FSMStateID.TOOK_DAMAGE);
    }

    protected override void OnAtackReflected(DamagableBase damagable, AttackInteractionInfo attackInfo)
    {
        FsmTransitionBehaviour.DOFSMTransition(FSMStateID.RECOVER);
    }
}
