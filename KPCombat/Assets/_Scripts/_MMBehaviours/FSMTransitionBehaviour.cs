using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMTransitionBehaviour : MonoBehaviour
{
    public FSMController FSMController;

    public FSMStateID TransitionID
    {
        get
        {
            return FSMController.TransitionID;
        }
    }

    public void DOFSMTransition(FSMStateID transition)
    {
        FSMController.SetTransition(transition);
    }
}
