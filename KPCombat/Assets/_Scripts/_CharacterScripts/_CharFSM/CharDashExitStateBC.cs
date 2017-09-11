using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharDashExitStateBC : FSMBehaviourController
{
    public FSMStateID NextStateID { get; set; }

    public AnimationBehaviour AnimationBehaviour;
    public AnimationBasedMovementBehaviour MovementBehaviour;
    public FSMTransitionBehaviour FSMTransitionBehaviour;

    public AttackerBase DashAttacker;
    public DamagableBase DashDamagable;
    public DamagableBase BaseDamagable;

    public PusherBase Pusher;
    public PushableBase Pushable;

    IEnumerator _dashExitProgress;

    protected override void InitFSMBC()
    {
        StateID = FSMStateID.DASH_EXIT;
    }

    public override void Execute()
    {
        StartDashExitProgress();
    }

    void StartDashExitProgress()
    {
        StopDashExitProgress();

        _dashExitProgress = DashExitProgress();
        StartCoroutine(_dashExitProgress);
    }

    void StopDashExitProgress()
    {
        if (_dashExitProgress != null)
            StopCoroutine(_dashExitProgress);
    }

    IEnumerator DashExitProgress()
    {
        AnimationBehaviour.PlayAnimation(Constants.CHAR_DASH_EXIT_ANIM_STATE);

        FSMTransitionBehaviour.DOFSMTransition(NextStateID);

        FireOnExecutionCompleted();

        yield break;
    }

    public override void Exit()
    {
        base.Exit();

        DashAttacker.IsInteractionActive = false;

        DashDamagable.IsReactionActive = false;
        BaseDamagable.IsReactionActive = true;

        Pusher.IsInteractionActive = true;
        Pushable.IsReactionActive = true;
    }
}
