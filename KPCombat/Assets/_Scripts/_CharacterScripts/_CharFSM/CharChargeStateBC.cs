using System;
using System.Collections;
using UnityEngine;

public class CharChargeStateBC : FSMBehaviourController
{
    public float ChargeDuration;

    IEnumerator _waitForChargeRoutine;

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

        Animationbehaviour.PlayAnimation(Constants.CHAR_MELEE_CHARGE_ENTER_ANIM_STATE);

        StartWaitForChargeProgress();
    }

    void StartWaitForChargeProgress()
    {
        StopWaitForChargeProgress();

        _waitForChargeRoutine = WaitForChargeProgress();
        StartCoroutine(_waitForChargeRoutine);
    }

    void StopWaitForChargeProgress()
    {
        if (_waitForChargeRoutine != null)
            StopCoroutine(_waitForChargeRoutine);
    }

    public override void Exit()
    {
        FireOnChargeInterrupted();

        _isChargeCompleted = false;

        StopWaitForChargeProgress();

        CharacterInputController.Instance.IsChargePressed = false;

        base.Exit();
    }

    IEnumerator WaitForChargeProgress()
    {
        yield return new WaitForSeconds(ChargeDuration);

        ChargeCompleted();
    }

    void ChargeCompleted()
    {
        _isChargeCompleted = true;

        FireOnChargeCompleted();

        Debug.Log("<color=cyan>Charge Completed!</color>");
    }
}