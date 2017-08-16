using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float MovementSpeed;

    IEnumerator _moveRoutine;

    bool _isMoving;

    public void Move()
    {
        if (_isMoving)
            return;

        _moveRoutine = MoveProgress();
        StartCoroutine(_moveRoutine);
    }

    public void Stop()
    {
        if (_moveRoutine != null)
            StopCoroutine(_moveRoutine);

        _isMoving = false;
    }

    IEnumerator MoveProgress()
    {
        _isMoving = true;

        while(true)
        {
            MoveStep();
            yield return null;
        }
    }

    void MoveStep()
    {
        Vector3 newPos = transform.position;

        newPos.y += MovementSpeed * Time.deltaTime;

        transform.position = newPos;
    }
}
