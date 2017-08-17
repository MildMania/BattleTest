using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    #region Events
    public static Action OnGameStarted;

    void FireOnGameStarted()
    {
        if (OnGameStarted != null)
            OnGameStarted();
    }

    public static Action OnGameEnded;

    void FireOnGameEnded()
    {
        if (OnGameEnded != null)
            OnGameEnded();
    }

    #endregion

    private void Awake()
    {
        Instance = this;
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    public void StartGame()
    {
        FireOnGameStarted();
    }

    public void EndGame()
    {
        FireOnGameEnded();
    }
}
