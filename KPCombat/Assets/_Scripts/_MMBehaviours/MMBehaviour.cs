using UnityEngine;
using System;

public class MMBehaviour : MonoBehaviour
{
    #region Events

    protected Action _onComplete;

    protected void FireOnComplete()
    {
        if (_onComplete == null)
            return;

        _onComplete();

        foreach (Action action in _onComplete.GetInvocationList())
        {
            _onComplete -= action;
        }
    }

    #endregion

    public void OnComplete(Action callback)
    {
        _onComplete += callback;
    }

    public void UnRegisterOnComplete(Action callback)
    {
        _onComplete -= callback;
    }
}