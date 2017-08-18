using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class MMBasicAnimController : MMGameSceneBehaviour
{
    Animator _animator;

    protected override void Awake()
    {
        base.Awake();
        
        InitAnimator();

        SetAnimatorActive(true);
        ResetAnimator();
    }

    void InitAnimator()
    {
        _animator = GetComponent<Animator>();
    }

    void ResetAnimator()
    {
        _animator.Play("IDLE");
    }

    void SetAnimatorActive(bool isActive)
    {
        _animator.enabled = isActive;
    }

    public MMBasicAnimController PlayAnimation(string stateName)
    {
        _animator.Play(stateName);

        return this;
    }

    public void OnComplete(Action callback)
    {
        _animator.OnComplete(this, callback);
    }

    public IEnumerator WaitForAnimComplete()
    {
        yield return StartCoroutine(_animator.WaitForAnimComplete(null));
    }
}
