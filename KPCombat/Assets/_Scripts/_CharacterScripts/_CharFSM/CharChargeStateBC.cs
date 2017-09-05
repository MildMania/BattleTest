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

    public Action OnChargeInterrupted;

    void FireOnChargeInterrupted()
    {
        if (OnChargeInterrupted != null)
            OnChargeInterrupted();
    }
    #endregion

    public AnimationBehaviour Animationbehaviour;

    bool _isChargeCompleted;

    protected override void InitFSMBC()
    {
        StateID = FSMStateID.MELEE_CHARGE;
    }

    public override void Execute()
    {
        _isChargeCompleted = false;

        base.Execute();

        Animationbehaviour.PlayAnimation(Constants.CHAR_MELEE_CHARGE_ENTER_ANIM_STATE).OnComplete(OnChargeEnterAnimCompleted);
    }

    public override void Exit()
    {
        if (!_isChargeCompleted)
            FireOnChargeInterrupted();

        _isChargeCompleted = false;

        base.Exit();
    }

    void OnChargeEnterAnimCompleted()
    {
        _isChargeCompleted = true;

        FireOnChargeCompleted();

        Debug.Log("<color=cyan>Charge Completed!</color>");
    }
}