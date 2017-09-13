using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public enum FSMType
{
    None,
    Movement,
    Battle,
}

public class TransitionTrigger
{
    public FSMStateID StateID;
    public int TriggerTime;

    public TransitionTrigger(FSMStateID stateID, int triggerTime)
    {
        StateID = stateID;
        TriggerTime = triggerTime;
    }
}

public class MMFSM : MonoBehaviour
{
    public FSMType FSMType;

    public Animator FSMAnimator;

    public FSMStateID CurStateID { get; private set; }

    public FSMStateID PrevStateID { get; private set; }

    public bool IsDebugActive;

    List<FSMStateBehaviour> _fsmStateBehaviourList;

    List<FSMBehaviourController> _fsmBehaviourControllerList;

    FSMStateID _transitionID;

    List<TransitionTrigger> _triggerList;

    IEnumerator _resetTriggerRoutine;

    float _lastTriggerFrame;

    #region Events

    public event Action<FSMStateID> OnStateChanged;

    void FireOnStateChanged()
    {
#if UNITY_EDITOR
        if (IsDebugActive)
            Debug.Log("fsm type: " + FSMType + " new state: <color=magenta>" + CurStateID + "</color>");
#endif

        if (OnStateChanged != null)
            OnStateChanged(CurStateID);
    }

    #endregion

    void Awake()
    {
        InitFSM();

        StartListeningEvents();

        StartResetTriggerProgress();
    }

    void OnDestroy()
    {
        FinishListeningEvents();
    }

    void InitFSM()
    {
        _triggerList = new List<TransitionTrigger>();

        _fsmStateBehaviourList = FSMAnimator.GetBehaviours<FSMStateBehaviour>().ToList();
        _fsmBehaviourControllerList = GetComponentsInChildren<FSMBehaviourController>().ToList();
    }

    void StartListeningEvents()
    {
        _fsmStateBehaviourList.ForEach(val => val.OnStateEntered += OnStateEntered);
        _fsmStateBehaviourList.ForEach(val => val.OnStateExited += OnStateExited);
    }

    void FinishListeningEvents()
    {
        _fsmStateBehaviourList.ForEach(val => val.OnStateEntered -= OnStateEntered);
        _fsmStateBehaviourList.ForEach(val => val.OnStateExited -= OnStateExited);
    }

    public void SetFSMActive(bool isActive)
    {
        FSMAnimator.enabled = isActive;
    }

    public FSMBehaviourController GetBehaviourControllerOfState(FSMStateID stateID)
    {
        try
        {
            return _fsmBehaviourControllerList.Single(val => val.StateID == stateID);
        }
        catch
        {
            return null;
        }
    }

    public void SetParameter(string parameterName, object parameterValue)
    {
        TypeCode tc = Type.GetTypeCode(parameterValue.GetType());

        switch (tc)
        {
            case TypeCode.Int32:
                FSMAnimator.SetInteger(parameterName, (int)parameterValue);
                break;
            case TypeCode.Single:
                FSMAnimator.SetFloat(parameterName, (float)parameterValue);
                break;
            case TypeCode.Boolean:
                FSMAnimator.SetBool(parameterName, (bool)parameterValue);
                break;
        }
    }

    public int GetIntegerParameter(string parameterName)
    {
        return FSMAnimator.GetInteger(parameterName);
    }

    public float GetFloatParameter(string parameterName)
    {
        return FSMAnimator.GetFloat(parameterName);
    }

    public bool GetBoolParameter(string parameterName)
    {
        return FSMAnimator.GetBool(parameterName);
    }

    public void SetState(FSMStateID stateID)
    {
        FSMAnimator.Play(stateID.ToString());
    }

    public void SetTransition(FSMStateID stateID)
    {
        _transitionID = stateID;

        _triggerList.Add(new TransitionTrigger(stateID, Time.frameCount));

        TriggerTransition();
    }

    void TriggerTransition()
    {
        if (!FSMAnimator.ContainsParam(_transitionID.ToString()))
        {
            _lastTriggerFrame = Time.frameCount;

            return;
        }

        FSMAnimator.SetTrigger(_transitionID.ToString());

        if(_lastTriggerFrame == Time.frameCount)
        {
            FSMAnimator.Update(0);

            if (FSMAnimator.ContainsParam(_transitionID.ToString()))
                FSMAnimator.ResetTrigger(_transitionID.ToString());
        }

        _lastTriggerFrame = Time.frameCount;
    }


    void StartResetTriggerProgress()
    {
        _resetTriggerRoutine = ResetTriggerProgress();
        StartCoroutine(_resetTriggerRoutine);
    }

    IEnumerator ResetTriggerProgress()
    {
        while (true)
        {
            for (int i = 0; i < _triggerList.Count; i++)
            {
                if (Time.frameCount - _triggerList[i].TriggerTime >= 2)
                {
                    if (FSMAnimator.ContainsParam(_triggerList[i].StateID.ToString()))
                        FSMAnimator.ResetTrigger(_triggerList[i].StateID.ToString());

                    _triggerList.RemoveAt(i);
                    i--;
                }
            }
            yield return new WaitForFixedUpdate();

        }
    }

    void OnStateEntered(FSMStateID stateID)
    {
        if (IsDebugActive)
            Debug.Log("state entered: " + stateID + " frame: " + Time.renderedFrameCount);

        PrevStateID = CurStateID;

        CurStateID = stateID;

        FireOnStateChanged();
    }

    void OnStateExited(FSMStateID stateID)
    {
        if (IsDebugActive)
            Debug.Log("state exited: " + stateID + " frame: " + Time.renderedFrameCount);
    }
}