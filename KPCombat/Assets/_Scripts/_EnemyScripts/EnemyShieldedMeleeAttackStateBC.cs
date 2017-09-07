using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShieldedMeleeAttackStateBC : FSMBehaviourController
{
    public FSMTransitionBehaviour FSMTransition;
    public AnimationBehaviour AnimationBehaviour;

    public AttackerBase Attacker;

    protected override void InitFSMBC()
    {
        StateID = FSMStateID.SHIELDED_MELEE_ATTACK;
    }

    public override void Execute()
    {
        base.Execute();

        AnimationBehaviour.PlayAnimation(Constants.ENEMY_SHIELDED_MELEE_ATTACK_ANIM_STATE).OnComplete(OnAnimationCompleted);
    }

    void OnAnimationCompleted()
    {
        FSMTransition.DOFSMTransition(FSMStateID.SHIELDED_STANCE);

        FireOnExecutionCompleted();
    }

    public override void Exit()
    {
        Attacker.IsInteractionActive = false;

        AnimationBehaviour.UnRegisterOnComplete(OnAnimationCompleted);

        base.Exit();
    }
}
