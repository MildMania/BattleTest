using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;

public class DamagableBase : Reaction
{
    public float Health;

    public bool IsDestructed { get; protected set; }

    public AttackInteractionInfo CurAttackInfo { get; protected set; }

    public List<AttackTypeEnum> ReflectedAttackTypeList;

    #region Events

    public Action<DamagableBase, AttackInteractionInfo> OnDamageTaken;

    void FireOnDamageTaken()
    {
        if (OnDamageTaken != null)
            OnDamageTaken(this, CurAttackInfo);
    }

    public Action<DamagableBase, AttackInteractionInfo> OnAttackReflected;

    void FireOnAttackReflected()
    {
        if (OnAttackReflected != null)
            OnAttackReflected(this, CurAttackInfo);
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

        CurAttackInfo = (AttackInteractionInfo)CurInteractionInfo;

        if (CurAttackInfo.IsDamageGiven)
            DamageTaken();
        else if (CurAttackInfo.IsAttackReflected)
            AttackReflected();

        ReactionCompleted();
    }

    void DamageTaken()
    {
		Health -= -CurAttackInfo.DamageAmount;

        if (Health <= 0)
	        DamgableDied();
        else
	        DamagableSurvived();

        if (IsDebugEnabled)
            Debug.Log("<color=red>Damage Taken: " + CurAttackInfo.DamageAmount + "</color>");

        FireOnDamageTaken();

    }

    void AttackReflected()
    {
        FireOnAttackReflected();
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