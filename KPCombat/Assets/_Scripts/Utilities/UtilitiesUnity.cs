﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

static partial class Utilities
{
    public static YieldInstruction WaitForFixedUpdate;
    public static YieldInstruction WaitForEndOfFrame;

    public static void Initialize()
    {
        WaitForFixedUpdate = new UnityEngine.WaitForFixedUpdate();
        WaitForEndOfFrame = new UnityEngine.WaitForEndOfFrame();

        InitializeMath();
    }

    public static List<T> FindObjectsOfTypeAll<T>()
    {
        List<T> results = new List<T>();
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            var s = SceneManager.GetSceneAt(i);
            if (s.isLoaded)
            {
                var allGameObjects = s.GetRootGameObjects();
                for (int j = 0; j < allGameObjects.Length; j++)
                {
                    var go = allGameObjects[j];
                    results.AddRange(go.GetComponentsInChildren<T>(true));
                }
            }
        }
        return results;
    }
}
