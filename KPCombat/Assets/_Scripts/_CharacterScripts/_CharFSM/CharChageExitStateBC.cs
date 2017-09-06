using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharChageExitStateBC : FSMBehaviourController
{
    public AnimationBehaviour AnimationBehaviour;
    public FSMTransitionBehaviour FsmTransitionBehaviour;

    protected override void InitFSMBC()
    {
        StateID = FSMStateID.MELEE_CHARGE_EXIT;
    }

    public override void Execute()
    {
        AnimationBehaviour.PlayAnimation(Constants.CHAR_WALK_ANIM_STATE);
        FsmTransitionBehaviour.DOFSMTransition(FSMStateID.MOVE);

        //AnimationBehaviour.PlayAnimation(Constants.CHAR_MELEE_CHARGE_EXIT_ANIM_STATE).OnComplete(OnAnimationComplete);
    }

    public override void Exit()
    {
        //AnimationBehaviour.UnRegisterOnComplete(OnAnimationComplete);

        base.Exit();
    }

    void OnAnimationComplete()
    {
        Debug.Log("animation completedd");

        //_animationBehaviour.PlayAnimation(Constants.CHAR_WALK_ANIM_STATE);

    }
}
