using UnityEngine;
using System;

public class AnimationBehaviour : MMBehaviour
{
    #region Events
    protected Action<float> _onUpdate;

    void FireOnUpdate(float remainingTime)
    {
        if (_onUpdate != null)
            _onUpdate(remainingTime);
    }
    #endregion

    public MMSpriteAnimatorBase SpriteAnimator;

    int _animationEnum;
    bool _isLoopAnim;

    public AnimationBehaviour PlayAnimation(int animationEnum, bool isLoopAnim = false)
    {
        _animationEnum = animationEnum;
        _isLoopAnim = isLoopAnim;

        return PlayAnimation();
    }

    public AnimationBehaviour PlayAnimation()
    {
        ResetOnCompleteEvents();
        ResetOnUpdateEvents();

        if (!_isLoopAnim)
            SpriteAnimator.PlayAnimation(_animationEnum).OnUpdate(OnAnimationUpdate).OnComplete(OnAnimationCompleted);
        else
        {
            SpriteAnimator.PlayAnimation(_animationEnum);
        }

        return this;
    }

    public void Stop()
    {
        SpriteAnimator.StopAnimation();
    }

    public MMBehaviour OnUpdate(Action<float> callback)
    {
        _onUpdate += callback;

        return this;
    }

    public void UnRegisterOnUpdate(Action<float> callback)
    {
        _onUpdate -= callback;
    }

    void OnAnimationCompleted()
    {
        FireOnComplete();

        ResetOnUpdateEvents();
    }

    void OnAnimationUpdate(float remainingTime)
    {
        FireOnUpdate(remainingTime);
    }

    void ResetOnUpdateEvents()
    {
        if (_onUpdate == null)
            return;
        
        foreach (Action<float> action in _onUpdate.GetInvocationList())
        {
            _onUpdate -= action;
        }
    }


}
