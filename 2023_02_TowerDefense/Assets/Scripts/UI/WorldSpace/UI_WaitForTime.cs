using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_WaitForTime : UI_Base
{
    enum Scrollbars
    {
        Scrollbar
    }

    protected override bool Init()
    {
        if (base.Init() == false)
            return false;
        BindScrollbar(typeof(Scrollbars));
        return true;
    }

    private void Update()
    {
        transform.rotation = Camera.main.transform.rotation;
    }

    public void OnWait(float duraton, Action action)
    {
        StartCoroutine(CoWait(duraton, action));
    }

    IEnumerator CoWait(float duration, Action action)
    {
        float currentTime = 0f;

        while(currentTime < duration)
        {
            currentTime += Time.deltaTime;
            GetScrollbar((int)Scrollbars.Scrollbar).size = currentTime / duration;
            yield return null;
        }

        action.Invoke();
        Managers.Resource.Destory(gameObject);
    }
}
