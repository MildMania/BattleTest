using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MMGameSceneBehaviour
{
    public FSMController FSMController;

    protected override void OnGameStarted()
    {
        base.OnGameStarted();

        FSMController.SetTransition(FSMStateID.MOVE);
    }
}
