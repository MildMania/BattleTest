using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class AnimatorExtentions
{
    static Dictionary<Action, IEnumerator> _callbackDict;

    public static void OnComplete(this Animator animator, MonoBehaviour behaviour, Action callback)
    {
        if (_callbackDict == null)
            _callbackDict = new Dictionary<Action, IEnumerator>();

        if (_callbackDict.ContainsKey(callback))
        {
            behaviour.StopCoroutine(_callbackDict[callback]);
            _callbackDict.Remove(callback);
        }

        IEnumerator waitForAnimCompleteRoutine = WaitForAnimComplete(animator, callback);

        _callbackDict.Add(callback, waitForAnimCompleteRoutine);

        behaviour.StartCoroutine(waitForAnimCompleteRoutine);
    }

    public static IEnumerator WaitForAnimComplete(this Animator animator, Action callback)
    {
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();

        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
            yield return new WaitForEndOfFrame();

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (callback != null)
            callback();

        _callbackDict.Remove(callback);
    }

    public static bool ContainsParam(this Animator animator, string paramName)
    {
        foreach (AnimatorControllerParameter param in animator.parameters)
        {
            if (param.name == paramName)
                return true;
        }

        return false;
    }
}