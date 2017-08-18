using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMTransitionBehaviour : MonoBehaviour
{
    public FSMController FSMController;

    public void DOFSMTransition(FSMStateID transition)
    {
        FSMController.SetTransition(transition);
    }
}
