using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class AnimatorExtentions
{
    public static void OnComplete(this Animator animator, MonoBehaviour behaviour, Action callback)
    {
        behaviour.StartCoroutine(WaitForAnimComplete(animator, callback));
    }

    public static IEnumerator WaitForAnimComplete(this Animator animator, Action callback)
    {
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        int hash = stateInfo.fullPathHash;

        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
            yield return new WaitForEndOfFrame();

        if (callback != null)
            callback();
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