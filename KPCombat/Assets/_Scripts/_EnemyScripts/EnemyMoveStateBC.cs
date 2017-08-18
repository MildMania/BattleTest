﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveStateBC : FSMBehaviourController
{
    public RunBehaviour Runbehaviour;

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
    }

    public override void Stop()
    {
        base.Stop();

        Runbehaviour.Stop();
    }
}
