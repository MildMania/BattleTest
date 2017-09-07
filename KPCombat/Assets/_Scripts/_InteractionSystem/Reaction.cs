using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Reaction : MMGameSceneBehaviour
{
    public InteractionType InteractionType { get; protected set; }

    public bool IsReactionActiveOnStart = true;

    public bool IsDebugEnabled;

    public InteractionInfo CurInteractionInfo { get; private set; }

    public bool IsReactionActive;

    protected List<Interaction> _targetInteractionList;

    #region Events

    public Action OnReadyToReact;

    void FireOnReadyReact()
    {
        if (OnReadyToReact != null)
            OnReadyToReact();
    }

    public Action<Reaction> OnReactionCompleted;

    void FireOnReactionCompleted()
    {
        if (OnReactionCompleted != null)
            OnReactionCompleted(this);
    }

    public Action<Reaction> OnReactionCancelled;

    void FireOnReactionCancelled()
    {
        if (OnReactionCancelled != null)
            OnReactionCancelled(this);
    }

    #endregion

    protected override void Awake()
    {
        InitReaction();

        base.Awake();
    }

    protected override void OnGameStarted()
    {
        base.OnGameStarted();

        IsReactionActive = IsReactionActiveOnStart;
    }

    protected override void OnGameEnded()
    {
        base.OnGameEnded();

        IsReactionActive = false;
    }

    public virtual void InitReaction()
    {
        SetReactionType();

        InitTargetInteractionList();
    }

    protected abstract void SetReactionType();

    protected void InitTargetInteractionList()
    {
        _targetInteractionList = new List<Interaction>();
    }

    public void AddedToInteraction(Interaction interaction)
    {
        _targetInteractionList.Add(interaction);

        if (_targetInteractionList.Count == 1)
        {
            FireOnReadyReact();
        }
    }

    public virtual void InteractionCancelled(Interaction interaction)
    {
        if (!_targetInteractionList.Remove(interaction))
            return;

        if (_targetInteractionList.Count == 0)
            ReactionCancelled();
    }

    protected void ReactionCancelled()
    {
        foreach (Interaction i in _targetInteractionList)
            i.ReactionCancelled(this);

        FireOnReactionCancelled();
    }

    public virtual void React(Interaction interaction, InteractionInfo interactionInfo)
    {
        CurInteractionInfo = interactionInfo;
    }

    public virtual void SetInteractionInfo(params object[] list)
    {

    }

    public virtual bool CanReact()
    {
        return IsReactionActive;
    }

    protected virtual void ReactionCompleted()
    {
        FireOnReactionCompleted();
    }
}
