using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class InteractionCollider : MMGameSceneBehaviour
{
    public bool RemoveTargetOnExit = true;

    public bool IsControllerActive { get; set; }

    List<Interaction> _targetInteractionList;

    protected override void Awake()
    {
        base.Awake();

        InitInteraction();
    }

    protected override void OnGameStarted()
    {
        base.OnGameStarted();

        IsControllerActive = true;
    }

    void InitInteraction()
    {
        _targetInteractionList = GetComponents<Interaction>().ToList();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!IsControllerActive)
            return;

        Reaction r = other.GetComponent<Reaction>();

        foreach(Interaction i in _targetInteractionList)
        {
			if (!CheckIsValidReaction(i, r))
				continue;

            i.CheckAndAddToTargetInteractableList(r);
		}
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (!IsControllerActive)
            return;

        Reaction r = other.GetComponent<Reaction>();

		foreach (Interaction i in _targetInteractionList)
		{
			if (!CheckIsValidReaction(i, r))
				continue;

			i.CheckAndAddToTargetInteractableList(r);
		}
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!IsControllerActive)
            return;

        if (!RemoveTargetOnExit)
            return;

        Reaction r = other.GetComponent<Reaction>();

        foreach(Interaction i in _targetInteractionList)
        {
            if (!CheckIsValidReaction(i, r))
                continue;

            i.RemoveFromTargetInteractableList(r);
        }
    }

    bool CheckIsValidReaction(Interaction i, Reaction r)
    {
        if (r == null
            || r.InteractionType != i.InteractionType)
            return false;

        return true;
    }
}
