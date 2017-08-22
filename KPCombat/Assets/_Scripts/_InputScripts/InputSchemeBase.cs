using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSchemeBase
{
    public virtual void StartListeningInputEvents()
    {
        GestureManager.OnFingerDown += OnFingerDown;
        GestureManager.OnSwipe += OnSwipe;
        GestureManager.OnFingerUp += OnFingerUp;
        GestureManager.OnFingerUpOnStartPos += OnFingerUpOnStartPos;
        GestureManager.OnTap += OnTap;
        GestureManager.OnPress += OnPress;
    }

    public virtual void FinishListeningInputEvents()
    {
        GestureManager.OnFingerDown -= OnFingerDown;
        GestureManager.OnSwipe -= OnSwipe;
        GestureManager.OnFingerUp -= OnFingerUp;
        GestureManager.OnFingerUpOnStartPos -= OnFingerUpOnStartPos;
        GestureManager.OnTap -= OnTap;
        GestureManager.OnPress -= OnPress;

    }


    protected virtual void OnFingerDown(int fingerID, Vector2 inputPos)
    {

    }

    protected virtual void OnFingerUp(int fingerID, Vector2 inputPos)
    {

    }

    protected virtual void OnFingerUpOnStartPos(int fingerID, Vector2 inputPos)
    {

    }

    protected virtual void OnTap(int fingerID, Vector2 inputPos)
    {

    }

    protected virtual void OnPress(int fingerID, Vector2 inputPos, float duration)
    {

    }

    protected virtual void OnSwipe(int fingerId, Vector2 startPos, SwipeDirection direction, float velocity)
    {
        switch (direction)
        {
            case SwipeDirection.Left:
                OnLeftSwipe();
                break;
            case SwipeDirection.Right:
                OnRightSwipe();
                break;
            case SwipeDirection.Up:
                OnUpSwipe();
                break;
            case SwipeDirection.Down:
                OnDownSwipe();
                break;
        }
    }

    protected virtual void OnLeftSwipe()
    {
        
    }

    protected virtual void OnRightSwipe()
    {
        
    }

    protected virtual void OnUpSwipe()
    {
        
    }

    protected virtual void OnDownSwipe()
    {
        
    }
}
