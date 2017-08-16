using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
    public FSMController FSMController;

    private void Awake()
    {
        KeyboardManager.AddListener(KeyCode.S, KeyState.Down, OnGameStarted);
    }

    void OnGameStarted()
    {
        KeyboardManager.RemoveListener(KeyCode.S, KeyState.Down, OnGameStarted);

        FSMController.SetTransition(FSMStateID.MOVE);
    }

}
