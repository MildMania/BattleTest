using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;

public class DamagableBase : Reaction
{
    public float Health;

    public bool IsDestructed { get; protected set; }

    public AttackInteractionInfo CurAttackInfo { get; set; }

    #region Events

    public Action<DamagableBase, AttackInteractionInfo> OnDamageTaken;

    void FireOnDamageTaken()
    {
        if (OnDamageTaken != null)
            OnDamageTaken(this, CurAttackInfo);
    }

    public Action<DamagableBase, AttackInteractionInfo> OnDamagableSurvived;

    void FireOnDamagableSurvived()
    {
        if (OnDamagableSurvived != null)
            OnDamagableSurvived(this, CurAttackInfo);
    }

    public Action<DamagableBase, AttackInteractionInfo> OnDamagableDestructed;

    void FireOnDamagableDestructed()
    {
        if (OnDamagableDestructed != null)
            OnDamagableDestructed(this, CurAttackInfo);
    }

    #endregion

    protected override void SetReactionType()
    {
        InteractionType = InteractionType.Combat;
    }

    public void SetDamagableActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }

    public override bool CanReact()
    {
        if (!gameObject.activeInHierarchy
            || IsDestructed)
            return false;

        return base.CanReact();
    }

    public override void React(Interaction interaction, InteractionInfo interactionInfo)
    {
        base.React(interaction, interactionInfo);

        TakeDamage();
    }

    protected void TakeDamage()
    {
        CurAttackInfo = (AttackInteractionInfo)CurInteractionInfo;

        if (IsDebugEnabled)
            Debug.Log("<color=red>Damage Taken: " + CurAttackInfo.DamageAmount + "</color>");


        if (CurAttackInfo.IsDamageGiven)
			DamageTaken(CurAttackInfo);

        ReactionCompleted();
    }

    void DamageTaken(AttackInteractionInfo ai)
    {
		Health -= -ai.DamageAmount;

        if (Health <= 0)
	        DamgableDied();
        else
	        DamagableSurvived();

		FireOnDamageTaken();
    }

    void DamgableDied()
    {
        IsDestructed = true;

        FireOnDamagableDestructed();
    }

    void DamagableSurvived()
    {
        FireOnDamagableSurvived();
    }
}