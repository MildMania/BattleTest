using System.Collections;
using UnityEngine;

public class MoveBehaviourBase : MonoBehaviour
{
    bool _canRun;

    IEnumerator _moveRoutine;

    public void StartMovement()
    {
        StopMovement();

        _moveRoutine = MoveProgress();
        StartCoroutine(_moveRoutine);
    }

    public void Stop()
    {
        StopMovement();
    }

    void StopMovement()
    {
        if (_moveRoutine != null)
            StopCoroutine(_moveRoutine);
    }

    protected virtual IEnumerator MoveProgress()
    {
        while (true)
        {
            MoveStep();

            yield return new WaitForFixedUpdate();
        }
    }

    protected virtual void MoveStep()
    {
    }
}