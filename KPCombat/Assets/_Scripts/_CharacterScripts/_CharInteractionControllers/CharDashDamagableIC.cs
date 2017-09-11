using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharDashDamagableIC : DamagableICBase
{
    public CharTookDamageBC TookDamageStateBC;

    public FSMTransitionBehaviour FsmTransitionBehaviour;

    protected override void OnDamagableSurvived(DamagableBase damagable, AttackInteractionInfo attackInfo)
    {
        TookDamageStateBC.AttackInteractionInfo = attackInfo;

        FsmTransitionBehaviour.DOFSMTransition(FSMStateID.TOOK_DAMAGE);
    }
}
