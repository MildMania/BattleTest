using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DamagableBase))]
public class DamagableICBase : ReactionControllerBase
{
    protected override void InitTargetReaction()
    {
        _targetReaction = GetComponent<DamagableBase>();
    }
    
    protected override void StartListeningEvents()
    {
        base.StartListeningEvents();

        ((DamagableBase)_targetReaction).OnDamageTaken += OnDamageTaken;
        ((DamagableBase)_targetReaction).OnDamagableSurvived += OnDamagableSurvived;
        ((DamagableBase)_targetReaction).OnDamagableDestructed += OnDamagableDestructed;
    }

    protected override void FinishListeningEvents()
    {
        base.FinishListeningEvents();

        ((DamagableBase)_targetReaction).OnDamageTaken -= OnDamageTaken;
        ((DamagableBase)_targetReaction).OnDamagableSurvived -= OnDamagableSurvived;
        ((DamagableBase)_targetReaction).OnDamagableDestructed -= OnDamagableDestructed;
    }

    protected virtual void OnDamageTaken(DamagableBase damagable, AttackInteractionInfo attackInfo)
    {

    }

    protected virtual void OnDamagableSurvived(DamagableBase damagable, AttackInteractionInfo attackInfo)
    {

    }

    protected virtual void OnDamagableDestructed(DamagableBase damagable, AttackInteractionInfo attackInfo)
    {

    }
}
