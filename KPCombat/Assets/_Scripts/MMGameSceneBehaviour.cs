using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMGameSceneBehaviour : MonoBehaviour
{
    protected virtual void Awake()
    {
        StartListeningEvents();
    }

    protected virtual void OnDestroy()
    {
        StopListeningEvents();
    }

    protected virtual void StartListeningEvents()
    {
        GameManager.OnGameStarted += OnGameStarted;
        GameManager.OnGameEnded += OnGameEnded;
    }

    protected virtual void StopListeningEvents()
    {
        StopListeningGameEvents();

        GameManager.OnGameStarted -= OnGameStarted;
        GameManager.OnGameEnded -= OnGameEnded;
    }

    protected virtual void OnGameStarted()
    {
        StartListeningGameEvents();
    }

    protected virtual void StartListeningGameEvents()
    {

    }

    protected virtual void StopListeningGameEvents()
    {

    }

    protected virtual void OnGameEnded()
    {
        StopListeningGameEvents();
    }
}
