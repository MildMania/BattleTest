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
        GestureManager.OnFingerUp += StartGameInputGiven;
    }

    protected override void StopListeningEvents()
    {
        base.StopListeningEvents();

        GestureManager.OnFingerUp -= StartGameInputGiven;
    }

    void StartGameButtonPressed()
    {
        KeyboardManager.RemoveListener(KeyCode.S, KeyState.Down, StartGameButtonPressed);

        GameManager.Instance.StartGame();
    }

    void StartGameInputGiven(int index, Vector2 input)
    {
        GestureManager.OnFingerUp -= StartGameInputGiven;

        GameManager.Instance.StartGame();
    }

    protected override void OnGameStarted()
    {
        base.OnGameStarted();

        FSMController.SetTransition(FSMStateID.MOVE);
    }

}
