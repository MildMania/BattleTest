using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMoveStateBC : FSMBehaviourController
{
    public RunBehaviour Runbehaviour;
    public AnimationBehaviour AnimationBehaviour;

    public Vector2 MoveDirection;
    public float MoveSpeed;

    protected override void InitFSMBC()
    {
        StateID = FSMStateID.MOVE;
    }

    public override void Execute()
    {
        base.Execute();

        Runbehaviour.HeadDirection = MoveDirection;
        Runbehaviour.RunSpeed = MoveSpeed;

        Runbehaviour.StartMovement();

        AnimationBehaviour.PlayAnimation((int)CharacterAnimEnum.Walk, true);
    }

    public override void Stop()
    {
        base.Stop();

        Runbehaviour.Stop();

        AnimationBehaviour.Stop();
    }
}
