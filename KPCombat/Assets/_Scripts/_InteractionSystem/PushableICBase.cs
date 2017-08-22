using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PushableBase))]
public class PushableICBase : ReactionControllerBase
{
    protected override void InitTargetReaction()
    {
        _targetReaction = GetComponent<PushableBase>();
    }
}
