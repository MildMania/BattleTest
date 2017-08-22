using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum Fade
{
    In,
    Out
}


static partial class Utilities
{
    public static bool IsVisibleFrom(Renderer renderer, Plane[] planes)
    {
        return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
    }

    public static IEnumerator WaitForParticlePlay(ParticleSystem particleSystem, Action<ParticleSystem> callback)
    {
        while (particleSystem.isPlaying)
            yield return null;

        if (callback != null)
            callback(particleSystem);
    }

    public static Bounds GetBiggestBoundInChildren(Transform parent)
    {
        Bounds bounds;
        if (parent.GetComponent<Renderer>())
        {
            bounds = parent.GetComponent<Renderer>().bounds;
            foreach (Transform child in parent.transform)
            {
                bounds.Encapsulate(child.gameObject.GetComponent<Renderer>().bounds);
            }
        }
        else
        {
            // First find a center for your bounds.
            Vector3 center = Vector3.zero;

            foreach (Transform child in parent.transform)
            {
                center += child.gameObject.GetComponent<Renderer>().bounds.center;
            }
            center /= parent.transform.childCount; //center is average center of children

            //Now you have a center, calculate the bounds by creating a zero sized 'Bounds', 
            bounds = new Bounds(center, Vector3.zero);

            foreach (Transform child in parent.transform)
            {
                bounds.Encapsulate(child.gameObject.GetComponent<Renderer>().bounds);
            }
        }


        return bounds;
    }

    #region Animation Region

    public static IEnumerator FlashRendererList(List<Renderer> rendererList, Material defMat, Material flashMat, float totalDuration, float flashInterval, Action onComplete = null)
    {
        float timer = totalDuration;

        while (timer > 0)
        {
            foreach (Renderer r in rendererList)
            {
                r.material = flashMat;
            }

            yield return new WaitForSeconds(flashInterval);

            foreach (Renderer r in rendererList)
            {
                r.material = defMat;
            }

            yield return new WaitForSeconds(flashInterval);

            timer -= 2.0f * flashInterval;
        }
        foreach (Renderer r in rendererList)
        {
            r.material = defMat;
        }

        if (onComplete != null)
            onComplete();
    }

    public static IEnumerator FlashRenderer(Renderer renderer, Material defMat, Material flashMat, float totalDuration, float flashInterval)
    {
        float timer = totalDuration;

        while (timer > 0)
        {
            renderer.material = flashMat;

            yield return new WaitForSeconds(flashInterval);

            renderer.material = defMat;

            yield return new WaitForSeconds(flashInterval);

            timer -= 2.0f * flashInterval;
        }

        renderer.material = defMat;
    }

    #endregion
}
