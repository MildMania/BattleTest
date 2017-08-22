using System;
using UnityEngine;

[RequireComponent(typeof(InteractionCollider))]
public class PusherBase : Interaction
{
    public Vector2 PushDirection { get; private set; }
    public float PushAmount { get; private set; }
    public float PushSpeed { get; private set; }

    protected override void SetInteractionType()
    {
        InteractionType = InteractionType.Push;
    }

    public void SetPushParameters(Vector2 pushDir, float pushAmount, float pushSpeed)
    {
        PushDirection = pushDir;
        PushAmount = pushAmount;
        PushSpeed = pushSpeed;
    }

    protected override InteractionInfo GetInteractionInfo(object message)
    {
        PushInteractionInfo pi = new PushInteractionInfo(PushDirection, PushAmount, PushSpeed);

        return pi;
    }
}
