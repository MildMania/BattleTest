using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackStateBC : FSMBehaviourController
{
    public FSMTransitionBehaviour FSMTransition;
    public AnimationBehaviour AnimationBehaviour;

    public MMBasicAnimController AttackCollderAnimController;

    const string ATTACK_COLLIDER_STATE = "ATTACK1";

    protected override void InitFSMBC()
    {
        StateID = FSMStateID.MELEE_ATTACK;
    }

    public override void Execute()
    {
        base.Execute();

        AttackCollderAnimController.PlayAnimation(ATTACK_COLLIDER_STATE);

        AnimationBehaviour.PlayAnimation((int)EnemyAnimEnum.Melee_Attack).OnComplete(OnAnimationCompleted);
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
