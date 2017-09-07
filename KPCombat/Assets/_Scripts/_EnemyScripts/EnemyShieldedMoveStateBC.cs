using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShieldedMoveStateBC : FSMBehaviourController
{
    public RunBehaviour Runbehaviour;
    public AnimationBehaviour AnimBehaviour;

    public Vector2 MoveDirection;
    public float MoveSpeed;

    protected override void InitFSMBC()
    {
        StateID = FSMStateID.SHIELDED_MOVE;
    }

    public override void Execute()
    {
        base.Execute();

        AnimBehaviour.PlayAnimation(Constants.ENEMY_SHIELDED_MOVE_ANIM_STATE);

        Runbehaviour.HeadDirection = MoveDirection;
        Runbehaviour.RunSpeed = MoveSpeed;

        Runbehaviour.StartMovement();
    }

    public override void Stop()
    {
        base.Stop();

        Runbehaviour.Stop();
    }
}
