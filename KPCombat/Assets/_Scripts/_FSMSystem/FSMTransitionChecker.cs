using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FSMTransitionChecker : ConditionChecker
{
    public FSMStateID StateID { get; set; }

    protected override void Awake()
    {
        base.Awake();
        InitFSMChecker();

    }

    protected abstract void InitFSMChecker();

    public override void CheckCondition(Action<bool> callback)
    {
        callback(true);
    }
}