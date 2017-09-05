using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharSuperStrikeStateBC : FSMBehaviourController
{
    public FSMTransitionBehaviour FSMTransition;
    public AnimationBehaviour AnimationBehaviour;
    public AnimationBasedMovementBehaviour MovementBehaviour;

    public AnimationCurve MovementCurve;
    public float MovementDuration;
    public Vector2 MovementDistance;

    protected override void InitFSMBC()
    {
        StateID = FSMStateID.SUPER_STRIKE;
    }

    public override void Execute()
    {
        AnimationBehaviour.PlayAnimation(Constants.CHAR_SUPER_STRIKE_ANIM_STATE);

        MovementBehaviour.AnimationCurve = MovementCurve;
        MovementBehaviour.MovementDuration = MovementDuration;
        MovementBehaviour.MovementDistance = MovementDistance;

        MovementBehaviour.Move().OnComplete(OnMovementCompleted);
    }

    void OnMovementCompleted()
    {
        FireOnExecutionCompleted();
    }
}
