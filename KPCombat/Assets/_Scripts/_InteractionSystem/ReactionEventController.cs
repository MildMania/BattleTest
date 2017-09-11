using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionEventController : MonoBehaviour
{
    public List<Reaction> ReactionList;

    public bool IsDebugEnabled;

    public void ActivateReaction(int interactionID)
    {
        if (IsDebugEnabled)
            Debug.Log("attack started");

        ReactionList[interactionID].IsReactionActive = true;
    }

    public void DeactivateReaction(int interactionID)
    {
        if (IsDebugEnabled)
            Debug.Log("attack finished");


        ReactionList[interactionID].IsReactionActive = false;
    }
}
