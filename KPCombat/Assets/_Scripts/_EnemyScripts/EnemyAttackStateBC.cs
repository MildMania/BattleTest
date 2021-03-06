﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackStateBC : FSMBehaviourController
{
    public FSMTransitionBehaviour FSMTransition;
    public AnimationBehaviour AnimationBehaviour;

    public AttackerBase Attacker;

    public PusherBase Pusher;
    public PushableBase Pushable;

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
        Attacker.IsInteractionActive = false;

        Pusher.IsInteractionActive = true;
        Pushable.IsReactionActive = true;

        AnimationBehaviour.UnRegisterOnComplete(OnAnimationCompleted);

        base.Exit();
    }
}
