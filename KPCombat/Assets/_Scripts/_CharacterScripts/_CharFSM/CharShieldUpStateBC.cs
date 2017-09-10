using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharShieldUpStateBC : FSMBehaviourController
{
    public CharShieldDownStateBC ShieldDownBC;

    public AnimationBehaviour AnimationBehaviour;
    public RunBehaviour RunBehaviour;
    public FSMTransitionBehaviour FSMTransitionBehaviour;

    public float ShieldedModeDuration;
    public float ShieldMoveSpeed;

    IEnumerator _shieldedModeRoutine;

    protected override void InitFSMBC()
    {
        StateID = FSMStateID.SHIELD_UP;
    }

    public override void Execute()
    {
        StartShieldedModeProgress();
    }

    void StartShieldedModeProgress()
    {
        StopShieldedModeProgress();

        _shieldedModeRoutine = ShieldedModeProgress();
        StartCoroutine(_shieldedModeRoutine);
    }

    void StopShieldedModeProgress()
    {
        if (_shieldedModeRoutine != null)
            StopCoroutine(_shieldedModeRoutine);
    }

    IEnumerator ShieldedModeProgress()
    {
        RunBehaviour.RunSpeed = ShieldMoveSpeed;

        RunBehaviour.StartMovement();

        AnimationBehaviour.PlayAnimation(Constants.CHAR_SHIELD_UP_ANIM_STATE);

        yield return new WaitForSeconds(ShieldedModeDuration);

        OnShieldedModeFinished();
    }

    void OnShieldedModeFinished()
    {
        ShieldDownBC.NextStateID = FSMStateID.MOVE;

        FSMTransitionBehaviour.DOFSMTransition(FSMStateID.SHIELD_DOWN);

        FireOnExecutionCompleted();
    }

    public override void Exit()
    {
        RunBehaviour.Stop();

        base.Exit();
    }
}
