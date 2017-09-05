using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackStateBC : FSMBehaviourController
{
    public FSMTransitionBehaviour FSMTransition;
    public AnimationBehaviour AnimationBehaviour;

    protected override void InitFSMBC()
    {
        StateID = FSMStateID.MELEE_ATTACK;
    }

    public override void Execute()
    {
        base.Execute();

        AnimationBehaviour.PlayAnimation(Constants.ENEMY_MELEE_ATTACK_ANIM_STATE).OnComplete(OnAnimationCompleted);
    }

    void OnAnimationCompleted()
    {
        FSMTransition.DOFSMTransition(FSMStateID.MOVE);

        FireOnExecutionCompleted();
    }

    public override void Exit()
    {
        AnimationBehaviour.UnRegisterOnComplete(OnAnimationCompleted);

        base.Exit();
    }
}
