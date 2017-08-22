using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PusherBase))]
public class PusherICBase : InteractionControllerBase
{
    public Vector2 PushDirection;
    public float PushAmount;
    public float PushSpeed;

    protected override void InitTargetInteraction()
    {
        _targetInteraction = GetComponent<PusherBase>();
    }

    protected override void OnPreInteract(Reaction target)
    {
        base.OnPreInteract(target);

        PusherBase pusher = (PusherBase)_targetInteraction;

        pusher.SetPushParameters(PushDirection, PushAmount, PushSpeed);

    }
}
