using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunBehaviour : MoveBehaviourBase
{
    public Transform TargetTransform;
    public Vector2 HeadDirection;

    public float RunSpeed;

    #region implemented abstract members of MoveBehaviourBase

    protected override void MoveStep()
    {
        if (TargetTransform == null)
            return;

        Vector3 newPos = TargetTransform.position;
        newPos += RunSpeed * (Vector3)HeadDirection * Time.fixedDeltaTime * Time.timeScale;

        TargetTransform.position = newPos;
    }

    #endregion


}