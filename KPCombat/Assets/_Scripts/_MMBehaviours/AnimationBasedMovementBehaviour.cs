using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class AnimationBasedMovementBehaviour : MMBehaviour
{
    public Transform TargetTransform;

    public AnimationCurve AnimationCurve;
    public Vector2 MovementDistance;
    public float MovementDuration;

    Tween _moveTween;

    public AnimationBasedMovementBehaviour Move()
    {
        if (_moveTween != null)
            _moveTween.Kill();

        _moveTween = TargetTransform.DOMove(GetTargetPos(), MovementDuration).SetEase(AnimationCurve).OnComplete(OnTweenCompleted);

        return this;
    }

    public void Stop()
    {
        if (_moveTween != null)
            _moveTween.Kill();
    }

    void OnTweenCompleted()
    {
        FireOnComplete();
    }

    Vector3 GetTargetPos()
    {
        Vector3 newPos = TargetTransform.position;

        newPos += (Vector3)MovementDistance;

        return newPos;
    }
}
