using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class FlashBehaviour : MMBehaviour
{
    public List<Renderer> FlashRendererList;

    public float Duration;
    public float Interval;

    IEnumerator _flashRoutine; 

    public FlashBehaviour Flash()
    {
        StartFlash();

        return this;
    }

    public void Stop()
    {
        StopFlash();
    }

    void StartFlash()
    {
        StopFlash();
        
        _flashRoutine = FlashProgress();
        StartCoroutine(_flashRoutine);
    }

    void StopFlash()
    {
        if (_flashRoutine != null)
            StopCoroutine(_flashRoutine);

        FlashRendererList.ForEach(val => SetFlashActive(val, false));
    }

    IEnumerator FlashProgress()
    {
        float timer = Duration;

        while (timer > 0)
        {
            foreach (Renderer r in FlashRendererList)
                SetFlashActive(r, true);

            yield return new WaitForSeconds(Interval);

            foreach (Renderer r in FlashRendererList)
                SetFlashActive(r, false);

            yield return new WaitForSeconds(Interval);

            timer -= 2.0f * Interval;
        }

        foreach (Renderer r in FlashRendererList)
            SetFlashActive(r, false);

        FireOnComplete();
    }

    void SetFlashActive(Renderer renderer, bool isActive)
    {
        Color color = Color.black;

        if (isActive)
            color = Color.white;
        
        MaterialPropertyBlock mpb = new MaterialPropertyBlock();
        renderer.GetPropertyBlock(mpb);
        mpb.SetColor("_AddColor", color);
        renderer.SetPropertyBlock(mpb);
    }
}
