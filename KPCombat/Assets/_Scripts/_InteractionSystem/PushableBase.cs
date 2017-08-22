using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableBase : Reaction
{
    public PushInteractionInfo CurPushInfo { get; private set; }

    protected override void SetReactionType()
    {
        InteractionType = InteractionType.Push;
    }

    public override void React(Interaction interaction, InteractionInfo interactionInfo)
    {
        base.React(interaction, interactionInfo);

        Pushed();
    }

    protected void Pushed()
    {
        CurPushInfo = (PushInteractionInfo)CurInteractionInfo;

        ReactionCompleted();
    }
}
