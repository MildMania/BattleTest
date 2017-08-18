using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

public abstract class Interaction : MMGameSceneBehaviour
{
    public InteractionType InteractionType { get; protected set; }

    public bool IsInteractionActiveOnStart = true;

    public bool InteractWithNewTarget;

    public bool IsDebugActive;

    public bool IsInteractionActive { get; set; }

    protected Reaction _newTarget;

    protected List<Reaction> _targetReactionList;

    protected IEnumerator _checkTargetReactionsRoutine;

    #region Events

    public Action OnReadyToInteract;

    void FireOnReadyToInteract()
    {
        if (OnReadyToInteract != null)
            OnReadyToInteract();
    }

    public Action OnStarted;

    void FireOnStarted()
    {
        if (OnStarted != null)
            OnStarted();
    }

    public Action OnFailed;

    void FireOnFailed()
    {
        if (OnFailed != null)
            OnFailed();
    }

    public Action<Reaction> OnNewTargetAdded;

    void FireOnNewTargetAdded(Reaction target)
    {
        if (OnNewTargetAdded != null)
            OnNewTargetAdded(target);
    }

    public Action<Reaction> OnInteractableRemoved;

    void FireOnTargetRemoved(Reaction target)
    {
        if (OnInteractableRemoved != null)
            OnInteractableRemoved(target);
    }

    public Action<Reaction> OnPreInteract;

    protected void FireOnPreInteract(Reaction target)
    {
        if (OnPreInteract != null)
            OnPreInteract(target);
    }

    public Action OnCancelled;

    void FireOnCancelled()
    {
        if (OnCancelled != null)
            OnCancelled();
    }

    public Action OnCompleted;

    void FireOnCompleted()
    {
        if (OnCompleted != null)
            OnCompleted();
    }

    #endregion

    protected override void Awake()
    {
        InitInteractionBase();

        base.Awake();
    }

    protected override void OnGameStarted()
    {
        base.OnGameStarted();

        IsInteractionActive = IsInteractionActiveOnStart;
    }

    public virtual void InitInteractionBase()
    {
        InitTargetInteractableList();

        SetInteractionType();
    }

    protected abstract void SetInteractionType();

    protected void InitTargetInteractableList()
    {
        _targetReactionList = new List<Reaction>();
    }

    public bool CheckAndAddToTargetInteractableList(Reaction reaction)
    {
        if (!gameObject.activeInHierarchy
            || !IsInteractionActive)
            return false;

        if (_targetReactionList.Contains(reaction)
            || !IsValidReaction(reaction))
            return false;

        _targetReactionList.Add(reaction);

        _newTarget = reaction;

        FireOnNewTargetAdded(reaction);

		if (_targetReactionList.Count == 1)
		{
			StartCheckTargetReactionsProgress();
			ReadyToInteract();
		}

		if (IsDebugActive)
			Debug.Log("target count: " + _targetReactionList.Count);

		if (InteractWithNewTarget)
            InteractWithInteractable(_newTarget);
        
        return true;
    }

    void StartCheckTargetReactionsProgress()
    {
        StopCheckTargetReactionsProgress();

        _checkTargetReactionsRoutine = CheckTargetReactionsProgress();
        StartCoroutine(_checkTargetReactionsRoutine);
    }

    void StopCheckTargetReactionsProgress()
    {
        if (_checkTargetReactionsRoutine != null)
            StopCoroutine(_checkTargetReactionsRoutine);
    }

    IEnumerator CheckTargetReactionsProgress()
    {
        while (true)
        {
            for (int i = 0; i < _targetReactionList.Count; i++)
            {
                if (!_targetReactionList[i].CanReact())
                {
                    RemoveFromTargetInteractableList(_targetReactionList[i]);
                    i--;
                }
            }
            yield return null;
        }
    }

    public void RemoveFromTargetInteractableList(Reaction target)
    {
        if (!_targetReactionList.Contains(target))
            return;

        _targetReactionList.Remove(target);

        target.InteractionCancelled(this);

        FireOnTargetRemoved(target);

        if (_targetReactionList.Count == 0)
        {
            InteractionCancelled();

            StopCheckTargetReactionsProgress();
        }
    }

    public virtual void InteractWithTargetInteractables()
    {
        InteractionStarted();

        if (_targetReactionList.Count == 0)
			InteractionCompleted();

        foreach (Reaction r in _targetReactionList)
            InteractWithInteractable(r);
    }

    public virtual void InteractWithInteractable(Reaction target)
    {
        FireOnPreInteract(target);

        target.React(this, GetInteractionInfo(null));

        InteractionCompleted();
    }

    protected virtual void ReadyToInteract()
    {
        FireOnReadyToInteract();
    }

    protected virtual void InteractionStarted()
    {
        FireOnStarted();
    }

    protected virtual void InteractionCancelled()
    {
        foreach (Reaction r in _targetReactionList)
            r.InteractionCancelled(this);

        FireOnCancelled();
    }

    public virtual bool CanInteract()
    {
        return true;
    }

    protected virtual void InteractionCompleted()
    {
        FireOnCompleted();
    }

    public void ReactionCancelled(Reaction reaction)
    {
        if (!_targetReactionList.Remove(reaction))
            return;

        if (_targetReactionList.Count == 0)
            InteractionCancelled();
    }

    protected bool IsValidReaction(Reaction reaction)
    {
        bool isValid = true;

        if (reaction.InteractionType != InteractionType
            || !reaction.CanReact())
            isValid = false;

        return isValid;
    }

    protected abstract InteractionInfo GetInteractionInfo(object message);
}
