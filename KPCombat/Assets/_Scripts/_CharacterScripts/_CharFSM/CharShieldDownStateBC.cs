using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharShieldDownStateBC : FSMBehaviourController
{
    public FSMStateID NextStateID { get; set; }

    public AnimationBehaviour Animationbehaviour;
    public FSMTransitionBehaviour FSMTransitionBehaviour;
    public RunBehaviour RunBehaviour;

    public float ShieldMoveSpeed;

    public DamagableBase BaseDamagable;
    public DamagableBase ShieldDamagable;

    protected override void InitFSMBC()
    {
        StateID = FSMStateID.SHIELD_DOWN;
    }

    public override void Execute()
    {
        RunBehaviour.RunSpeed = ShieldMoveSpeed;

        RunBehaviour.StartMovement();

        Animationbehaviour.PlayAnimation(Constants.CHAR_SHIELD_DOWN_ANIM_STATE).OnComplete(OnShieldDownAnimationCompleted);
    }

    void OnShieldDownAnimationCompleted()
    {
        FSMTransitionBehaviour.DOFSMTransition(NextStateID);

        FireOnExecutionCompleted();
    }

    public override void Exit()
    {
        base.Exit();

        BaseDamagable.IsReactionActive = true;
        ShieldDamagable.IsReactionActive = false;

        RunBehaviour.Stop();

        Animationbehaviour.UnRegisterOnComplete(OnShieldDownAnimationCompleted);
    }
}
