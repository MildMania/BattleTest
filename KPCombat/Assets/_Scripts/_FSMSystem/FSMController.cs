using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FSMController : MonoBehaviour
{
    public bool IsDebugEnabled;

    List<MMFSM> _fsmList;
    List<FSMTransitionChecker> _transitionCheckerList;

    FSMStateID _transitionID;

    void Awake()
    {
        InitFSMList();

        InitTransitionChecker();
    }

    void InitFSMList()
    {
        _fsmList = GetComponentsInChildren<MMFSM>().ToList();
    }

    void InitTransitionChecker()
    {
        _transitionCheckerList = GetComponentsInChildren<FSMTransitionChecker>().ToList();
    }

    public void SetFSMControllerActive(bool isActive)
    {
        _fsmList.ForEach(val => val.SetFSMActive(isActive));
    }

    public void SetState(FSMStateID stateID)
    {
        _fsmList.ForEach(val => val.SetState(stateID));
    }

    public void SetTransition(FSMStateID stateID)
    {
        _transitionID = stateID;

        CheckTransition();

    }

    void CheckTransition()
    {
        try
        {
            FSMTransitionChecker checker = _transitionCheckerList.Single(val => val.StateID == _transitionID);

            checker.CheckCondition(OnCheckFinished);
        }
        catch
        {
            TriggerTransition();
        }
    }

    void OnCheckFinished(bool result)
    {
        if (result)
            TriggerTransition();
    }

    void TriggerTransition()
    {
        if (IsDebugEnabled)
            Debug.Log("<color=magenta>transition triggered: " + _transitionID + " frame: " + Time.renderedFrameCount + "</color>");

        _fsmList.ForEach(val => val.SetTransition(_transitionID));

    }

    public MMFSM GetFSMOfType(FSMType type)
    {
        try
        {
            return _fsmList.Single(val => val.FSMType == type);
        }
        catch
        {
            return null;
        }
    }

    public FSMStateID GetCurStateIDOfFSM(FSMType type)
    {
        MMFSM fsm = GetFSMOfType(type);

        if (fsm == null)
            return FSMStateID.NULL_STATE;

        return fsm.CurStateID;
    }

    public FSMStateID GetPrevStateIDOfFSM(FSMType type)
    {
        MMFSM fsm = GetFSMOfType(type);

        if (fsm == null)
            return FSMStateID.NULL_STATE;

        return fsm.PrevStateID;
    }

    public void SetParameter(FSMType fsmType, string parameterName, object parameterValue)
    {
        MMFSM fsm = GetFSMOfType(fsmType);

        fsm.SetParameter(parameterName, parameterValue);
    }

    public int GetIntegerParameter(FSMType fsmType, string parameterName)
    {
        MMFSM fsm = GetFSMOfType(fsmType);

        return fsm.GetIntegerParameter(parameterName);
    }

    public float GetFloatParameter(FSMType fsmType, string parameterName)
    {
        MMFSM fsm = GetFSMOfType(fsmType);

        return fsm.GetFloatParameter(parameterName);
    }

    public bool GetBoolParameter(FSMType fsmType, string parameterName)
    {
        MMFSM fsm = GetFSMOfType(fsmType);

        return fsm.GetBoolParameter(parameterName);
    }

    public FSMBehaviourController GetBehaviourControllerOfState(FSMType fsmType, FSMStateID stateID)
    {
        try
        {
            MMFSM fsm = GetFSMOfType(fsmType);

            return fsm.GetBehaviourControllerOfState(stateID);
        }
        catch
        {
            return null;
        }
    }
}