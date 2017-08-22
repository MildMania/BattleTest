using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public enum Directions
{
    Vertical,
    Horizontal
}

public enum SwipeDirection
{
    Right = 1,
    Left = 2,
    Up = 3,
    Down = 4

}

public enum ScreenArea
{
    None,
    Left,
    Right

}

public class GestureManager : MonoBehaviour
{
    private class GestureManagerTouch
    {
        public Vector2 StartPoint;
        public Vector2 PrevPoint;
        public Vector2 TouchPoint;
        public Vector2 DeltaMove;
        public bool IsTouchDown;
        public bool IsMoved;
        public bool IsUITouch;
        public Vector2 SwipeStartPoint;
        public SwipeDirection SwipeDirection;
        public bool IsSwipePossible;
        public float StartTime;
        public float LastMoveTime;
        public ScreenArea TouchStartScreenArea;
        public ScreenArea TouchContinueScreenArea;
    }

    public float TapDuration = 0.2f;
    public float TapWorldTreshold = 0.2f;
    float _tapPixelTreshold;
    public float PressDuration = 0.05f;
    public float DragWorldTreshold = 0.2f;
    float _dragPixelTreshold;

    public int MaxInputCount = 5;

    public float DragSwipeWorldTreshold = 0.3f;
    float _dragSwipePixelTreshold;

    public bool ReceiveUIInputs;

    public bool IsContinuousSwipeEnabled;

    private static GestureManager _instance;

    public static GestureManager Instance
    {
        get { return _instance; }
    }

    #region Gesture Variables

    private Dictionary<int, GestureManagerTouch> _inputDataDict = new Dictionary<int, GestureManagerTouch>();

    #endregion

    #region Events

    public static Action<int, Vector2> OnLeftTap;
    public static Action<int, Vector2> OnRightTap;
    public static Action<int, Vector2, float> OnPress;
    public static Action<int, Vector2> OnTap;
    public static Action<int, Vector2, float> OnLeftPress;
    public static Action<int, Vector2, float> OnRightPress;
    public static Action<int, Vector2> OnFingerDown;
    public static Action<int, Vector2> OnLeftFingerDown;
    public static Action<int, Vector2> OnRightFingerDown;
    public static Action<int, Vector2> OnLeftFingerUp;
    public static Action<int, Vector2> OnRightFingerUp;
    public static Action<int, Vector2> OnFingerUp;
    public static Action<int, Vector2> OnFingerUpOnStartPos;

    public static Action<int, Vector2, Vector2> OnDragBegin;
    public static Action<int, Vector2, Vector2> OnDragMove;
    public static Action<int, Vector2> OnDragEnd;

    public static Action<int, Vector2, SwipeDirection, float> OnSwipe;
    public static Action<int, Vector2, SwipeDirection, float> OnLeftSwipe;
    public static Action<int, Vector2, SwipeDirection, float> OnRightSwipe;

    public static Action<float> OnMouseScroll;


    private void FireOnTap(int fingerId, Vector2 fingerPos)
    {
        if (OnTap != null)
            OnTap(fingerId, fingerPos);
    }

    private void FireOnLeftTap(int fingerId, Vector2 fingerPos)
    {
        if (OnLeftTap != null)
            OnLeftTap(fingerId, fingerPos);
    }

    private void FireOnRightTap(int fingerId, Vector2 fingerPos)
    {
        if (OnRightTap != null)
            OnRightTap(fingerId, fingerPos);
    }

    private void FireOnPress(int fingerId, Vector2 fingerPos, float duration)
    {
        if (OnPress != null)
            OnPress(fingerId, fingerPos, duration);
    }

    private void FireOnLeftPress(int fingerId, Vector2 fingerPos, float duration)
    {
        if (OnLeftPress != null)
            OnLeftPress(fingerId, fingerPos, duration);
    }

    private void FireOnRightPress(int fingerId, Vector2 fingerPos, float duration)
    {
        if (OnRightPress != null)
            OnRightPress(fingerId, fingerPos, duration);
    }

    private void FireOnFingerDown(int fingerId, Vector2 fingerPos)
    {
        if (OnFingerDown != null)
            OnFingerDown(fingerId, fingerPos);
    }

    private void FireOnLeftFingerDown(int fingerId, Vector2 fingerPos)
    {
        if (OnLeftFingerDown != null)
            OnLeftFingerDown(fingerId, fingerPos);
    }

    private void FireOnRightFingerDown(int fingerId, Vector2 fingerPos)
    {
        if (OnRightFingerDown != null)
            OnRightFingerDown(fingerId, fingerPos);
    }

    private void FireOnFingerUp(int fingerId, Vector2 fingerPos)
    {
        if (OnFingerUp != null)
            OnFingerUp(fingerId, fingerPos);
    }

    private void FireOnLeftFingerUp(int fingerId, Vector2 fingerPos)
    {
        if (OnLeftFingerUp != null)
            OnLeftFingerUp(fingerId, fingerPos);
    }

    private void FireOnRightFingerUp(int fingerId, Vector2 fingerPos)
    {
        if (OnRightFingerUp != null)
            OnRightFingerUp(fingerId, fingerPos);
    }

    private void FireOnFingerUpOnStartPos(int fingerId, Vector2 fingerPos)
    {
        if (OnFingerUpOnStartPos != null)
            OnFingerUpOnStartPos(fingerId, fingerPos);
    }

    private void FireOnDragBegin(int fingerId, Vector2 fingerPos, Vector2 startPos)
    {
        if (OnDragBegin != null)
            OnDragBegin(fingerId, fingerPos, startPos);
    }

    private void FireOnDragMove(int fingerId, Vector2 fingerPos, Vector2 delta)
    {
        if (OnDragMove != null)
            OnDragMove(fingerId, fingerPos, delta);
    }

    private void FireOnDragEnd(int fingerId, Vector2 fingerPos)
    {
        if (OnDragEnd != null)
            OnDragEnd(fingerId, fingerPos);
    }

    private void FireOnDragSwipe(int fingerId, Vector2 startPos, SwipeDirection direction, float velocity)
    {
        if (OnSwipe != null)
            OnSwipe(fingerId, startPos, direction, velocity);
    }

    private void FireOnSwipe(int fingerId, Vector2 startPos, SwipeDirection direction, float velocity)
    {
        if (OnSwipe != null)
            OnSwipe(fingerId, startPos, direction, velocity);
    }

    private void FireOnLeftSwipe(int fingerId, Vector2 startPos, SwipeDirection direction, float velocity)
    {
        if (OnLeftSwipe != null)
            OnLeftSwipe(fingerId, startPos, direction, velocity);
    }

    private void FireOnRightSwipe(int fingerId, Vector2 startPos, SwipeDirection direction, float velocity)
    {
        if (OnRightSwipe != null)
            OnRightSwipe(fingerId, startPos, direction, velocity);
    }

    private void FireOnMouseScroll(float delta)
    {
        if (OnMouseScroll != null)
            OnMouseScroll(delta);
    }

    #endregion

    private Camera _uiCamera;
    private Vector3 _touchPointInUi;

    private void Awake()
    {
        _instance = this;

        SceneManager.sceneLoaded += OnSceneLoaded;

        InitDistanceTresholds();

        InitInputDataDict();
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode loadMode)
    {
        InitDistanceTresholds();
    }

    void InitDistanceTresholds()
    {
        if (Camera.main == null)
            return;
        
        float worldToPixels = ((Screen.height / 2.0f) / Camera.main.orthographicSize);
        
        _tapPixelTreshold = worldToPixels * TapWorldTreshold;
        _dragPixelTreshold = worldToPixels * DragWorldTreshold;
        _dragSwipePixelTreshold = worldToPixels * DragSwipeWorldTreshold;

        //Debug.Log("tap pixel: " + _tapPixelTreshold);
        //Debug.Log("drag pixel: " + _dragPixelTreshold);
        //Debug.Log("swipe pixel: " + _dragSwipePixelTreshold);

    }

    void InitInputDataDict()
    {
        _inputDataDict = new Dictionary<int, GestureManagerTouch>();

        for (int i = 0; i < MaxInputCount; i++)
            _inputDataDict.Add(i, new GestureManagerTouch());
    }

    void Update()
    {
        ProcessInputDatas();
    }

    void ProcessInputDatas()
    {
        for (int i = 0; i < _inputDataDict.Count; i++)
            ProcessInputData(_inputDataDict[i], i);
    }

    void ProcessInputData(GestureManagerTouch touch, int index)
    {
        Vector3 inputPos = Vector3.zero;
        
        if (Utilities.IsTouchPlatform())
            ProcessTouchInput(touch, index);
        else
            ProcessMouseInput(touch, index);

        touch.PrevPoint = touch.TouchPoint;
    }

    void ProcessMouseInput(GestureManagerTouch touch, int index)
    {
        if (EventSystem.current != null
            && EventSystem.current.IsPointerOverGameObject()
            && !ReceiveUIInputs)
            return;

        Vector3 inputPos = Input.mousePosition;

        touch.TouchPoint = inputPos;

        if (!touch.IsTouchDown)
        {
            if (Input.GetMouseButtonDown(index))
                InputStarted(touch, index);
        }
        else
        {
            if (Input.GetMouseButton(index))
                InputContinued(touch, index);
            else if (Input.GetMouseButtonUp(index))
                InputEnded(touch, index);
        }

        float scrollAmount = Input.GetAxis("Mouse ScrollWheel");

        if (Mathf.Abs(scrollAmount) > 0)
            FireOnMouseScroll(scrollAmount);

    }

    void ProcessTouchInput(GestureManagerTouch touch, int index)
    {
        if (index >= Input.touchCount)
            return;

        Touch unityTouch = Input.GetTouch(index);

        Vector3 inputPos = (Vector3)unityTouch.position;

        touch.TouchPoint = inputPos;

        if (!touch.IsTouchDown)
        {
            if (unityTouch.phase == TouchPhase.Began && !EventSystem.current.IsPointerOverGameObject(unityTouch.fingerId))
                InputStarted(touch, index);
        }
        else
        {
            if (unityTouch.phase == TouchPhase.Moved
                || unityTouch.phase == TouchPhase.Stationary)
                InputContinued(touch, index);
            else if (unityTouch.phase == TouchPhase.Ended)
                InputEnded(touch, index);
        }
    }

    void InputStarted(GestureManagerTouch touch, int index)
    {
        touch.IsTouchDown = true;
        touch.StartPoint = touch.TouchPoint;
        touch.StartTime = Time.time;
        touch.PrevPoint = touch.TouchPoint;
        touch.IsSwipePossible = true;
        touch.SwipeStartPoint = touch.TouchPoint;
        touch.IsMoved = false;
        touch.IsUITouch = false;

        touch.TouchStartScreenArea = touch.StartPoint.x <= Screen.width / 2.0f ? ScreenArea.Left : ScreenArea.Right;

        FireOnFingerDown(index, touch.TouchPoint);

        switch (touch.TouchStartScreenArea)
        {
            case ScreenArea.Left:
                FireOnLeftFingerDown(index, touch.TouchPoint);
                break;
            case ScreenArea.Right:
                FireOnRightFingerDown(index, touch.TouchPoint);
                break;
        }
    }

    void InputContinued(GestureManagerTouch touch, int index)
    {
        touch.DeltaMove = touch.TouchPoint - touch.PrevPoint;
        touch.TouchContinueScreenArea = touch.TouchPoint.x <= Screen.width / 2f ? ScreenArea.Left : ScreenArea.Right;

        if (!touch.IsMoved
            && touch.DeltaMove.sqrMagnitude >= _dragPixelTreshold)
            DragBegan(touch, index);
        else if (touch.IsMoved
                 && touch.DeltaMove.sqrMagnitude >= _dragPixelTreshold)
            DragContinued(touch, index);
        else if (touch.IsMoved)
            DragEnded(touch, index);

        if (!touch.IsMoved)
            CheckPressed(touch, index);
    }

    void InputEnded(GestureManagerTouch touch, int index)
    {
        if (touch.IsMoved)
            DragEnded(touch, index);
        else
        {
            CheckTapped(touch, index);
            CheckFingerUpOnStartPos(touch, index);
        }

        FireOnFingerUp(index, touch.TouchPoint);

        switch (touch.TouchStartScreenArea)
        {
            case ScreenArea.Left:
                FireOnLeftFingerUp(index, touch.TouchPoint);
                break;
            case ScreenArea.Right:
                FireOnRightFingerUp(index, touch.TouchPoint);
                break;
        }

        touch.IsTouchDown = false;
    }

    void DragBegan(GestureManagerTouch touch, int index)
    {
        touch.IsMoved = true;
        touch.LastMoveTime = Time.time;

        if (touch.IsSwipePossible
            || IsContinuousSwipeEnabled)
            PossibleSwipeBegan(touch, index);

        FireOnDragBegin(index, touch.TouchPoint, touch.StartPoint);
    }

    void DragContinued(GestureManagerTouch touch, int index)
    {
        CheckSwipe(touch, index);

        touch.LastMoveTime = Time.time;

        FireOnDragMove(index, touch.TouchPoint, touch.DeltaMove);
    }

    void DragEnded(GestureManagerTouch touch, int index)
    {
        touch.IsMoved = false;

        if (IsContinuousSwipeEnabled)
            touch.IsSwipePossible = true;

        FireOnDragEnd(index, touch.TouchPoint);
    }

    void PossibleSwipeBegan(GestureManagerTouch touch, int index)
    {
        touch.IsSwipePossible = true;
        
        touch.SwipeStartPoint = touch.TouchPoint;

        if (Mathf.Abs(touch.DeltaMove.x) > Mathf.Abs(touch.DeltaMove.y))
            touch.SwipeDirection = touch.DeltaMove.x > 0 ? SwipeDirection.Right : SwipeDirection.Left;
        else
            touch.SwipeDirection = touch.DeltaMove.y > 0 ? SwipeDirection.Up : SwipeDirection.Down;
    }

    void CheckSwipe(GestureManagerTouch touch, int index)
    {
        if (touch.IsSwipePossible)
        {
            if (touch.SwipeDirection == SwipeDirection.Right && touch.DeltaMove.x < 0
                || touch.SwipeDirection == SwipeDirection.Left && touch.DeltaMove.x > 0
                || touch.SwipeDirection == SwipeDirection.Up && touch.DeltaMove.y < 0
                || touch.SwipeDirection == SwipeDirection.Down && touch.DeltaMove.y > 0)
            {
                if (IsContinuousSwipeEnabled)
                    PossibleSwipeBegan(touch, index);
                else
                    touch.IsSwipePossible = false;
                
                return;
            }

            if (Vector2.Distance(touch.SwipeStartPoint, touch.TouchPoint) > _dragSwipePixelTreshold)
            {
                float swipeVelocity = Vector2.Distance(touch.TouchPoint, touch.SwipeStartPoint) / (Time.time - touch.StartTime);

                FireOnSwipe(index, touch.StartPoint, touch.SwipeDirection, swipeVelocity);

                touch.IsSwipePossible = false;

                switch (touch.TouchStartScreenArea)
                {
                    case ScreenArea.Left:
                        FireOnLeftSwipe(index, touch.StartPoint, touch.SwipeDirection, swipeVelocity);
                        break;
                    case ScreenArea.Right:
                        FireOnRightSwipe(index, touch.StartPoint, touch.SwipeDirection, swipeVelocity);
                        break;
                }
            }
        }
    }

    void CheckFingerUpOnStartPos(GestureManagerTouch touch, int index)
    {
        if (Vector2.Distance(touch.StartPoint, touch.TouchPoint) > _tapPixelTreshold)
            return;

        FireOnFingerUpOnStartPos(index, touch.TouchPoint);
    }

    void CheckTapped(GestureManagerTouch touch, int index)
    {
        if (Time.time - touch.StartTime > TapDuration
            || Vector2.Distance(touch.StartPoint, touch.TouchPoint) > _tapPixelTreshold)
            return;

        FireOnTap(index, touch.TouchPoint);

        switch (touch.TouchStartScreenArea)
        {
            case ScreenArea.Left:
                FireOnLeftTap(index, touch.TouchPoint);
                break;
            case ScreenArea.Right:
                FireOnRightTap(index, touch.TouchPoint);
                break;
        }
    }

    void CheckPressed(GestureManagerTouch touch, int index)
    {
        float duration = Time.time - touch.StartTime;

        FireOnPress(index, touch.TouchPoint, duration);

        switch (touch.TouchStartScreenArea)
        {
            case ScreenArea.Left:
                FireOnLeftPress(index, touch.TouchPoint, duration);
                break;
            case ScreenArea.Right:
                FireOnRightPress(index, touch.TouchPoint, duration);
                break;
        }
            
    }
}
