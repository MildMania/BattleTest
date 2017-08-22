using System;
using UnityEngine;

public class CharChargeStateBC : FSMBehaviourController
{
    #region Events
    public Action OnChargeCompleted;

    void FireOnChargeCompleted()
    {
        if (OnChargeCompleted != null)
            OnChargeCompleted();
    }
    #endregion

    public AnimationBehaviour Animationbehaviour;

    protected override void InitFSMBC()
    {
        StateID = FSMStateID.MELEE_CHARGE;
    }

    public override void Execute()
    {
        base.Execute();

        Animationbehaviour.PlayAnimation((int)CharacterAnimEnum.Melee_Charge).OnComplete(OnChargeEnterAnimCompleted);
    }

    void OnChargeEnterAnimCompleted()
    {
        FireOnChargeCompleted();
    }
}