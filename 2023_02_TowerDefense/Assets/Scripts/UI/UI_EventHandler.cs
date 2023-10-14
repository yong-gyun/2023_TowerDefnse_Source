using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_EventHandler : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Action<PointerEventData> OnPointerClickAction = null;
    public Action<PointerEventData> OnPointerEnterAction = null;
    public Action<PointerEventData> OnPointerExitAction = null;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (OnPointerClickAction != null)
            OnPointerClickAction.Invoke(eventData);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (OnPointerEnterAction != null)
            OnPointerEnterAction.Invoke(eventData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (OnPointerExitAction != null)
            OnPointerExitAction.Invoke(eventData);
    }
}
