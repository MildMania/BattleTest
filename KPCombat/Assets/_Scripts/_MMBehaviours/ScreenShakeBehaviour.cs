using DG.Tweening;
using UnityEngine;
using System;

public class ScreenShakeBehaviour : MonoBehaviour
{
    public float ShakeAmount;
    public float ShakeDuration;
    public int ShakeVibration;
    public Ease EaseType;

    #region Events

    public Action OnComplete;

    void FireOnComplete()
    {
        if (OnComplete != null)
            OnComplete();
    }

    #endregion

    public void Shake()
    {
        CameraShakeScript.Instance.ShakeCamera(ShakeDuration, ShakeAmount, ShakeVibration, EaseType, OnShakeComplete);
    }

    void OnShakeComplete()
    {
        FireOnComplete();
    }
}
