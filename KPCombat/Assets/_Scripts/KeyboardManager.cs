using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public enum KeyState
{
    None,
    Down,
    Pressed,
    Up,
}

public class KeyboardManager : MonoBehaviour
{
    class InputInfo
    {
        public KeyCode Key;
        public KeyState State;
        public List<Action> CallbackList;

        public InputInfo(KeyCode key, KeyState state, Action callback)
        {
            Key = key;
            State = state;
            CallbackList = new List<Action>();
            CallbackList.Add(callback);
        }
    }

    static KeyboardManager _instance;

    public static KeyboardManager TreeInstance { get { return _instance; } }

    static List<InputInfo> _inputInfoList;

    void Awake()
    {
        _instance = this;

        InitInputInfoList();
    }

    void OnDestroy()
    {
        _instance = null;
    }

    void InitInputInfoList()
    {
        _inputInfoList = new List<InputInfo>();
    }

    void Update()
    {
        CheckInput();
    }

    void CheckInput()
    {
        List<InputInfo> inputInfoList = _inputInfoList.ToList();

        foreach (InputInfo inputInfo in inputInfoList)
        {
            switch (inputInfo.State)
            {
                case KeyState.Down:
                    if (Input.GetKeyDown(inputInfo.Key))
                        InvokeInputInfoCallbacks(inputInfo);
                    break;
                case KeyState.Pressed:
                    if (Input.GetKey(inputInfo.Key))
                        InvokeInputInfoCallbacks(inputInfo);
                    break;
                case KeyState.Up:
                    if (Input.GetKeyUp(inputInfo.Key))
                        InvokeInputInfoCallbacks(inputInfo);
                    break;
            }
        }
    }

    static void InvokeInputInfoCallbacks(InputInfo inputInfo)
    {
        List<Action> callbackList = inputInfo.CallbackList.ToList();

        foreach (Action action in callbackList)
            action.Invoke();
    }

    public static void AddListener(KeyCode key, KeyState state, Action callback)
    {
        InputInfo inputInfo = GetInfoByKeyAndState(key, state);

        if (inputInfo != null)
        {
            inputInfo.CallbackList.Add(callback);
            return;
        }

        _inputInfoList.Add(new InputInfo(key, state, callback));
    }

    public static void RemoveListener(KeyCode key, KeyState state, Action callback)
    {
        InputInfo inputInfo = GetInfoByKeyAndState(key, state);

        if (inputInfo == null)
            return;

        inputInfo.CallbackList.Remove(callback);

        if (inputInfo.CallbackList.Count == 0)
            _inputInfoList.Remove(inputInfo);
    }

    static InputInfo GetInfoByKeyAndState(KeyCode key, KeyState state)
    {
        try
        {
            return _inputInfoList.Single(val => val.Key == key && val.State == state);
        }
        catch
        {
            return null;
        }
    }
}