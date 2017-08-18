using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractionControllerBase : MonoBehaviour
{
    protected Interaction _targetInteraction;

	protected abstract void InitTargetInteraction();

	protected virtual void Awake()
    {
        InitRequiredComponents();

        InitTargetInteraction();

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
        _targetInteraction.OnReadyToInteract += OnReadyToInteract;
        _targetInteraction.OnPreInteract += OnPreInteract;
        _targetInteraction.OnStarted += OnStarted;
        _targetInteraction.OnFailed += OnFailed;
        _targetInteraction.OnNewTargetAdded += OnNewTargetAdded;
        _targetInteraction.OnInteractableRemoved += OnInteractableRemoved;
        _targetInteraction.OnCancelled += OnCancelled;
        _targetInteraction.OnCompleted += OnComplete;
    }

    protected virtual void FinishListeningEvents()
    {
        _targetInteraction.OnReadyToInteract -= OnReadyToInteract;
        _targetInteraction.OnPreInteract -= OnPreInteract;
        _targetInteraction.OnStarted -= OnStarted;
        _targetInteraction.OnFailed -= OnFailed;
        _targetInteraction.OnNewTargetAdded -= OnNewTargetAdded;
        _targetInteraction.OnInteractableRemoved -= OnInteractableRemoved;
        _targetInteraction.OnCancelled -= OnCancelled;
        _targetInteraction.OnCompleted -= OnComplete;
    }

    protected virtual void OnReadyToInteract()
    {

    }

    protected virtual void OnPreInteract(Reaction target)
    {

    }

    protected virtual void OnStarted()
    {

    }

    protected virtual void OnFailed()
    {

    }

    protected virtual void OnNewTargetAdded(Reaction target)
    {

    }

    protected virtual void OnInteractableRemoved(Reaction target)
    {

    }

    protected virtual void OnCancelled()
    {

    }

    protected virtual void OnComplete()
    {

    }
}
