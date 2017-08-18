using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRecoverStateBC : FSMBehaviourController
{
    public FSMTransitionBehaviour FSMTransitionBehaviour;

    public float RecoverDuration;

    IEnumerator _recoverRoutine;

    protected override void InitFSMBC()
    {
        StateID = FSMStateID.RECOVER;
    }

    public override void Execute()
    {
        StartRecoverProgress();
    }

    void StartRecoverProgress()
    {
        StopRecoverProgress();

        _recoverRoutine = RecoverProgress();
        StartCoroutine(_recoverRoutine);
    }

    void StopRecoverProgress()
    {
        if (_recoverRoutine != null)
            StopCoroutine(_recoverRoutine);
    }

    IEnumerator RecoverProgress()
    {
        yield return new WaitForSeconds(RecoverDuration);

        FSMTransitionBehaviour.DOFSMTransition(FSMStateID.MOVE);
    }
}
