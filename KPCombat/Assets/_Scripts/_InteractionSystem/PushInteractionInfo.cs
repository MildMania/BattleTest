using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushInteractionInfo : InteractionInfo
{
    public Vector2 PushDirection { get; private set; }
    public float PushAmount { get; private set; }
    public float PushSpeed { get; private set; }

    public PushInteractionInfo(Vector2 pushDirection, float pushAmount, float pushSpeed)
    {
        PushDirection = pushDirection;
        PushAmount = pushAmount;
        PushSpeed = pushSpeed;
    }
}
