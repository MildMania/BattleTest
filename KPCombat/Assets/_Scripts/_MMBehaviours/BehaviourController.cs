using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class BehaviourController : MonoBehaviour
{
    #region Events
    public Action OnExecutionCompleted;

    protected void FireOnExecutionCompleted()
    {
        if (OnExecutionCompleted != null)
            OnExecutionCompleted();
    }
    #endregion

    protected virtual void Awake()
    {
        InitRequiredComponents();

        SetParameters();

        StartListeningEvents();
    }

    protected virtual void OnDestroy()
    {
        FinishListeningEvents();
    }

    protected virtual void InitRequiredComponents()
    {

    }

    protected virtual void SetParameters()
    {

    }

    protected virtual void StartListeningEvents()
    {

    }

    protected virtual void FinishListeningEvents()
    {

    }

    public abstract void Execute();

    public virtual void Stop()
    {
        ResetEvents();
    }

    void ResetEvents()
    {
        if (OnExecutionCompleted == null)
            return;

        foreach (Action action in OnExecutionCompleted.GetInvocationList())
        {
            OnExecutionCompleted -= action;
        }
    }
}