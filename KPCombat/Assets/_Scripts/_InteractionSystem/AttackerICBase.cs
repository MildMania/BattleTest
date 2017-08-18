using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(AttackerBase))]
public class AttackerICBase : InteractionControllerBase
{
    protected override void InitTargetInteraction()
    {
        _targetInteraction = GetComponent<AttackerBase>();
    }
    
    protected override void StartListeningEvents()
    {
        base.StartListeningEvents();

        ((AttackerBase)_targetInteraction).OnDamageGiven += OnDamageGiven;
    }

    protected override void FinishListeningEvents()
    {
        base.FinishListeningEvents();

        ((AttackerBase)_targetInteraction).OnDamageGiven += OnDamageGiven;
    }

    protected virtual void OnDamageGiven(DamagableBase damagable, AttackInteractionInfo attackInteractionInfo)
    {
    }
}
