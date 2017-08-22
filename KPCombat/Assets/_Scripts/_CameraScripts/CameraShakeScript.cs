using UnityEngine;
using System.Collections;
using DG.Tweening;
using System;

public class CameraShakeScript : MonoBehaviour
{
    static CameraShakeScript _instance;

    public static CameraShakeScript Instance { get { return _instance; } }

    public float DefautShakeAmount;
    public float DefaultShakeDuration;
    public int DefaultVibration;
    public Ease DefaultEaseType;

    float _curShakeAmount;
    float _curShakeDuration;
    int _curVibration;
    Ease _curEase;

    Transform _cameraCarrier;


    Tween _shakeTween;
    Action _curCallback;

    void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        InitCameraCarrier();
    }

    void InitCameraCarrier()
    {
        _cameraCarrier = transform.parent;
    }

    public void ShakeCamera(float duration = 0, float amount = 0, int vibration = 0, Ease easeType = Ease.Linear, Action callback = null)
    {
        _curShakeAmount = amount;
        _curShakeDuration = duration;
        _curVibration = vibration;
        _curEase = easeType;

        if (_curShakeAmount == 0)
            _curShakeAmount = DefautShakeAmount;

        if (_curShakeDuration == 0)
            _curShakeDuration = DefaultShakeDuration;

        if (_curVibration == 0)
            _curVibration = DefaultVibration;

        float shake = _curShakeDuration;
        float shakeAmount = _curShakeAmount;

        _curCallback = callback;

        _shakeTween = _cameraCarrier.DOShakePosition(_curShakeDuration, shakeAmount, _curVibration).SetEase(_curEase).OnComplete(OnShakeCompleted);
    }

    public void StopShaking()
    {
        if (_shakeTween != null)
            _shakeTween.Kill();

        _cameraCarrier.localPosition = Vector3.zero;
    }

    void OnShakeCompleted()
    {
        _cameraCarrier.localPosition = Vector3.zero;

        if (_curCallback != null)
            _curCallback.Invoke();
    }
}
