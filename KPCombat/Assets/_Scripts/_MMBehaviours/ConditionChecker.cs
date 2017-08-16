using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class ConditionChecker : MonoBehaviour
{
    protected virtual void Awake()
    {
        InitRequiredComponents();

        SetParameters();
    }

    protected virtual void InitRequiredComponents()
    {

    }

    protected virtual void SetParameters()
    {

    }

    public abstract void CheckCondition(Action<bool> callback);
}