using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MMGameSceneBehaviour
{
    public FSMController FSMController;

    protected override void StartListeningEvents()
    {
        base.StartListeningEvents();

        KeyboardManager.AddListener(KeyCode.S, KeyState.Down, StartGameButtonPressed);
        GestureManager.OnFingerDown += StartGameInputGiven;
    }

    void StartGameButtonPressed()
    {
        KeyboardManager.RemoveListener(KeyCode.S, KeyState.Down, StartGameButtonPressed);

        GameManager.Instance.StartGame();
    }

    void StartGameInputGiven(int index, Vector2 input)
    {
        GestureManager.OnFingerDown -= StartGameInputGiven;

        GameManager.Instance.StartGame();
    }

    protected override void OnGameStarted()
    {
        base.OnGameStarted();

        FSMController.SetTransition(FSMStateID.MOVE);
    }

}
