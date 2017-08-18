using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ReactionControllerBase : MonoBehaviour
{
    protected Reaction _targetReaction;

	protected abstract void InitTargetReaction();

	protected virtual void Awake()
    {
        InitRequiredComponents();
        
        InitTargetReaction();

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
        _targetReaction.OnReadyToReact += OnReadyToReact;
        _targetReaction.OnReactionCompleted += OnReactionCompleted;
        _targetReaction.OnReactionCancelled += OnReactionCancelled;
    }

    protected virtual void FinishListeningEvents()
    {
        _targetReaction.OnReadyToReact -= OnReadyToReact;
        _targetReaction.OnReactionCompleted -= OnReactionCompleted;
        _targetReaction.OnReactionCancelled -= OnReactionCancelled;
    }

    protected virtual void OnReadyToReact()
    {
        
    }

    protected virtual void OnReactionCompleted(Reaction reaction)
    {
        
    }

    protected virtual void OnReactionCancelled(Reaction reaction)
    {
        
    }
}
