using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FSMBehaviourController : BehaviourController
{
    public FSMStateID StateID { get; set; }

    protected override void Awake()
    {
        base.Awake();
        InitFSMBC();
    }

    protected abstract void InitFSMBC();

    public override void Execute()
    {
    }

    public virtual void Exit()
    {
        Stop();
    }
}