using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharRecoverStateBC : FSMBehaviourController
{
    public CharShieldDownStateBC ShieldDownBC;

    public float KnockBackGridCount;
    public float KnockBackGridCountPerSec;

    public KnockBackBehaviour KnockbackBehaviour;
    public FSMTransitionBehaviour FSMTransitionBehaviour;
    public ScreenShakeBehaviour ShakeBehaviour;

    protected override void InitFSMBC()
    {
        StateID = FSMStateID.RECOVER;
    }

    public override void Execute()
    {
        ShakeBehaviour.Shake();

        KnockbackBehaviour.KnockBackGridCount = KnockBackGridCount;
        KnockbackBehaviour.KnockBackGridCountPerSec = KnockBackGridCountPerSec;

        KnockbackBehaviour.KnockBack().OnComplete(OnKnockBackCompleted);
    }

    protected virtual void OnKnockBackCompleted()
    {
        FireOnExecutionCompleted();

        ShieldDownBC.NextStateID = FSMStateID.MOVE;

        FSMTransitionBehaviour.DOFSMTransition(FSMStateID.SHIELD_DOWN);
    }

    public override void Exit()
    {
        base.Exit();

        KnockbackBehaviour.UnRegisterOnComplete(OnKnockBackCompleted);
    }
}
