using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharShieldUpStateBC : FSMBehaviourController
{
    public CharShieldDownStateBC ShieldDownBC;

    public AnimationBehaviour AnimationBehaviour;
    public RunBehaviour RunBehaviour;
    public FSMTransitionBehaviour FSMTransitionBehaviour;

    public float MaxShieldedModeDuration;
    public float MinShieldedModeDuration;

    public float ShieldMoveSpeed;

    public float CurShieldedModeDuration { get; private set; }

    IEnumerator _waitForAuroShieldDownRoutine;

    protected override void InitFSMBC()
    {
        StateID = FSMStateID.SHIELD_UP;
    }

    public override void Execute()
    {
        RunBehaviour.RunSpeed = ShieldMoveSpeed;

        RunBehaviour.StartMovement();

        AnimationBehaviour.PlayAnimation(Constants.CHAR_SHIELD_UP_ANIM_STATE);

        StartWaitForAutoShieldDownProgress();
    }

    void StartWaitForAutoShieldDownProgress()
    {
        StopWaitForAutoShieldDownProgress();

        _waitForAuroShieldDownRoutine = WaitForAutoShieldDownProgress();
        StartCoroutine(_waitForAuroShieldDownRoutine);
    }

    void StopWaitForAutoShieldDownProgress()
    {
        if (_waitForAuroShieldDownRoutine != null)
            StopCoroutine(_waitForAuroShieldDownRoutine);
    }

    IEnumerator WaitForAutoShieldDownProgress()
    {
        CurShieldedModeDuration = 0;

        do
        {
            CurShieldedModeDuration += Time.deltaTime;

            yield return null;
        } while (CurShieldedModeDuration <= MaxShieldedModeDuration);

        ShieldDownBC.NextStateID = FSMStateID.MOVE;

        FSMTransitionBehaviour.DOFSMTransition(FSMStateID.SHIELD_DOWN);
    }

    public override void Exit()
    {
        StopWaitForAutoShieldDownProgress();

        RunBehaviour.Stop();

        base.Exit();
    }
}
