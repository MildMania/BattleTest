using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public class CameraFrustum : MonoBehaviour
{
    static CameraFrustum _instance;

    public static CameraFrustum Instance { get { return _instance; } }

    public Camera Camera;

    Plane[] cameraPlanes;

    float _cameraOrthoSize;

    public Bounds CameraBounds{ get; private set; }

    public float VertExtent { get; private set; }

    public float HorzExtent { get ; private set; }

    IEnumerator _updateRoutine;

    void Awake()
    {
        _instance = this;

        StartUpdateProgress();
    }

    void OnDestroy()
    {
        _instance = null;
    }

    void StartUpdateProgress()
    {
        StopUpdateProgress();

        _updateRoutine = UpdateProgress();
        StartCoroutine(_updateRoutine);
    }

    void StopUpdateProgress()
    {
        if (_updateRoutine != null)
            StopCoroutine(_updateRoutine);
    }

    IEnumerator UpdateProgress()
    {
        while (true)
        {
            UpdateCameraFrustum();
            UpdateCameraBounds();

            yield return null;
        }
    }

    void UpdateCameraFrustum()
    {
        VertExtent = Camera.orthographicSize;    
        HorzExtent = VertExtent * Screen.width / Screen.height;
    }

    void UpdateCameraBounds()
    {
        CameraBounds = new Bounds(transform.position, new Vector3(HorzExtent * 2, VertExtent * 2, 0));
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        
        Vector3 bottomLeft = new Vector3(CameraBounds.min.x, CameraBounds.min.y);
        Vector3 bottomRight = new Vector3(CameraBounds.max.x, CameraBounds.min.y);
        Vector3 topRight = new Vector3(CameraBounds.max.x, CameraBounds.max.y);
        Vector3 topLeft = new Vector3(CameraBounds.min.x, CameraBounds.max.y);

        Gizmos.DrawLine(bottomLeft, bottomRight);
        Gizmos.DrawLine(bottomRight, topRight);
        Gizmos.DrawLine(topRight, topLeft);
        Gizmos.DrawLine(topLeft, bottomLeft);

    }

    public Vector2 GetCameraCenter()
    {
        return CameraBounds.center;
    }
}
