using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerEventContoller : MonoBehaviour
{
    public AttackerBase Attacker;

    public void AttackStarted()
    {
        Attacker.IsInteractionActive = true;
    }

    public void AttackFinished()
    {
        Attacker.IsInteractionActive = false;
    }
}
