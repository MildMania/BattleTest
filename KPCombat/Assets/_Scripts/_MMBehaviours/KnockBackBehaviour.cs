using System.Collections;
using UnityEngine;
using DG.Tweening;
using System;
using System.Collections.Generic;

public class KnockBackBehaviour : MMBehaviour
{
    public Transform TargetTransform;

    public Vector2 KnockBackDirection;

    public float KnockBackGridCount { get; set; }

    public int KnockBackGridCountPerSec;

    public float DistanceFromObstacle;

    LayerMask _restrictingLayerMask;

    Coroutine _knockBackRoutine;
    Tween _knockBackTween;

    bool _isLayerMaskInited;

    public KnockBackBehaviour KnockBack()
    {
        StartKnockBackProgress();
        return this;
    }

    public void Stop()
    {
        StopKnockBackProgress();
    }

    void StartKnockBackProgress()
    {
        StopKnockBackProgress();

        _knockBackRoutine = StartCoroutine(KnockBackProgress());
    }

    void StopKnockBackProgress()
    {
        if (_knockBackRoutine != null)
            StopCoroutine(_knockBackRoutine);

        if (_knockBackTween != null)
            _knockBackTween.Kill();
    }

    IEnumerator KnockBackProgress()
    {
        Vector3 targetPos = GetKnockBackPosition();

        float duration = GetKnockBackDuration();

        _knockBackTween = TargetTransform.DOMove(targetPos, duration).SetEase(Ease.OutSine).SetUpdate(UpdateType.Fixed);

        yield return new WaitForSeconds(duration);

        StopKnockBackProgress();

        FireOnComplete();
    }

    Vector3 GetKnockBackPosition()
    {
        Vector3 newPos = TargetTransform.position;

        Vector3 rayDirection = KnockBackDirection.normalized;

        float distance = KnockBackGridCount;

        newPos += ((Vector3)(KnockBackDirection).normalized) * distance;

        return newPos;
    }

    float GetKnockBackDuration()
    {
        float duration = (float)(KnockBackGridCount) / (float)(KnockBackGridCountPerSec);

        return duration;
    }

}