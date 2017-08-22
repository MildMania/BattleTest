using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public abstract class MMSpriteAnimatorBase : MMGameSceneBehaviour
{
    public Animator ReferenceAnimator;

    protected Animator _curReferenceAnimator;

    bool _isUpdateRoutineRunning;

    public bool IsDebugEnabled;

    const string IDLE_ANIM_NAME = "IDLE";

	#region Events
	protected Action<float> _onUpdate;

    void FireOnUpdate(float remainingTime)
    {
        if (_onUpdate != null)
            _onUpdate(remainingTime);
    }

    protected Action _onComplete;

    void FireOnComplete()
    {
        if (_onComplete != null)
            _onComplete();

        ResetOnCompleteEvent();
    }

    #endregion

    protected override void Awake()
    {
        InitAnimQueue();

        SetAnimatorsActive(true);

        ResetAnimators();

        base.Awake();

    }

    protected virtual void ResetAnimators()
    {
        ReferenceAnimator.Play(IDLE_ANIM_NAME);
    }

    protected virtual void SetAnimatorsActive(bool isActive)
    {
        ReferenceAnimator.enabled = isActive;
    }

    void InitAnimQueue()
    {
        _curReferenceAnimator = ReferenceAnimator;
    }

    public virtual MMSpriteAnimatorBase PlayAnimation(int animationEnum)
    {
        _curReferenceAnimator.OnComplete(this, OnAnimationCompleted);

        ResetOnCompleteEvent();

        PlayAnimation(ReferenceAnimator, GetAnimStateName(animationEnum));

        return this;
    }

    public virtual void StopAnimation()
    {
        ReferenceAnimator.StopPlayback();
    }

    public MMSpriteAnimatorBase OnComplete(Action callback)
    {
        _onComplete += callback;

        return this;
    }

    public void UnregisterFromOnComplete(Action callback)
    {
        _onComplete -= callback;
    }

    void OnAnimationCompleted()
    {
        FireOnComplete();
    }

    public MMSpriteAnimatorBase OnUpdate(Action<float> callback)
    {
        _onUpdate += callback;

        if (!_isUpdateRoutineRunning)
            StartCoroutine(UpdateProgress());

        return this;
    }

    IEnumerator UpdateProgress()
    {
        _isUpdateRoutineRunning = true;

        float normalizedTime;
        float remainingTime;

        do
        {
            AnimatorStateInfo stateInfo = _curReferenceAnimator.GetCurrentAnimatorStateInfo(0);

            normalizedTime = stateInfo.normalizedTime;

            remainingTime = (1.0f - normalizedTime) * stateInfo.length;

			FireOnUpdate(remainingTime);

            yield return null;

        } while (normalizedTime < 1);

        ResetOnUpdateEvent();

        _isUpdateRoutineRunning = false;
    }

    void ResetOnUpdateEvent()
    {
        if (_onUpdate == null)
            return;

		foreach (Action<float> action in _onUpdate.GetInvocationList())
		{
			_onUpdate -= action;
		}
    }

    void ResetOnCompleteEvent()
    {
        if (_onComplete == null)
            return;

        foreach (Action action in _onComplete.GetInvocationList())
        {
            _onComplete -= action;
        }
    }


    protected MMSpriteAnimatorBase PlayAnimation(Animator animator, string name)
    {
        animator.enabled = false;
        animator.enabled = true;
        animator.Play(name);

        return this;
    }

    protected abstract string GetAnimStateName(int animEnum);
}
