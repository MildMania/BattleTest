using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionEventController : MonoBehaviour
{
    public List<Interaction> InteractionList;

    public bool IsDebugEnabled;

    public void ActivateInteraction(int interactionID)
    {
        if (IsDebugEnabled)
            Debug.Log("attack started");

        InteractionList[interactionID].IsInteractionActive = true;
    }

    public void DeactivateInteraction(int interactionID)
    {
        if (IsDebugEnabled)
            Debug.Log("attack finished");


        InteractionList[interactionID].IsInteractionActive = false;
    }
}
