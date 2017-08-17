using UnityEngine;
using System.Collections;

public class CameraFollowScript : MMGameSceneBehaviour
{
    public static CameraFollowScript Instance { get; private set; }

    public Transform Character;

    public Transform CameraCarrier;

    public Camera Camera;

    public float MaxCameraSpeed;

    public float DefaultFollowYOffset;

    public float DampTime;

    Transform _targetTransform;

    Vector3 _followVel;
    bool _canFollow;

    protected override void Awake()
    {
        Instance = this;

        base.Awake();
    }

    protected override void OnDestroy()
    {
        Instance = null;

        base.OnDestroy();
    }

    protected override void OnGameStarted()
    {
        SetTargetTransform(Character);

        StartFollowing();
    }

    protected override void OnGameEnded()
    {
        StopFollowing();
    }

    public void SetTargetTransform(Transform targetTransform)
    {
        _targetTransform = targetTransform;
    }

    void StartFollowing()
    {
        _canFollow = true;

        _followVel = Vector3.zero;
    }

    void StopFollowing()
    {
        _canFollow = false;
    }

    void FixedUpdate()
    {
        FollowStep();
    }

    void FollowStep()
    {
        if (!_canFollow)
            return;

        Vector3 targetPos = CameraCarrier.position;

        targetPos.y = _targetTransform.position.y;
        targetPos.y += DefaultFollowYOffset;

        CameraCarrier.position = Vector3.SmoothDamp(CameraCarrier.position, targetPos, ref _followVel, DampTime, MaxCameraSpeed);
    }

    void SnapToTransform(Transform targetTransform)
    {
        Vector3 targetPos = CameraCarrier.position;

        targetPos.y = targetTransform.position.y;
        targetPos.y += DefaultFollowYOffset;

        CameraCarrier.position = targetPos;
    }
}
