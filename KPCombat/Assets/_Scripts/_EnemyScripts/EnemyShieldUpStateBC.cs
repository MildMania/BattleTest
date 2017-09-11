using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShieldUpStateBC : FSMBehaviourController
{
    public AnimationBehaviour AnimBehaviour;
    public FSMTransitionBehaviour FSMTransitionBehaviour;

    public DamagableBase ShieldDamagable;
    public DamagableBase BaseDamagable;

    protected override void InitFSMBC()
    {
        StateID = FSMStateID.SHIELD_UP;
    }

    public override void Execute()
    {
        base.Execute();

        ShieldDamagable.IsReactionActive = true;
        BaseDamagable.IsReactionActive = false;

        AnimBehaviour.PlayAnimation(Constants.ENEMY_SHIELD_UP_ANIM_STATE).OnComplete(OnShieldUpAnimCompleted);
    }

    void OnShieldUpAnimCompleted()
    {
        FSMTransitionBehaviour.DOFSMTransition(FSMStateID.SHIELDED_STANCE);

        FireOnExecutionCompleted();
    }
}
