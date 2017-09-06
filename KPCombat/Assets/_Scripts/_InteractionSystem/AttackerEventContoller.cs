using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerEventContoller : MonoBehaviour
{
    public AttackerBase Attacker;

    public bool IsDebugEnabled;

    public void AttackStarted()
    {
        if (IsDebugEnabled)
            Debug.Log("attack started");

        Attacker.IsInteractionActive = true;
    }

    public void AttackFinished()
    {
        if (IsDebugEnabled)
            Debug.Log("attack finished");

        Attacker.IsInteractionActive = false;
    }
}
